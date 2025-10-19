using App.Business.Services.InternalServices.Interfaces;
using App.Core.DTOs.BlogDTOs;
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
    public class BlogService : IBlogService
    {
        readonly IBlogRepository _blogRepository;
        readonly IMapper _mapper;

        public BlogService(IBlogRepository blogRepository, IMapper mapper)
        {
            _blogRepository = blogRepository;
            _mapper = mapper;
        }

        public async Task<BlogDTO> CreateAsync(CreateBlogDTO dto)
        {
            var entity = _mapper.Map<Blog>(dto);
            var result = await _blogRepository.AddAsync(entity);

            return _mapper.Map<BlogDTO>(result);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = _blogRepository.GetById(x => x.Id == id, tracking: false);
            var result = await _blogRepository.DeleteAsync(entity);
        }

        public IQueryable<BlogDTO> GetAll()
        {
            var entities = _blogRepository.GetAll(x => x.IsDeleted == false, tracking: false);

            return entities.Select(x => _mapper.Map<BlogDTO>(x));
        }

        public BlogDTO GetById(int id)
        {
            var entity = _blogRepository.GetById(x => x.Id == id && x.IsDeleted == false, tracking: false);

            return _mapper.Map<BlogDTO>(entity);
        }

        public async Task RecoverAsync(int id)
        {
            var entity = _blogRepository.GetById(x => x.Id == id, tracking: false);
            var result = await _blogRepository.RecoverAsync(entity);
        }

        public Task RemoveAsync(int id)
        {
            var entity = _blogRepository.GetById(x => x.Id == id, tracking: false);
            return _blogRepository.RemoveAsync(entity);
        }

        public async Task<BlogDTO> UpdateAsync(int id, UpdateBlogDTO dto)
        {
            var entity = _blogRepository.GetById(x => x.Id == id, tracking: true);
            var mappedEntity = _mapper.Map(dto, entity);

            var result = await _blogRepository.UpdateAsync(mappedEntity);

            return _mapper.Map<BlogDTO>(result);
        }
    }
}
