using AutoMapper;
using BlogModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Mapping.BlogNewsDTO
{
    public class BlogNewsProfile : Profile
    {
        public BlogNewsProfile()
        {
            CreateMap<BlogNews, BlogNewsGetDTO>()
                .ForMember(
                dest => dest.Time,
                orignal => orignal.MapFrom(src => src.Time.ToLongTimeString ())
                ) ;
            CreateMap<BlogNewsCreateDTO, BlogNews>();
        }
    }
}
