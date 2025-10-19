﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.DTOs.UserDTOs
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string ImageUrl { get; set; }
        public string Role { get; set; }
        public bool IsBanned { get; set; }
    }
}
