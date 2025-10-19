using App.Core.Entities.Commons;
using App.Core.Enums.UserEnums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Entities.Identity
{
    public class User : IdentityUser
    {
        public string? FullName { get; set; }
        public string? ImageUrl { get; set; }
        public EUserVerificationStatus EVerificationStatus { get; set; }

        public ICollection<Pet> Pets { get; set; }
        public ICollection<AdoptionRequest> AdoptionRequests { get; set; }
    }
}
