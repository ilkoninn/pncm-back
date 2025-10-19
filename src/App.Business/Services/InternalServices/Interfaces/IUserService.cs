using App.Core.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Services.InternalServices.Interfaces
{
    public interface IUserService
    {
        Task<IQueryable<UserDTO>> GetAllAsync();

        Task RemoveAsync(string id);
        Task LockedOutAsync(string id);
        Task RecoverAsync(string id);
        Task<UserDTO> AddAsync(CreateUserDTO dto);
        Task<UserDTO> UpdateAsync(string id, UpdateUserDTO dto);
        Task<UserDTO> UpdateAsync(string id, UpdateMainUserDTO dto);
    }
}
