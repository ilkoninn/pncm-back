using App.Business.Services.InternalServices.Interfaces;
using App.Core.DTOs.ReportDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        public IActionResult GetAllAsync()
        {
            var result = _reportService.GetAll();

            return Ok(result);
        }

        [HttpPatch("delete/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _reportService.DeleteAsync(id);
            return Ok();
        }

        [HttpPatch("recover/{id}")]
        public async Task<IActionResult> RecoverAsync(int id)
        {
            await _reportService.RecoverAsync(id);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAsync(int id)
        {
            await _reportService.RemoveAsync(id);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateReportDTO dto)
        {
            var result = await _reportService.CreateAsync(dto);
            return Ok(result);
        }
    }
}
