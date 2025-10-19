using App.Business.MappingProfiles.Commons;
using App.Core.DTOs.SpeciesDTOs;
using App.Core.DTOs.UserDTOs;
using App.Core.Entities;
using App.Core.Entities.Identity;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.MappingProfiles
{
    public class SpeciesMP : Profile
    {
        public SpeciesMP()
        {
            CreateMap<Species, SpeciesDTO>().ReverseMap();
            CreateMap<CreateSpeciesDTO, Species>()
                .ForMember(dest => dest.IconUrl, opt => opt.Ignore())
                .AfterMap<CustomMappingAction<CreateSpeciesDTO, Species>>();
            CreateMap<UpdateSpeciesDTO, Species>()
                .ForMember(dest => dest.IconUrl, opt => opt.Ignore())
                .AfterMap<CustomMappingAction<CreateSpeciesDTO, Species>>();
        }
    }
}
