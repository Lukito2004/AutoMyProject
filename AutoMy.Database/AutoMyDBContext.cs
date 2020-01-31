using AutoMy.DomainModels;
using AutoMy.DomainModels.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoMy.Database
{
    public class AutoMyDBContext : IdentityDbContext<Account>
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Report> Reports { get; set; }
        public AutoMyDBContext(DbContextOptions<AutoMyDBContext> options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Post>();
            modelBuilder.Entity<Category>();
            modelBuilder.Entity<Report>();

            modelBuilder.Entity<Account>()
                .HasMany(g => g.Posts)
                .WithOne();
            modelBuilder.ApplyConfiguration<Post>(new PostConfiguration());
            modelBuilder.ApplyConfiguration<Category>(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration<Report>(new ReportConfiguration());
        }
    }
}
