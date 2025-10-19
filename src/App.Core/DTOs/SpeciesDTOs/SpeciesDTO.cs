using App.Core.DTOs.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.DTOs.SpeciesDTOs
{
    public class SpeciesDTO : BaseEntityDTO
    {
        public string Name { get; set; }
        public string IconUrl { get; set; }
    }
}
