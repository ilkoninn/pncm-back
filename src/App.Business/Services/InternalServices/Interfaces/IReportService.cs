using App.Core.DTOs.ReportDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Services.InternalServices.Interfaces
{
    public interface IReportService
    {
        IQueryable<ReportDTO> GetAll();
        ReportDTO GetById(int id);

        Task<ReportDTO> CreateAsync(CreateReportDTO dto);
        Task DeleteAsync(int id);
        Task RecoverAsync(int id);
        Task RemoveAsync(int id);
    }
}
