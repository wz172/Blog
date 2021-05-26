using AutoMapper;
using BlogModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Mapping.BlogTypeInfoDTO
{
    public class BlogTypeInfoProfile : Profile
    {
        public BlogTypeInfoProfile()
        {
            CreateMap<BlogTypeInfo, BlogTypeInfoGetDTO>();
            CreateMap<BlogTypeInfoCreateDTO, BlogTypeInfo>();
        }
    }
}
