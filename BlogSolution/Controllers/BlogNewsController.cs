using Blog.IService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Blog.Mapping.BlogNewsDTO;
using BlogModel;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlogSolution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogNewsController : ControllerBase
    {
        private readonly IBlogNewsServer _blogNewsService;
        private readonly IMapper _mapper;
        public BlogNewsController(IBlogNewsServer blogNewsService, IMapper mapper)
        {
            this._blogNewsService = blogNewsService;
            _mapper = mapper;
        }
        // GET: api/<BlogNewsController>
        [HttpGet]
        public async Task<ActionResult> GetBlogNews()
        {
            var data = await _blogNewsService.QueryAsync();
            if (data.Count() <= 0)
            {
                return NotFound("没有找到一条关于博客文章信息！");
            }
            return Ok(_mapper.Map<IEnumerable<BlogNewsGetDTO>>(data));
        }

        // GET api/<BlogNewsController>/5
        [HttpGet("{id}",Name = "GetNewsById")]
        public async Task<IActionResult> GetNewsById([FromRoute]int id)
        {
            var data = await _blogNewsService.FindAsync(id);
            if (data==null)
            {
                return NotFound($"没有找到ID={id}的博客文章。");
            }
            return Ok(_mapper.Map<BlogNewsGetDTO>(data));
        }

        // POST api/<BlogNewsController>
        [HttpPost]
        public async Task<IActionResult> CreateOneBlogNews([FromBody] BlogNewsCreateDTO blogNews)
        {
            var createData = _mapper.Map<BlogNews>(blogNews);
            if (createData==null)
            {
                return BadRequest();
            }
            bool flag = await _blogNewsService.AddAsync(createData);
            if (!flag)
            {
                return BadRequest();
            }
            return Created("GetNewsById",createData.ID);
        }

        // PUT api/<BlogNewsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdataBlogNewsByID([FromRoute]int id, [FromBody] BlogNewsUpdataDTO blogDto)
        {
            var blogNewsDataFromResponse = await _blogNewsService.FindAsync(id);
            if (blogNewsDataFromResponse==null)
            {
                return NotFound($"要更新的资源ID={id}不存在");
            }
            _mapper.Map(blogDto, blogNewsDataFromResponse);
            await _blogNewsService.EditAsync(blogNewsDataFromResponse);
            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchBlogNewsByIdAsync([FromRoute] int id, [FromBody] JsonPatchDocument <BlogNewsUpdataDTO> blogDto)
        {
            var blogNewsDataFromResponse = await _blogNewsService.FindAsync(id);
            if (blogNewsDataFromResponse == null)
            {
                return NotFound($"要更新的资源ID={id}不存在");
            }
            var blogNewsDataFromDto = _mapper.Map<BlogNewsUpdataDTO>(blogNewsDataFromResponse);

            blogDto.ApplyTo(blogNewsDataFromDto);
             _mapper.Map(blogNewsDataFromDto, blogNewsDataFromResponse);
            await _blogNewsService.EditAsync(blogNewsDataFromResponse);
            return Ok();
        }

        // DELETE api  [Authorize(Roles = "admin", AuthenticationSchemes = "Bearer")]
        [HttpDelete("{id}")]
        [Authorize(Roles ="root",AuthenticationSchemes ="Bearer")]
        public async Task<IActionResult> DeleteBlogNewsByID([FromRoute]int id)
        {
            var deleEntity = await _blogNewsService.FindAsync(id);
            if (deleEntity==null)
            {
                return NotFound($"要删除的ID={id}文章不存在！");
            }
            await _blogNewsService.DeletAsync(deleEntity);
            return NoContent();
        }
    }
}
