using Blog.IResponse;
using Blog.IService;
using BlogModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service
{

   public class BlogAuthorService:BlogBaseService<Author>, IAuthorService
    {
        public BlogAuthorService(IBaseResponse<Author> response):base(response)
        {

        }
    }
}
