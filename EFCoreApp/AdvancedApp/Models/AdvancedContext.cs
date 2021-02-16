using Microsoft.EntityFrameworkCore;
using System;

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
            modelBuilder.Entity<Employee>()
                .Property(e => e.Salary)
                .HasColumnType("decimal(8,2)")
                .HasField("databaseSalary")
                .UsePropertyAccessMode(PropertyAccessMode.Field);
                //.IsConcurrencyToken();
            modelBuilder.Entity<Employee>()
                .Property<DateTime>("LastUpdated")
                .HasDefaultValue(DateTime.Now);
            modelBuilder.Entity<Employee>().Property(e => e.RowVersion).IsRowVersion();

            modelBuilder.Entity<SecondaryIdentity>().Property(e => e.Name).HasMaxLength(100);
            modelBuilder.Entity<SecondaryIdentity>()
                .HasOne(si => si.PrimaryIdentity)
                .WithOne(e => e.OtherIdentity)
                .HasPrincipalKey<Employee>(e => new { e.SSN, e.FirstName, e.FamilyName })
                .HasForeignKey<SecondaryIdentity>(si => new { si.PrimarySSN, si.PrimaryFirstName, si.PrimaryFamilyName });
        }
    }
}
