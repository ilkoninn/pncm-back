using App.Core.DTOs.Commons;
using App.Core.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.DTOs.UserDTOs
{
    public class UpdateUserDTO : IAuditedImageEntityDTO
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public IFormFile? Image { get; set; }
        public string Role { get; set; }
    }
}