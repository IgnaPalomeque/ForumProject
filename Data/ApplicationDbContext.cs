using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ForumProject.Models;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Identity;

namespace ForumProject.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ForumProject.Models.ForumPost> ForumPost { get; set; } = default!;
        public DbSet<ForumProject.Models.PostComment> PostComment { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ForumPost>()
                .HasMany(e => e.Comments)
                .WithOne(e => e.ForumPost)
                .HasForeignKey(e => e.PostID)
                .IsRequired(false);
            modelBuilder.Entity<IdentityUserLogin<string>>().HasNoKey();
            modelBuilder.Entity<IdentityUserRole<string>> ().HasNoKey();
            modelBuilder.Entity<IdentityUserToken<string>>().HasNoKey();
        }
    }
}
