using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TypicalSchoolWebsite_API.Entities
{
    public class TSW_DbContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostCategory> PostCategories { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }


        public TSW_DbContext(DbContextOptions options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Post
            modelBuilder.Entity<Post>()
                .Property(p => p.Title)
                    .IsRequired();
            modelBuilder.Entity<Post>()
                .Property(p => p.TextContent)
                    .IsRequired();

            //PostCategory
            modelBuilder.Entity<PostCategory>()
                .Property(p => p.Name)
                    .IsRequired();
            modelBuilder.Entity<PostCategory>()
                .Property(p => p.Description)
                    .IsRequired();

            //Role
            modelBuilder.Entity<Role>()
                .Property(p => p.RoleName)
                    .IsRequired();
            modelBuilder.Entity<Role>()
                .Property(p => p.Description)
                    .IsRequired();
            modelBuilder.Entity<Role>()
                .Property(p => p.AccessLevel)
                    .IsRequired();

            modelBuilder.Entity<User>()
                .Property(p => p.UserName)
                    .IsRequired();
            modelBuilder.Entity<User>()
                .Property(p => p.Email)
                    .IsRequired();
        }

    }
}
