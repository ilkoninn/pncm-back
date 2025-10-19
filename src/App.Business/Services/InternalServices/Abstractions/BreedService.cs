using App.Business.Services.InternalServices.Interfaces;
using App.Core.DTOs.BreedDTOs;
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
    public class BreedService : IBreedService
    {
        readonly IBreedRepository _breedRepository;
        readonly IMapper _mapper;

        public BreedService(IBreedRepository breedRepository, IMapper mapper)
        {
            _breedRepository = breedRepository;
            _mapper = mapper;
        }

        public async Task<BreedDTO> CreateAsync(CreateBreedDTO dto)
        {
            var entity = _mapper.Map<Breed>(dto);
            var result = await _breedRepository.AddAsync(entity);

            return _mapper.Map<BreedDTO>(result);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = _breedRepository.GetById(x => x.Id == id, tracking: false);
            var result = await _breedRepository.DeleteAsync(entity);
        }

        public IQueryable<BreedDTO> GetAll()
        {
            var entities = _breedRepository.GetAll(x => x.IsDeleted == false, tracking: false);

            return entities.Select(x => _mapper.Map<BreedDTO>(x));
        }

        public BreedDTO GetById(int id)
        {
            var entity = _breedRepository.GetById(x => x.Id == id && x.IsDeleted == false, tracking: false);

            return _mapper.Map<BreedDTO>(entity);
        }

        public async Task RecoverAsync(int id)
        {
            var entity = _breedRepository.GetById(x => x.Id == id, tracking: false);
            var result = await _breedRepository.RecoverAsync(entity);
        }

        public Task RemoveAsync(int id)
        {
            var entity = _breedRepository.GetById(x => x.Id == id, tracking: false);
            return _breedRepository.RemoveAsync(entity);
        }

        public async Task<BreedDTO> UpdateAsync(int id, UpdateBreedDTO dto)
        {
            var entity = _breedRepository.GetById(x => x.Id == id, tracking: true);
            var mappedEntity = _mapper.Map(dto, entity);

            var result = await _breedRepository.UpdateAsync(mappedEntity);

            return _mapper.Map<BreedDTO>(result);
        }
    }
}
