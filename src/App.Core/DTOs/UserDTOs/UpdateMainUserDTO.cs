using App.Core.DTOs.Commons;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.DTOs.UserDTOs
{
    public class UpdateMainUserDTO : IAuditedImageEntityDTO
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public IFormFile? Image { get; set; }
        public string? Password { get; set; }
    }
}