using CleanNow.Infrastructured.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanNow.Infrastructured.Identity.Context
{
    public class IdentityContext:IdentityDbContext<ApplicationUser>
    {
        public IdentityContext(DbContextOptions<IdentityContext> context):base(context)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("Identity");
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable(name: "Users");
                entity.Property(u => u.UserName).IsRequired(false);

            });
            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name:"Roles");
            });
            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable(name: "UserRoles");
            });
            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable(name: "UsersLogin");
            });
        }
    }
}
