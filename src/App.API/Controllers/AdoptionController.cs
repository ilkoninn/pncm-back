using App.Business.Services.InternalServices.Interfaces;
using App.Core.DTOs.AdoptionRequestDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdoptionController : ControllerBase
    {
        private readonly IAdoptionRequestService _adoptionRequestService;

        public AdoptionController(IAdoptionRequestService adoptionRequestService)
        {
            _adoptionRequestService = adoptionRequestService;
        }

        [HttpGet]
        public IActionResult GetAllAsync()
        {
            var result = _adoptionRequestService.GetAll();

            return Ok(result);
        }

        [HttpPatch("delete/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _adoptionRequestService.DeleteAsync(id);
            return Ok();
        }

        [HttpPatch("recover/{id}")]
        public async Task<IActionResult> RecoverAsync(int id)
        {
            await _adoptionRequestService.RecoverAsync(id);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAsync(int id)
        {
            await _adoptionRequestService.RemoveAsync(id);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateAdoptionRequestDTO dto)
        {
            await _adoptionRequestService.CreateAsync(dto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] ModifyAdoptionRequestStatusDTO dto)
        {
            await _adoptionRequestService.UpdateAdoptionRequestStatusAsync(id, dto);
            return Ok();
        }
    }
}
