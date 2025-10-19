using App.Core.Entities.Commons;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Entities
{
    public class Breed : AuditableEntity
    {
        public string Name { get; set; }

        public int SpeciesId { get; set; }
        public Species Species { get; set; }
        
        public ICollection<Pet> Pets { get; set; }
    }
}
