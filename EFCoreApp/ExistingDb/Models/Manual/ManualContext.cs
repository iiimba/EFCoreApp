using Microsoft.EntityFrameworkCore;

namespace ExistingDb.Models.Manual
{
    public class ManualContext : DbContext
    {
        public ManualContext(DbContextOptions<ManualContext> options)
            : base(options)
        {

        }

        public DbSet<Shoe> Shoe { get; set; }

        public DbSet<Style> ShoeStyles { get; set; }

        public DbSet<ShoeWidth> ShoeWidths { get; set; }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShoeWidth>().ToTable("Fittings");
            modelBuilder.Entity<ShoeWidth>().HasKey(t => t.UniqueIdent);
            modelBuilder.Entity<ShoeWidth>().Property(t => t.UniqueIdent).HasColumnName("Id");
            modelBuilder.Entity<ShoeWidth>().Property(t => t.WidthName).HasColumnName("Name");
            modelBuilder.Entity<Shoe>().Property(t => t.WidthId).HasColumnName("FittingId");
            modelBuilder.Entity<Shoe>().HasOne(t => t.Width).WithMany(w => w.Products).HasForeignKey(s => s.WidthId).IsRequired();
        }
    }
}
