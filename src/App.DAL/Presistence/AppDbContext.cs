using App.Core.Entities.Commons;
using App.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using App.Core.Entities;
using App.Shared.Services.Interfaces;

namespace App.DAL.Presistence
{
    public class AppDbContext : IdentityDbContext<User>
    {
        private readonly IClaimService? _claimService;

        public AppDbContext(DbContextOptions<AppDbContext> options, 
            IClaimService? claimService = null) : base(options)
        {
            _claimService = claimService;
        }

        // Models Here !!
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Breed> Breeds { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Species> Species { get; set; }
        public DbSet<PetPhoto> PetPhotos { get; set; }
        public DbSet<AdoptionRequest> AdoptionRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public new async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            var userId = _claimService?.GetUserId() ?? "ByServer";

            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedById = userId;
                        entry.Entity.CreatedOn = DateTime.UtcNow;

                        entry.Entity.LastModifiedById = userId;
                        entry.Entity.LastModifiedOn = DateTime.UtcNow;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedById = userId;
                        entry.Entity.LastModifiedOn = DateTime.UtcNow;
                        break;

                    case EntityState.Deleted:
                        entry.Entity.DeletedById = userId;
                        entry.Entity.DeletedOn = DateTime.UtcNow;
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
