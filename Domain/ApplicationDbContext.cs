using Microsoft.EntityFrameworkCore;
using PostOpinion.Domain.Entities;

namespace PostOpinion.Domain
{   
    public class ApplicationDbContext:DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>()
                .HasMany(p => p.Comment)
                .WithOne(c => c.Post)
                .HasForeignKey(fk=>fk.PostID);

            modelBuilder.Entity<Comment>()
                .HasOne(cv => cv.Post)
                .WithMany(cv => cv.Comment)
                .HasForeignKey(cv=>cv.PostID); 
        }
    }
}
