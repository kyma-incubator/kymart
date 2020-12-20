using Microsoft.EntityFrameworkCore;

namespace artbackend.Database
{

    public class LikesContext : DbContext
    {
        public DbSet<Like> Likes { get; set; }

        public LikesContext() : base()
        {
        }

        public LikesContext(DbContextOptions<LikesContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Like>()
                .HasKey(like => new { like.ImageId, like.ImageVersion });
        }
    }
}