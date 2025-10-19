using App.Core.DTOs.Commons;
using App.Business.Helpers;
using App.Business.Services.ExternalServices.Abstractions;
using App.Business.Services.ExternalServices.Interfaces;
using App.Business.Services.InternalServices.Abstractions;
using App.Business.Services.InternalServices.Interfaces;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FluentValidation;

namespace App.Business
{
    public static class BusinessDependencyInjection
    {
        public static IServiceCollection AddBusiness(this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddServices();
            services.RegisterAutoMapper();
            services.AddControllers(options =>
            {
                options.Conventions.Add(new PluralizedRouteConvention());
                options.ModelValidatorProviders.Clear();
            })
           .AddFluentValidation(fv => fv
           .RegisterValidatorsFromAssemblyContaining<AbstractValidator<BaseEntityDTO>>())
           .AddJsonOptions(options =>
           {
               options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
           });

            return services;
        }

        private static void AddServices(this IServiceCollection services)
        {
            // External Services 
            services.AddScoped<IFileManagerService, FileManagerService>();
            services.AddScoped<IAuthService, AuthService>();

            // Internal Services
            services.AddScoped<IPetService, PetService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<IBreedService, BreedService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<ISpeciesService, SpeciesService>();
            services.AddScoped<IAdoptionRequestService, AdoptionRequestService>();
        }

        private static void RegisterAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(BusinessDependencyInjection));
        }
    }
}
