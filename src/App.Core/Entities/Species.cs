using App.Core.Entities.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Entities
{
    public class Species : AuditableEntity
    {
        public string Name { get; set; }
        public string IconUrl { get; set; }

        public ICollection<Breed> Breeds { get; set; }
    }
}
