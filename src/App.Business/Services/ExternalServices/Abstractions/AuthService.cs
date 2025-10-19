using App.Business.Helpers;
using App.Business.Services.ExternalServices.Interfaces;
using App.Core.DTOs.AuthDTOs;
using App.Core.Entities.Identity;
using App.Core.Exceptions.Commons;
using App.Shared.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Services.ExternalServices.Abstractions
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public AuthService(
            UserManager<User> userManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }


        public async Task<LoginResponseDTO> LoginAsync(LoginDTO dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.UsernameOrEmail);

            if (user == null)
                user = await _userManager.FindByNameAsync(dto.UsernameOrEmail);

            if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
                throw new UnauthorizedAccessException("Email or password is incorrect.");

            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault() ?? "RoleError";

            var token = JwtGenerator.GenerateToken(user, role, _configuration);

            return new LoginResponseDTO
            {
                Token = token
            };
        }

        public async Task<User> CheckUserNotFoundAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user is null)
                throw new EntityNotFoundException($"Entity of type {typeof(User).Name.ToLower()} not found.");

            return user;
        }
    }
}
