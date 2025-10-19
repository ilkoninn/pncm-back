using App.Core.Entities.Commons;
using App.Core.Entities.Identity;
using App.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Entities
{
    public class AdoptionRequest : AuditableEntity
    {
        public string? Message { get; set; } 
        public EAdoptionStatus Status { get; set; } 

        public int PetId { get; set; }
        public Pet Pet { get; set; }

        public int RequesterId { get; set; }
        public User Requester { get; set; }
    }
}
