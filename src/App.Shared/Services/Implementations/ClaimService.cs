using App.Shared.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace App.Shared.Services.Implementations
{
    public class ClaimService : IClaimService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClaimService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void CreateAccessToken(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddHours(2)
            };

            _httpContextAccessor.HttpContext.Response.Cookies.Append("access_token", token, cookieOptions);
        }

        public void RemoveAccessToken()
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("access_token");
        }

        public string GetUserId()
        {
            return GetClaim(ClaimTypes.Name);
        }

        private string GetClaim(string key)
        {
            var result = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value
                ?? _httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault()?.Value;

            return result;
        }
    }
}
