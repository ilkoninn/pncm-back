using App.Business.Services.InternalServices.Interfaces;
using App.Core.DTOs.BreedDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BreedController : ControllerBase
    {
        readonly IBreedService _breedService;

        public BreedController(IBreedService breedService)
        {
            _breedService = breedService;
        }

        [HttpGet]
        public IActionResult GetAllAsync()
        {
            var result = _breedService.GetAll();

            return Ok(result);
        }

        [HttpPatch("delete/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _breedService.DeleteAsync(id);
            return Ok();
        }

        [HttpPatch("recover/{id}")]
        public async Task<IActionResult> RecoverAsync(int id)
        {
            await _breedService.RecoverAsync(id);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAsync(int id)
        {
            await _breedService.RemoveAsync(id);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateBreedDTO dto)
        {
            var result = await _breedService.CreateAsync(dto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] UpdateBreedDTO dto)
        {
            var result = await _breedService.UpdateAsync(id, dto);
            return Ok(result);
        }
    }
}
