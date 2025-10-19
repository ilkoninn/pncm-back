using App.Core.DTOs.Commons;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.DTOs.SpeciesDTOs
{
    public class CreateSpeciesDTO : IAuditedIconEntityDTO
    {
        public string Name { get; set; }
        public IFormFile Icon { get; set; }
    }
}
