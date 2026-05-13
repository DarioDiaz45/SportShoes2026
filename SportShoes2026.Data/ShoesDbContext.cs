using Microsoft.EntityFrameworkCore;
using SportShoes2026.Entities;


namespace SportShoes2026.Data
{
    public class ShoesDbContext:DbContext
    {
        public DbSet<Brand> Brands { get; set; }

        public DbSet<Sport> Sports { get; set; }

        public DbSet<SiZe> SiZes { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<SportShoe> SportShoes { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.; Initial Catalog=ShoesDb2026; Trusted_Connection=true; TrustServerCertificate=true;");

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SportShoe>().ToTable("Shoes");
            modelBuilder.Entity<SportShoe>().HasKey(s => s.ShoeId);
            modelBuilder.Entity<SportShoe>(entity =>
            {
                entity.HasOne(d => d.Size)
                      .WithMany()
                      .HasForeignKey(d => d.SizeId);
            });

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShoesDbContext).Assembly);

            foreach (var fk in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
