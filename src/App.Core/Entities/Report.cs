using App.Core.Entities.Commons;
using App.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Entities
{
    public class Report : AuditableEntity
    {
        public string Reason { get; set; }
        public string? Description { get; set; }

        public int PetId { get; set; }
        public Pet Pet { get; set; }

        public int ReporterId { get; set; }
        public User Reporter { get; set; }
    }
}
