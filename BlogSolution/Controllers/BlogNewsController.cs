using Blog.IService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Blog.Mapping.BlogNewsDTO;
using BlogModel;

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
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
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
            return NoContent();
        }

        // PUT api/<BlogNewsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BlogNewsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
