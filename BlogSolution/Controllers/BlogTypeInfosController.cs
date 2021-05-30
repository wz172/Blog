using AutoMapper;
using Blog.IService;
using Blog.Mapping.BlogTypeInfoDTO;
using BlogModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BlogSolution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogTypeInfosController : ControllerBase
    {
        #region Ioc 注入

        private readonly IBlogTypeInfoService _blogTypeService;

        private readonly IMapper _mapper;

        public BlogTypeInfosController(
            IBlogTypeInfoService blogTypeService,
            IMapper mapper
            )
        {
            this._blogTypeService = blogTypeService;
            this._mapper = mapper;
        }

        #endregion

        [HttpGet(Name = "GetBlogTypesAsync")]
        public async Task<IActionResult> GetBlogTypesAsync()
        {
            var dataFromService = await _blogTypeService.QueryAsync();
            if (dataFromService==null||dataFromService.Count()==0)
            {
                return NotFound("博客文章信息不存在。");
            }
            var dataTranDto = _mapper.Map<IEnumerable< BlogTypeInfoGetDTO>>(dataFromService);
            return Ok(dataTranDto);
        }
        [HttpPost(Name = "CreateBlogTypeInfoAsync")]
        public async Task<IActionResult> CreateBlogTypeInfoAsync([FromBody] BlogTypeInfoCreateDTO infoCreateDTO)
        {
            if (infoCreateDTO==null)
            {
                return BadRequest(nameof(infoCreateDTO));
            }

            var modelBlogType = _mapper.Map<BlogTypeInfo>(infoCreateDTO);
            bool addFlag = await _blogTypeService.AddAsync(modelBlogType);
            if (!addFlag)
            {
                return StatusCode((int)HttpStatusCode.ServiceUnavailable, "添加数据失败。");
            }
            return NoContent();
        }
        [HttpDelete("{id}",Name = "DeleteBlogTypeinfoAsync")]
        public async Task<ActionResult> DeleteBlogTypeinfoAsync([FromRoute] int id)
        {
            //1. 先查出目标实体
            var dataFromService = await _blogTypeService.FindAsync(id);
            if (dataFromService==null)
            {
                return NotFound($"删除 BlogTypeInfo 的id={id} 信息不存在！-");
            }
            await _blogTypeService.DeletAsync(dataFromService);
            return NoContent();
        }

    }
}
