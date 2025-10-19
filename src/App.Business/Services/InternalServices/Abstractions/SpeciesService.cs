using App.Business.Services.InternalServices.Interfaces;
using App.Core.DTOs.SpeciesDTOs;
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
    public class SpeciesService : ISpeciesService
    {
        readonly ISpeciesRepository _speciesRepository;
        readonly IMapper _mapper;

        public SpeciesService(ISpeciesRepository speciesRepository, IMapper mapper)
        {
            _speciesRepository = speciesRepository;
            _mapper = mapper;
        }

        public async Task<SpeciesDTO> CreateAsync(CreateSpeciesDTO dto)
        {
            var entity = _mapper.Map<Species>(dto);
            var result = await _speciesRepository.AddAsync(entity);

            return _mapper.Map<SpeciesDTO>(result);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = _speciesRepository.GetById(x => x.Id == id, tracking: false);
            var result = await _speciesRepository.DeleteAsync(entity);
        }

        public IQueryable<SpeciesDTO> GetAll()
        {
            var entities = _speciesRepository.GetAll(x => x.IsDeleted == false, tracking: false);

            return entities.Select(x => _mapper.Map<SpeciesDTO>(x));
        }

        public SpeciesDTO GetById(int id)
        {
            var entity = _speciesRepository.GetById(x => x.Id == id && x.IsDeleted == false, tracking: false);

            return _mapper.Map<SpeciesDTO>(entity);
        }

        public async Task RecoverAsync(int id)
        {
            var entity = _speciesRepository.GetById(x => x.Id == id, tracking: false);
            var result = await _speciesRepository.RecoverAsync(entity);
        }

        public Task RemoveAsync(int id)
        {
            var entity = _speciesRepository.GetById(x => x.Id == id, tracking: false);
            return _speciesRepository.RemoveAsync(entity);
        }

        public async Task<SpeciesDTO> UpdateAsync(int id, UpdateSpeciesDTO dto)
        {
            var entity = _speciesRepository.GetById(x => x.Id == id, tracking: true);
            var mappedEntity = _mapper.Map(dto, entity);

            var result = await _speciesRepository.UpdateAsync(mappedEntity);

            return _mapper.Map<SpeciesDTO>(result);
        }
    }
}
