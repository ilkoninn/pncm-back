using App.Core.DTOs.AdoptionRequestDTOs;
using App.Core.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.MappingProfiles
{
    public class AdoptionRequestMP : Profile
    {
        public AdoptionRequestMP()
        {
            CreateMap<AdoptionRequest, AdoptionRequestDTO>().ReverseMap();
            CreateMap<CreateAdoptionRequestDTO, AdoptionRequest>();
        }
    }
}
