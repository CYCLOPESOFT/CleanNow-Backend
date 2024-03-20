using CleanNow.Core.Domain.Common;
using CleanNow.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanNow.Infrastructured.Persistence.Context
{
    public class ApplicationContext:DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> contextOptions):base(contextOptions)
        {
        
        }
        public DbSet<DetailsDomicile> detailsDomiciles { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State) 
                { 
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                    break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedDate = DateTime.Now; 
                        break;
                }

            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DetailsDomicile>(options =>
            {
                options.HasKey(e=>e.Id);
                options.Property(e=>e.Addresses).IsRequired().HasMaxLength(200);
                options.Property(e => e.ImageDomicile).IsRequired();
                options.Property(e => e.TypeClean).IsRequired().HasMaxLength(150);
                options.Property(e => e.Apt).IsRequired(false).HasMaxLength(50);
                options.Property(e => e.Time).IsRequired().HasMaxLength(100);
            });
        }
    }
}
