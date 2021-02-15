using Microsoft.EntityFrameworkCore;

namespace AdvancedApp.Models
{
    public class AdvancedContext : DbContext
    {
        public AdvancedContext(DbContextOptions<AdvancedContext> options)
            : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().Property(e => e.Id).UseHiLo();
            //modelBuilder.Entity<Employee>().HasIndex(e => e.SSN).IsUnique();

            modelBuilder.Entity<Employee>().HasAlternateKey(s => s.SSN);
            modelBuilder.Entity<SecondaryIdentity>()
                .HasOne(si => si.PrimaryIdentity)
                .WithOne(e => e.OtherIdentity)
                .HasPrincipalKey<Employee>(e => e.SSN)
                .HasForeignKey<SecondaryIdentity>(si => si.PrimarySSN);
        }
    }
}
