using App.Core.DTOs.AdoptionRequestDTOs;
using App.Core.DTOs.BlogDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Services.InternalServices.Interfaces
{
    public interface IAdoptionRequestService
    {
        IQueryable<AdoptionRequestDTO> GetAll();
        AdoptionRequestDTO GetById(int id);

        Task CreateAsync(CreateAdoptionRequestDTO dto);  
        Task UpdateAdoptionRequestStatusAsync(int id, ModifyAdoptionRequestStatusDTO dto);
        Task DeleteAsync(int id);
        Task RecoverAsync(int id);
        Task RemoveAsync(int id);
    }
}
