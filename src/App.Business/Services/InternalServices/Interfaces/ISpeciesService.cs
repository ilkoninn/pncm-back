using App.Core.DTOs.SpeciesDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Services.InternalServices.Interfaces
{
    public interface ISpeciesService
    {
        IQueryable<SpeciesDTO> GetAll();
        SpeciesDTO GetById(int id);

        Task<SpeciesDTO> CreateAsync(CreateSpeciesDTO dto);
        Task<SpeciesDTO> UpdateAsync(int id, UpdateSpeciesDTO dto);
        Task DeleteAsync(int id);
        Task RecoverAsync(int id);
        Task RemoveAsync(int id);
    }
}
