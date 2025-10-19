using App.Business.Services.InternalServices.Interfaces;
using App.Core.DTOs.AdoptionRequestDTOs;
using App.Core.DTOs.BreedDTOs;
using App.Core.Entities;
using App.Core.Enums;
using App.DAL.Repositories.Implementations;
using App.DAL.Repositories.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Services.InternalServices.Abstractions
{
    public class AdoptionRequestService : IAdoptionRequestService
    {
        readonly IAdoptionRequestRepository _adoptionRequestRepository;
        readonly IMapper _mapper;

        public AdoptionRequestService(IAdoptionRequestRepository adoptionRequestRepository, IMapper mapper)
        {
            _adoptionRequestRepository = adoptionRequestRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(CreateAdoptionRequestDTO dto)
        {
            var entity = _mapper.Map<AdoptionRequest>(dto);
            var result = await _adoptionRequestRepository.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = _adoptionRequestRepository.GetById(x => x.Id == id, tracking: false);
            var result = await _adoptionRequestRepository.DeleteAsync(entity);
        }

        public IQueryable<AdoptionRequestDTO> GetAll()
        {
            var entities = _adoptionRequestRepository.GetAll(x => x.IsDeleted == false, tracking: false);

            return entities.Select(x => _mapper.Map<AdoptionRequestDTO>(x));
        }

        public AdoptionRequestDTO GetById(int id)
        {
            var entity = _adoptionRequestRepository.GetById(x => x.Id == id && x.IsDeleted == false, tracking: false);

            return _mapper.Map<AdoptionRequestDTO>(entity);
        }

        public async Task RecoverAsync(int id)
        {
            var entity = _adoptionRequestRepository.GetById(x => x.Id == id, tracking: false);
            var result = await _adoptionRequestRepository.RecoverAsync(entity);
        }

        public Task RemoveAsync(int id)
        {
            var entity = _adoptionRequestRepository.GetById(x => x.Id == id, tracking: false);
            return _adoptionRequestRepository.RemoveAsync(entity);
        }

        public async Task UpdateAdoptionRequestStatusAsync(int id, ModifyAdoptionRequestStatusDTO dto)
        {
            var entity = _adoptionRequestRepository.GetById(x => x.Id == id);

            if (!Enum.TryParse<EAdoptionStatus>(dto.Value.Replace(" ", ""), true, out var newStatus))
            {
                throw new Exception("Invalid status keyword");
            }

            entity.Status = newStatus;
            await _adoptionRequestRepository.UpdateAsync(entity);
        }
    }
}
