using App.Business.Services.InternalServices.Interfaces;
using App.Core.DTOs.BlogDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet]
        public IActionResult GetAllAsync()
        {
            var result = _blogService.GetAll();

            return Ok(result);
        }

        [HttpPatch("delete/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _blogService.DeleteAsync(id);
            return Ok();
        }

        [HttpPatch("recover/{id}")]
        public async Task<IActionResult> RecoverAsync(int id)
        {
            await _blogService.RecoverAsync(id);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAsync(int id)
        {
            await _blogService.RemoveAsync(id);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateBlogDTO dto)
        {
            var result = await _blogService.CreateAsync(dto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] UpdateBlogDTO dto)
        {
            var result = await _blogService.UpdateAsync(id, dto);
            return Ok(result);
        }
    }
}
