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
            modelBuilder.Entity<ShoeWidth>().HasKey(sw => sw.UniqueIdent);
            modelBuilder.Entity<ShoeWidth>().Property(sw => sw.UniqueIdent).HasColumnName("Id");
            modelBuilder.Entity<ShoeWidth>().Property(sw => sw.WidthName).HasColumnName("Name");
            modelBuilder.Entity<Shoe>().Property(sw => sw.WidthId).HasColumnName("FittingId");
            modelBuilder.Entity<Shoe>().HasOne(s => s.Width).WithMany(sw => sw.Products).HasForeignKey(s => s.WidthId).IsRequired();
        }
    }
}
