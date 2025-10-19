using App.Core.DTOs.PetDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Services.InternalServices.Interfaces
{
    public interface IPetService
    {
        IQueryable<PetDTO> GetAll();
        PetDTO GetById(int id);

        Task<PetDTO> CreateAsync(CreatePetDTO dto);
        Task<PetDTO> UpdateAsync(int id, UpdatePetDTO dto);
        Task DeleteAsync(int id);
        Task RecoverAsync(int id);
        Task RemoveAsync(int id);
    }
}
