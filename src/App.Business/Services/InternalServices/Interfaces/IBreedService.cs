using App.Core.DTOs.BreedDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Services.InternalServices.Interfaces
{
    public interface IBreedService
    {
        IQueryable<BreedDTO> GetAll();
        BreedDTO GetById(int id);

        Task<BreedDTO> CreateAsync(CreateBreedDTO dto);
        Task<BreedDTO> UpdateAsync(int id, UpdateBreedDTO dto);
        Task DeleteAsync(int id);
        Task RecoverAsync(int id);
        Task RemoveAsync(int id);
    }
}
