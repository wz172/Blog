using AutoMapper;
using Blog.IService;
using Blog.Mapping;
using Blog.Mapping.AuthorDTO;
using Blog.Service;
using BlogModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlogSolution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public AuthorsController(
             IAuthorService authorService,
              IMapper mapper,
            IConfiguration configuration
            )
        {
            this._authorService = authorService;
            this._mapper = mapper;
            this._configuration = configuration;
        }

        #region CRUD 异步方法

        [HttpGet(Name = "GetAllBlogAuthorsAsync")]
        public async Task<IActionResult> GetAllBlogAuthorsAsync()
        {
            IEnumerable<Author> dataFromServices = await _authorService.QueryAsync();
            if (dataFromServices.Count() == 0)
            {
                return NotFound("没有查到任何数据。");
            }
            var dataMap = _mapper.Map<IEnumerable<AuthorGetDTO>>(dataFromServices);
            return Ok(dataMap);
        }

        [HttpPost(Name = "CreateAuthorAsync")]
        public async Task<IActionResult> CreateAuthorAsync([FromBody] AuthorCreateDTO author)
        {
            if (author == null)
            {
                return BadRequest(nameof(author));
            }
            var modelAuthor = _mapper.Map<Author>(author);
            bool flag = await _authorService.AddAsync(modelAuthor);
            if (!flag)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, "创建失败。");
            }
            return NoContent();
        }

        [HttpPut("{id}", Name = "EditAuthorAsync")]
        public async Task<IActionResult> EditAuthorAsync([FromBody] AuthorEditDTO authorEdit, [FromRoute] int id)
        {
            if (authorEdit == null)
            {
                return BadRequest(nameof(authorEdit));
            }
            authorEdit.ID = id;
            var modelAuthor = _mapper.Map<Author>(authorEdit);
            var editFlag = await _authorService.EditAsync(modelAuthor);
            if (!editFlag)
            {
                return BadRequest($"修改作者id={id}信息失败！");
            }
            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteAuthorAsync")]
        public async Task<IActionResult> DeleteAuthorAsync([FromRoute] int id)
        {
            var dataFromServiceByid = await _authorService.FindAsync(id);
            if (dataFromServiceByid == null)
            {
                return NotFound($"找不到id={id}信息。");
            }
            await _authorService.DeletAsync(dataFromServiceByid);
            return NoContent();
        }

        #endregion

        #region JWT 登录
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> AutorLogin([FromBody] AuthorLogInDTO authorLogInDTO)
        {
            if (authorLogInDTO == null)
            {
                return BadRequest("登录对象不能为空！");
            }
            var dataAuthors = await _authorService.QueryAsync(ret => ret.UserName == authorLogInDTO.UserName );
            var dataAuthorFromService = dataAuthors.ToList().FirstOrDefault();
            if (dataAuthorFromService == null)
            {
                return NotFound($"用户名：{authorLogInDTO.UserName}信息不存在！");
            }
            var authorTransFromDto = _mapper.Map<Author>(authorLogInDTO);
            if (!(authorTransFromDto.UserName == dataAuthorFromService.UserName && authorTransFromDto.UserPassword == dataAuthorFromService.UserPassword))
            {
                return BadRequest(nameof(authorLogInDTO));
            }
            //此时开始生成JWT 码
            //1. 确定加密算法
            var head = SecurityAlgorithms.HmacSha256;
            //2.加密所有要的元数据
            var dataClaims = new List<Claim>()
            {
                    new Claim(JwtRegisteredClaimNames.Sub,dataAuthorFromService.ID.ToString()),
                    new Claim(ClaimTypes.Role,dataAuthorFromService.UserName)
            };
            //3.数字签名
            var configkeyBytes = Encoding.UTF8.GetBytes(_configuration["SignatureKey:loginKey"]);
            var signingKey = new SymmetricSecurityKey(configkeyBytes);
            var mysigningCreadentials = new SigningCredentials(signingKey, head);

            //4. 获取token
            var token = new JwtSecurityToken(
                  issuer: _configuration["SignatureKey:Issuer"],
                  audience: _configuration["SignatureKey:Audience"],
                   dataClaims,
                  notBefore: DateTime.UtcNow,
                  expires: DateTime.UtcNow.AddDays(int.Parse(_configuration["SignatureKey:ExpiresDays"])),
                  mysigningCreadentials
              );
            var tokenStr = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(tokenStr);
        }

        #endregion
    }
}
