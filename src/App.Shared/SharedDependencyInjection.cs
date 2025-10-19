using App.Shared.Services.Implementations;
using App.Shared.Services.Interfaces;
using CloudinaryDotNet;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Shared
{
    public static class SharedDependencyInjection
    {
        public static IServiceCollection AddShared(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddServices();
            services.AddCloud(configuration);
            services.AddHttpContextAccessor();

            return services;
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IClaimService, ClaimService>();
            services.AddScoped<ICloudService, CloudService>();
            services.AddScoped<IEmailService, EmailService>();
        }

        private static void AddCloud(this IServiceCollection services, IConfiguration configuration)
        {
            var cloudName = configuration["Cloudinary:CloudName"];
            var cloudApiKey = configuration["Cloudinary:ApiKey"];
            var cloudApiSecret = configuration["Cloudinary:ApiSecret"];

            var account = new Account(cloudName, cloudApiKey, cloudApiSecret);
            var cloudinary = new Cloudinary(account);

            services.AddSingleton(cloudinary);
        }
    }
}
