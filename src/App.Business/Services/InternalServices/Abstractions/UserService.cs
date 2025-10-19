using App.Business.Services.ExternalServices.Interfaces;
using App.Business.Services.InternalServices.Interfaces;
using App.Core.DTOs.UserDTOs;
using App.Core.Entities.Identity;
using App.Core.Enums.UserEnums;
using App.Core.Exceptions.Commons;
using AutoMapper;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Services.InternalServices.Abstractions
{
    public class UserService : IUserService
    {
        private readonly IFileManagerService _fileManagerService;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<User> userManager, IMapper mapper, IFileManagerService fileManagerService)
        {
            _fileManagerService = fileManagerService;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<UserDTO> AddAsync(CreateUserDTO dto)
        {
            var user = _mapper.Map<User>(dto);

            user.UserName = dto.FullName.ToLower().Replace(" ", ".");
            user.EmailConfirmed = true;

            if (!string.IsNullOrWhiteSpace(dto.Password))
            {
                EnsureIdentitySucceeded(await _userManager.CreateAsync(user, dto.Password));
            }
            else
            {
                var defaultPassword = $"{dto.FullName.Replace(" ", ".")}dev123!@";
                EnsureIdentitySucceeded(await _userManager.CreateAsync(user, defaultPassword));
            }

            if (!string.IsNullOrWhiteSpace(dto.Role) && Enum.TryParse<EUserRole>(dto.Role, true, out var role))
            {
                EnsureIdentitySucceeded(await _userManager.AddToRoleAsync(user, role.ToString()));
            }
            else
            {
                EnsureIdentitySucceeded(await _userManager.AddToRoleAsync(user, EUserRole.User.ToString()));
            }

            return _mapper.Map<UserDTO>(user);
        }

        public async Task LockedOutAsync(string id)
        {
            var user = await CheckUserNotFoundAsync(id);

            user.LockoutEnd = DateTimeOffset.UtcNow.AddYears(100);
            EnsureIdentitySucceeded(await _userManager.UpdateAsync(user));
        }

        public async Task<IQueryable<UserDTO>> GetAllAsync()
        {
            var users = _userManager.Users.Where(x => x.UserName != "admin").ToList();

            var result = new List<UserDTO>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                result.Add(new UserDTO
                {
                    Id = user.Id,
                    Email = user.Email,
                    FullName = user.FullName,
                    PhoneNumber = user.PhoneNumber,
                    ImageUrl = user.ImageUrl,
                    Role = roles.FirstOrDefault() ?? string.Empty,
                    IsBanned = !user.LockoutEnabled
                });
            }

            return result.AsQueryable();
        }

        public async Task RecoverAsync(string id)
        {
            var user = await CheckUserNotFoundAsync(id);

            user.LockoutEnd = null;
            EnsureIdentitySucceeded(await _userManager.UpdateAsync(user));
        }

        public async Task RemoveAsync(string id)
        {
            EnsureIdentitySucceeded(await _userManager.DeleteAsync(await CheckUserNotFoundAsync(id)));
        }

        public async Task<UserDTO> UpdateAsync(string id, UpdateUserDTO dto)
        {
            var oldUser = await CheckUserNotFoundAsync(id);

            if (dto.Image is not null)
            {
                if (!string.IsNullOrWhiteSpace(oldUser.ImageUrl))
                    await _fileManagerService.RemoveFileAsync(oldUser.ImageUrl);
            }

            var updatedUser = _mapper.Map(dto, oldUser);
            var role = (await _userManager.GetRolesAsync(oldUser)).FirstOrDefault();

            if (role is not null && !role.Equals(EUserRole.Admin.ToString()))
                updatedUser.UserName = dto.FullName.ToLower().Replace(" ", ".");

            if (dto.Image is null)
            {
                if (!string.IsNullOrWhiteSpace(updatedUser.ImageUrl))
                    await _fileManagerService.RemoveFileAsync(updatedUser.ImageUrl);

                updatedUser.ImageUrl = null;
            }

            EnsureIdentitySucceeded(await _userManager.UpdateAsync(updatedUser));

            if (!string.IsNullOrWhiteSpace(dto.Role))
            {
                if (Enum.TryParse<EUserRole>(dto.Role, true, out var newRole))
                {
                    var currentRoles = await _userManager.GetRolesAsync(updatedUser);

                    if (currentRoles.Any())
                        EnsureIdentitySucceeded(await _userManager
                            .RemoveFromRolesAsync(updatedUser, currentRoles));

                    EnsureIdentitySucceeded(await _userManager.AddToRoleAsync(updatedUser, newRole.ToString()));
                }
                else
                {
                    var currentRoles = await _userManager.GetRolesAsync(updatedUser);
                    if (currentRoles.Any())
                        EnsureIdentitySucceeded(await _userManager
                            .RemoveFromRolesAsync(updatedUser, currentRoles));

                    EnsureIdentitySucceeded(await _userManager
                        .AddToRoleAsync(updatedUser, EUserRole.User.ToString()));
                }
            }

            return _mapper.Map<UserDTO>(updatedUser);
        }

        public async Task<UserDTO> UpdateAsync(string id, UpdateMainUserDTO dto)
        {
            var oldUser = await CheckUserNotFoundAsync(id);

            if (dto.Image is not null)
            {
                if (!string.IsNullOrWhiteSpace(oldUser.ImageUrl))
                    await _fileManagerService.RemoveFileAsync(oldUser.ImageUrl);
            }

            var updatedUser = _mapper.Map(dto, oldUser);
            var role = (await _userManager.GetRolesAsync(oldUser)).FirstOrDefault();

            if (role is not null && !role.Equals(EUserRole.Admin.ToString()))
                updatedUser.UserName = dto.FullName.ToLower().Replace(" ", ".");

            if (dto.Image is null)
            {
                if (!string.IsNullOrWhiteSpace(updatedUser.ImageUrl))
                    await _fileManagerService.RemoveFileAsync(updatedUser.ImageUrl);

                updatedUser.ImageUrl = null;
            }

            if (!string.IsNullOrWhiteSpace(dto.Password))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(updatedUser);
                EnsureIdentitySucceeded(await _userManager
                    .ResetPasswordAsync(updatedUser, token, dto.Password));
            }

            EnsureIdentitySucceeded(await _userManager.UpdateAsync(updatedUser));

            return _mapper.Map<UserDTO>(updatedUser);
        }

        // Support methods
        private async Task<User> CheckUserNotFoundAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user is null)
                throw new EntityNotFoundException($"Entity of type {typeof(User).Name.ToLower()} not found.");

            return user;
        }

        private void EnsureIdentitySucceeded(IdentityResult result)
        {
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"{errors}");
            }
        }
    }
}