using AutoMapper;
using BlogModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Mapping.AuthorDTO
{
    public class BlogAuthorProfile : Profile
    {
        public BlogAuthorProfile()
        {
            CreateMap<AuthorCreateDTO,Author>();
            CreateMap<Author, AuthorGetDTO>();
        }
    }
}
