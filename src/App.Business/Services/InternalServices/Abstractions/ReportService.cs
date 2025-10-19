using App.Business.Services.InternalServices.Interfaces;
using App.Core.DTOs.ReportDTOs;
using App.Core.Entities;
using App.DAL.Repositories.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Services.InternalServices.Abstractions
{
    public class ReportService : IReportService
    {
        readonly IReportRepository _reportRepository;
        readonly IMapper _mapper;

        public ReportService(IReportRepository reportRepository, IMapper mapper)
        {
            _reportRepository = reportRepository;
            _mapper = mapper;
        }

        public async Task<ReportDTO> CreateAsync(CreateReportDTO dto)
        {
            var entity = _mapper.Map<Report>(dto);
            var result = await _reportRepository.AddAsync(entity);

            return _mapper.Map<ReportDTO>(result);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = _reportRepository.GetById(x => x.Id == id, tracking: false);
            var result = await _reportRepository.DeleteAsync(entity);
        }

        public IQueryable<ReportDTO> GetAll()
        {
            var entities = _reportRepository.GetAll(x => x.IsDeleted == false, tracking: false);

            return entities.Select(x => _mapper.Map<ReportDTO>(x));
        }

        public ReportDTO GetById(int id)
        {
            var entity = _reportRepository.GetById(x => x.Id == id && x.IsDeleted == false, tracking: false);

            return _mapper.Map<ReportDTO>(entity);
        }

        public async Task RecoverAsync(int id)
        {
            var entity = _reportRepository.GetById(x => x.Id == id, tracking: false);
            var result = await _reportRepository.RecoverAsync(entity);
        }

        public Task RemoveAsync(int id)
        {
            var entity = _reportRepository.GetById(x => x.Id == id, tracking: false);
            return _reportRepository.RemoveAsync(entity);
        }
    }
}
