using Blog.IService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlogSolution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogNewsController : ControllerBase
    {
        private readonly IBlogNewsServer _blogNewsService;
        public BlogNewsController(IBlogNewsServer blogNewsService)
        {
            this._blogNewsService = blogNewsService;
        }
        // GET: api/<BlogNewsController>
        [HttpGet]
        public async Task<ActionResult> GetBlogNews()
        {
            var data = await _blogNewsService.QueryAsync();
            if (data.Count()<=0)
            {
                return NotFound("没有找到一条关于博客文章信息！");
            }
            return  Ok(data);
        }

        // GET api/<BlogNewsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BlogNewsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
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
