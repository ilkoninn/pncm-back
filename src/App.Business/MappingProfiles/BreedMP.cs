using App.Core.DTOs.BreedDTOs;
using App.Core.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.MappingProfiles
{
    public class BreedMP : Profile
    {
        public BreedMP()
        {
            CreateMap<Breed, BreedDTO>().ReverseMap();
            CreateMap<CreateBreedDTO, Breed>();
            CreateMap<UpdateBreedDTO, Breed>();
        }
    }
}
