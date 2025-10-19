﻿using App.Core.Entities.Identity;
using App.DAL.Presistence;
using App.DAL.Repositories.Implementations;
using App.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DAL
{
    public static class DALDependencyInjection
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDatabase(configuration);
            services.AddIdentity();
            services.AddRepositories();

            return services;
        }

        private static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            // SQL Database 
            var connectionString = Environment.GetEnvironmentVariable("CloudConnection")
                                         ?? configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21)),
                mySqlOptions => mySqlOptions.EnableStringComparisonTranslations()));

            //services.AddDbContext<AppDbContext>(opt =>
            //{
            //    opt.UseSqlServer(connectionString);
            //});
        }

        private static void AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            // Repositories
            services.AddScoped<IPetRepository, PetRepository>();
            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<IBreedRepository, BreedRepository>();
            services.AddScoped<IReportRepository, ReportRepository>();
            services.AddScoped<ISpeciesRepository, SpeciesRepository>();
            services.AddScoped<IPetPhotoRepository, PetPhotoRepository>();
            services.AddScoped<IAdoptionRequestRepository, AdoptionRequestRepository>();
        }
    }
}
