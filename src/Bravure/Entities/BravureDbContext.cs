using System;
using System.Threading;
using System.Threading.Tasks;
using Bravure.Entities.Abstractions;
using Bravure.Infrastructure.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bravure.Entities
{
    public class BravureDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public const string DEFAULT_SCHEMA = "dbo";

        private readonly IIdentityService _identityService;
        private readonly IDateTime _dateTime;

        public BravureDbContext(DbContextOptions<BravureDbContext> options, IIdentityService identityService, IDateTime dateTime)
            : base(options)
        {
            _identityService = identityService;
            _dateTime = dateTime;
        }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<MedicalAssistance> MedicalAssistances { get; set; }

        public DbSet<Medicine> Medicines { get; set; }

        public DbSet<MedicalExamination> MedicalExaminations { get; set; }

        public DbSet<CilinicExamination> CilinicExaminations { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<IAuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _identityService.CurrentUserId != null ? _identityService.CurrentUserId.Value : default(Guid);
                        entry.Entity.CreatedDate = _dateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedBy = _identityService.CurrentUserId != null ? _identityService.CurrentUserId.Value : default(Guid);
                        entry.Entity.UpdatedDate = _dateTime.UtcNow;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>().HasMany(p => p.Roles).WithOne().HasForeignKey(p => p.UserId).IsRequired();
        }
    }
}
