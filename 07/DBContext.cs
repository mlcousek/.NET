using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace PNE07
{
    public class InstagramContext : DbContext
    {
        public InstagramContext() { Database.EnsureCreated(); }
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=mysmaIG.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Following)
                .WithMany(u => u.Followed)
                .UsingEntity(j => j.ToTable("UserFollowers"));

            modelBuilder.Entity<Post>()
                .HasMany(p => p.Comments)
                .WithOne(c => c.Post)
                .HasForeignKey(c => c.PostId);

            modelBuilder.Entity<Post>()
                .HasMany(p => p.Likes)
                .WithMany()
                .UsingEntity(j => j.ToTable("PostLikes"));

            modelBuilder.Entity<Comment>()
                .HasMany(c => c.Likes)
                .WithMany()
                .UsingEntity(j => j.ToTable("CommentLikes"));

        }
    }
}
