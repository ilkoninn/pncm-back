using App.Core.DTOs.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.DTOs.BreedDTOs
{
    public class BreedDTO : BaseEntityDTO
    {
        public string Name { get; set; }
        public int SpeciesId { get; set; }
        public string SpeciesName { get; set; }
    }
}
