using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyInstagram.Models;

namespace MyInstagram.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<MyInstagram.Models.Post> Post { get; set; } = default!;
        public DbSet<MyInstagram.Models.Comment> Comment { get; set; } = default!;
    }
}
