using App.Business.MappingProfiles.Commons;
using App.Core.DTOs.BlogDTOs;
using App.Core.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.MappingProfiles
{
    public class BlogMP : Profile
    {
        public BlogMP()
        {
            CreateMap<Blog, BlogDTO>().ReverseMap();
            CreateMap<CreateBlogDTO, Blog>()
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore())
                .AfterMap<CustomMappingAction<CreateBlogDTO, Blog>>();
            CreateMap<UpdateBlogDTO, Blog>()
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore())
                .AfterMap<CustomMappingAction<CreateBlogDTO, Blog>>();
        }
    }
}
