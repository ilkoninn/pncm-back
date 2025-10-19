using App.Business.Services.InternalServices.Interfaces;
using App.Core.DTOs.SpeciesDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpeciesController : ControllerBase
    {
        readonly ISpeciesService _speciesService;

        public SpeciesController(ISpeciesService speciesService)
        {
            _speciesService = speciesService;
        }

        [HttpGet]
        public IActionResult GetAllAsync()
        {
            var result = _speciesService.GetAll();

            return Ok(result);
        }

        [HttpPatch("delete/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _speciesService.DeleteAsync(id);
            return Ok();
        }

        [HttpPatch("recover/{id}")]
        public async Task<IActionResult> RecoverAsync(int id)
        {
            await _speciesService.RecoverAsync(id);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAsync(int id)
        {
            await _speciesService.RemoveAsync(id);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateSpeciesDTO dto)
        {
            var result = await _speciesService.CreateAsync(dto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] UpdateSpeciesDTO dto)
        {
            var result = await _speciesService.UpdateAsync(id, dto);
            return Ok(result);
        }
    }
}
