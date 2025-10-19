using App.Core.Entities;
using App.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.DTOs.ReportDTOs
{
    public class CreateReportDTO
    {
        public string Reason { get; set; }
        public string? Description { get; set; }

        public int PetId { get; set; }
        public int ReporterId { get; set; }
    }
}
