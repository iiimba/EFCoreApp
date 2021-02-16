using Microsoft.EntityFrameworkCore;

namespace AdvancedApp.Models
{
    public class AdvancedContext : DbContext
    {
        public AdvancedContext(DbContextOptions<AdvancedContext> options)
            : base(options)
        {
            //ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<SecondaryIdentity> SecondaryIdentity { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasQueryFilter(e => !e.SoftDeleted);

            modelBuilder.Entity<Employee>().Ignore(e => e.Id);
            modelBuilder.Entity<Employee>().HasKey(e => new { e.SSN, e.FirstName, e.FamilyName });
            modelBuilder.Entity<Employee>().Property(e => e.SoftDeleted).HasDefaultValue(false);

            modelBuilder.Entity<SecondaryIdentity>()
                .HasOne(si => si.PrimaryIdentity)
                .WithOne(e => e.OtherIdentity)
                .HasPrincipalKey<Employee>(e => new { e.SSN, e.FirstName, e.FamilyName })
                .HasForeignKey<SecondaryIdentity>(si => new { si.PrimarySSN, si.PrimaryFirstName, si.PrimaryFamilyName });
        }
    }
}
