using App.Business.Services.InternalServices.Interfaces;
using App.Business.Validators.UserValidators;
using App.Core.DTOs.UserDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, CEO, Manager, COFounder")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _userService.GetAllAsync();

            return Ok(result);
        }

        [HttpPatch("ban/{id}")]
        [Authorize(Roles = "Admin, CEO, Manager")]
        public async Task<IActionResult> LockedOutAsync(string id)
        {
            await _userService.LockedOutAsync(id);
            return Ok();
        }

        [HttpPatch("recover/{id}")]
        [Authorize(Roles = "Admin, CEO, Manager")]
        public async Task<IActionResult> RecoverAsync(string id)
        {
            await _userService.RecoverAsync(id);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, CEO, Manager")]
        public async Task<IActionResult> RemoveAsync(string id)
        {
            await _userService.RemoveAsync(id);
            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = "Admin, CEO, Manager")]
        public async Task<IActionResult> CreateAsync([FromForm] CreateUserDTO dto)
        {
            var validationResult = await new CreateUserDTOValidator().ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var result = await _userService.AddAsync(dto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, CEO, Manager")]
        public async Task<IActionResult> UpdateAsync([FromRoute] string id, [FromForm] UpdateUserDTO dto)
        {
            var validationResult = await new UpdateUserDTOValidator().ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var result = await _userService.UpdateAsync(id, dto);
            return Ok(result);
        }

        [HttpPut("main/{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] string id, [FromForm] UpdateMainUserDTO dto)
        {
            var validationResult = await new UpdateMainUserDTOValidator().ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var result = await _userService.UpdateAsync(id, dto);
            return Ok(result);
        }
    }
}