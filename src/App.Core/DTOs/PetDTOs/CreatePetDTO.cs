using App.Core.DTOs.Commons;
using App.Core.Entities;
using App.Core.Entities.Identity;
using App.Core.Enums.PetEnums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.DTOs.PetDTOs
{
    public class CreatePetDTO
    {
        // Required fields
        public string Name { get; set; }
        public string Location { get; set; }
        public EPetSize Size { get; set; }
        public EPetGender Gender { get; set; }
        public EPetVerificationStatus VerificationStatus { get; set; }

        // Optional fields
        public int? AgeYears { get; set; }
        public int? AgeMonths { get; set; }
        public string? Color { get; set; }
        public string? Description { get; set; }
        public bool Vaccinated { get; set; }
        public string? VaccinationDetails { get; set; }

        // Foreign keys and navigation properties
        public int BreedId { get; set; }
        public int OwnerId { get; set; }

        public ICollection<IFormFile> Images { get; set; }
    }
}
