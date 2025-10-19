using App.Core.DTOs.BlogDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Services.InternalServices.Interfaces
{
    public interface IBlogService
    {
        IQueryable<BlogDTO> GetAll();
        BlogDTO GetById(int id);

        Task<BlogDTO> CreateAsync(CreateBlogDTO dto);
        Task<BlogDTO> UpdateAsync(int id, UpdateBlogDTO dto);
        Task DeleteAsync(int id);
        Task RecoverAsync(int id);
        Task RemoveAsync(int id);
    }
}
