using AutoMapper;
using BlogCommon;
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
            CreateMap<AuthorCreateDTO, Author>().
                ForMember(
                    dest => dest.UserPassword,
                    original => original.MapFrom(src => Util.MD5Encrpy(src.UserPassword))
                );
            CreateMap<Author, AuthorGetDTO>();

            CreateMap<AuthorBaseDTO, Author>();
            CreateMap<Author, AuthorBaseDTO>();

            CreateMap<AuthorEditDTO, Author>()
                .ForMember(
                    dest => dest.UserPassword,
                    original => original.MapFrom(src => Util.MD5Encrpy(src.UserPassword))
                );
            CreateMap<AuthorLogInDTO, Author>()
                .ForMember(
                    dest => dest.UserPassword,
                    original => original.MapFrom(src => Util.MD5Encrpy(src.UserPassword))
                );
        }
    }
}
