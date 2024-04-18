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
        public DbSet<Like> likes { get; set; }
        public DbSet<Opinion> opinions { get; set; }
        public DbSet<Hiring> hirings { get; set; }
        public DbSet<Solicit> solicits { get; set; }
        public DbSet<Assistant> assistants { get; set; }
        public DbSet<Location> locations { get; set; }

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

            #region DetailsDomicile
            modelBuilder.Entity<DetailsDomicile>(options =>
            {
                options.HasKey(e=>e.Id);
                options.Property(e=>e.Addresses).IsRequired().HasMaxLength(200);
                options.Property(e => e.ImageDomicile);
                options.Property(e => e.TypeClean).IsRequired().HasMaxLength(150);
                options.Property(e => e.Apt).IsRequired(false).HasMaxLength(50);
                options.Property(e => e.Time).IsRequired().HasMaxLength(100);
                options.Property(e => e.UserId).IsRequired();
            });
            #endregion
            #region Likes
            modelBuilder.Entity<Like>(options =>
            {
                options.HasKey(e => e.Id);
                options.Property(e=>e.isLike).IsRequired();
                options.Property(e=>e.UserId).IsRequired();
            });
            modelBuilder.Entity<Like>()
                .HasOne<Assistant>(a => a.assistant)
                .WithMany(a => a.likes)
                .HasForeignKey(l => l.AssistantId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
            #region Opinions
            modelBuilder.Entity<Opinion>(o =>
            {
                o.HasKey(o => o.Id);
                o.Property(o=>o.Start).IsRequired().HasMaxLength(6);
                o.Property(o=>o.Description).IsRequired(false);
                o.Property(o=>o.ValuerName).IsRequired().HasMaxLength(100);
            });
            modelBuilder.Entity<Opinion>()
                .HasOne<Assistant>(o => o.assistant)
                .WithMany(a => a.opinions)
                .HasForeignKey(o => o.AssistantId)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion
            #region Hiring 
            modelBuilder.Entity<Hiring>(h =>
            {
                h.HasKey(h => h.Id);
                h.Property(h=>h.PayType).IsRequired().HasMaxLength(150);
                h.Property(h => h.Meter).IsRequired().HasMaxLength(200);
                h.Property(h => h.Total).IsRequired();
                h.Property(h => h.UserId).IsRequired();

            });

            modelBuilder.Entity<Hiring>()
                .HasOne<Assistant>(h => h.assistant)
                .WithMany(a => a.hirings)
                .HasForeignKey(f => f.AssistentId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Hiring>()
                .HasOne<Location>(h => h.location)
                .WithMany(l => l.hirings)
                .HasForeignKey(l => l.LocationId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
            #region Solicit 
            modelBuilder.Entity<Solicit>(s =>
            {
                s.HasKey(s => s.Id);
                s.Property(s => s.Status).IsRequired();
                s.Property(s=>s.SelectedDate).IsRequired();
            });
            modelBuilder.Entity<Solicit>()
                .HasOne<Hiring>(h => h.hiring)
                .WithMany(s => s.solicit)
                .HasForeignKey(s => s.HiringId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
            #region Location 
            modelBuilder.Entity<Location>(l =>
            {
                l.HasKey(l => l.Id);
                l.Property(l => l.Street).IsRequired();
                l.Property(l => l.Apt).IsRequired().HasMaxLength(100);
                l.Property(l => l.Address).IsRequired().HasMaxLength(200);
                l.Property(l => l.City).IsRequired().HasMaxLength(100);
                l.Property(l => l.Doorbell).IsRequired().HasMaxLength(100);
            });
            #endregion
            #region Assistant 
            modelBuilder.Entity<Assistant>(a =>
            {
                a.HasKey(k => k.Id);
                a.Property(a => a.Name).IsRequired().HasMaxLength(100);
                a.Property(a => a.LastName).IsRequired().HasMaxLength(100);
                a.Property(a => a.AboutMe).IsRequired().HasMaxLength(200);
                a.Property(a => a.Availability).IsRequired();
                a.Property(a => a.Age).IsRequired();
                a.Property(a => a.Experience).IsRequired().HasMaxLength(2);
                a.Property(a => a.IsVerify).IsRequired();
                a.Property(a => a.Price).IsRequired();
                a.Property(a => a.Location).IsRequired();
                a.Property(a => a.Image);

            });
            #endregion
        }
    }
}
