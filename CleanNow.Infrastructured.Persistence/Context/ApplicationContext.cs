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
        public DbSet<Category> categories { get; set; }
        public DbSet<DetailsDomicile> detailsDomiciles { get; set; }
        public DbSet<Message> message { get; set; }
        public DbSet<ProfileUser> profileUsers { get; set; }
        public DbSet<Ranking> rankings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
