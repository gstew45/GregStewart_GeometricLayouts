namespace GeometricLayouts.Core.Models
{
    using Microsoft.EntityFrameworkCore;

    public class TriangleContext : DbContext
    {
        public TriangleContext(DbContextOptions<TriangleContext> options)
            : base(options)
        {
        }

        public DbSet<Triangle> Triangles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Triangle>()
                .HasKey(t => new { t.Row, t.Column });

            modelBuilder.Entity<Triangle>()
                .Property(t => t._Points);
        }
    }
}
