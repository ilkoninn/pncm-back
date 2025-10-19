using App.Core.DTOs.AuthDTOs;
using App.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Services.ExternalServices.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDTO> LoginAsync(LoginDTO dto);
        Task<User> CheckUserNotFoundAsync(string id);
    }
}
