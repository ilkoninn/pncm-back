using App.Core.Entities;
using App.Core.Entities.Identity;
using App.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.DTOs.AdoptionRequestDTOs
{
    public class AdoptionRequestDTO
    {
        public string? Message { get; set; }
        public EAdoptionStatus Status { get; set; }

        public int PetId { get; set; }
        public string PetName { get; set; }

        public int RequesterId { get; set; }
        public string RequesterFullName { get; set; }
    }
}
