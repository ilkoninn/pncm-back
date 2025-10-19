using App.Core.Entities.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Entities
{
    public class PetPhoto : AuditableEntity
    {
        public string ImageUrl { get; set; }

        public int PetId { get; set; }
        public Pet Pet { get; set; }
    }
}
