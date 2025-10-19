using App.Core.Entities.Commons;
using App.Core.Entities.Identity;
using App.Core.Enums.PetEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Entities
{
    public class Pet : AuditableEntity
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
        public Breed Breed { get; set; }

        public int OwnerId { get; set; }
        public User Owner { get; set; }

        // New Field !!!!!!!!!!!!!!!!!!!!!!!!!!
        public string ImageUrl { get; set; }
        public ICollection<PetPhoto> Photos { get; set; }
    }
}
