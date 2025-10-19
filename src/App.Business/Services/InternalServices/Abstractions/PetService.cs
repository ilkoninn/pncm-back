using App.Business.Services.ExternalServices.Interfaces;
using App.Business.Services.InternalServices.Interfaces;
using App.Core.DTOs.PetDTOs;
using App.Core.Entities;
using App.Core.Exceptions.PetExceptions;
using App.DAL.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Services.InternalServices.Abstractions
{
    public class PetService : IPetService
    {
        readonly IPetPhotoRepository _petPhotoRepository;
        readonly IFileManagerService _fileManagerService;
        readonly IPetRepository _petRepository;
        readonly IMapper _mapper;

        public PetService(IPetRepository petRepository, IMapper mapper, IPetPhotoRepository petPhotoRepository, IFileManagerService fileManagerService)
        {
            _fileManagerService = fileManagerService;
            _petPhotoRepository = petPhotoRepository;
            _petRepository = petRepository;
            _mapper = mapper;
        }

        public async Task<PetDTO> CreateAsync(CreatePetDTO dto)
        {
            var entity = _mapper.Map<Pet>(dto);
            var result = await _petRepository.AddAsync(entity);

            foreach (var image in dto.Images)
            {
                var imageUrl = await _fileManagerService.UploadFileAsync(image);

                var petImage = new PetPhoto
                {
                    ImageUrl = imageUrl,
                    PetId = result.Id
                };

                await _petPhotoRepository.AddAsync(petImage);
            }

            return _mapper.Map<PetDTO>(result);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = _petRepository.GetById(x => x.Id == id, tracking: false);
            var result = await _petRepository.DeleteAsync(entity);
        }

        public IQueryable<PetDTO> GetAll()
        {
            var entities = _petRepository.GetAll(x => x.IsDeleted == false, tracking: false);

            return entities.Select(x => _mapper.Map<PetDTO>(x));
        }

        public PetDTO GetById(int id)
        {
            var entity = _petRepository.GetById(x => x.Id == id && x.IsDeleted == false, tracking: false);

            return _mapper.Map<PetDTO>(entity);
        }

        public async Task RecoverAsync(int id)
        {
            var entity = _petRepository.GetById(x => x.Id == id, tracking: false);
            var result = await _petRepository.RecoverAsync(entity);
        }

        public Task RemoveAsync(int id)
        {
            var entity = _petRepository.GetById(x => x.Id == id, tracking: false);
            return _petRepository.RemoveAsync(entity);
        }

        public async Task<PetDTO> UpdateAsync(int id, UpdatePetDTO dto)
        {
            var entity = _petRepository.GetById(x => x.Id == id, tracking: true);

            _mapper.Map(dto, entity);

            if (dto.Images == null || !dto.Images.Any())
                throw new AtLeastOnePetImageException();

            await UploadPetPhotosAsync(entity.Photos, dto.Images, entity.Id);

            var updatedEntity = await _petRepository.UpdateAsync(entity);

            return _mapper.Map<PetDTO>(updatedEntity);
        }

        // Supporting Methods

        private async Task UploadPetPhotosAsync(
            ICollection<PetPhoto> photos, 
            ICollection<IFormFile> images, 
            int entityId)
        {
            if (images.Count() > 0)
            {

                if (photos != null && photos.Any())
                {
                    foreach (var photo in photos)
                    {
                        await _fileManagerService.RemoveFileAsync(photo.ImageUrl);
                        await _petPhotoRepository.DeleteAsync(photo);
                    }
                }

                foreach (var image in images)
                {
                    var imageUrl = await _fileManagerService.UploadFileAsync(image);

                    var petImage = new PetPhoto
                    {
                        ImageUrl = imageUrl,
                        PetId = entityId
                    };

                    await _petPhotoRepository.AddAsync(petImage);
                }
            }
        }
    }
}
