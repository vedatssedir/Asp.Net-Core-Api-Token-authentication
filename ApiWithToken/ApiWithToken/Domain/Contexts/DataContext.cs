
using ApiWithToken.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiWithToken.Domain.Contexts
{
    public  class DataContext : DbContext
    {
        public DataContext()
        {
            
        }
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Turkish_CI_AS");

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.Category).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Price).HasColumnType("money");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(8);

                entity.Property(e => e.RefreshToken).HasMaxLength(500);

                entity.Property(e => e.RefreshTokenEndDate).HasColumnType("datetime");

                entity.Property(e => e.Surname).HasMaxLength(50);
            });

        }

    }
}
