using App.Core.DTOs.PetDTOs;
using App.Core.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.MappingProfiles
{
    public class PetMP : Profile
    {
        public PetMP()
        {
            CreateMap<Pet, PetDTO>().ReverseMap();
            CreateMap<CreatePetDTO, Pet>();
            CreateMap<UpdatePetDTO, Pet>();
        }
    }
}
