using App.Business.Services.InternalServices.Interfaces;
using App.Core.DTOs.PetDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        readonly IPetService _petService;

        public PetController(IPetService petService)
        {
            _petService = petService;
        }

        [HttpGet]
        public IActionResult GetAllAsync()
        {
            var result = _petService.GetAll();

            return Ok(result);
        }

        [HttpPatch("delete/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _petService.DeleteAsync(id);
            return Ok();
        }

        [HttpPatch("recover/{id}")]
        public async Task<IActionResult> RecoverAsync(int id)
        {
            await _petService.RecoverAsync(id);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAsync(int id)
        {
            await _petService.RemoveAsync(id);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreatePetDTO dto)
        {
            var result = await _petService.CreateAsync(dto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] UpdatePetDTO dto)
        {
            var result = await _petService.UpdateAsync(id, dto);
            return Ok(result);
        }
    }
}
