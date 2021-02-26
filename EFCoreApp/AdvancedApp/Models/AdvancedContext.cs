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
            //Database.AutoTransactionsEnabled = false;
        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<SecondaryIdentity> SecondaryIdentities { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Computer> Computers { get; set; }

        public DbSet<ComputerDetail> ComputerDetails { get; set; }

        public DbSet<Person1> Person1s { get; set; }

        public DbSet<Person2> Person2s { get; set; }

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
                .HasDefaultValue(new DateTime(2020, 1, 1));
            
            modelBuilder.Entity<Employee>().Ignore(e => e.RowVersion);
            //.Property(e => e.RowVersion).IsRowVersion();

            modelBuilder.HasSequence<int>("ReferenceSequence")
                .StartsAt(100)
                .IncrementsBy(2);

            //modelBuilder.Entity<Employee>()
            //    .Property(e => e.GeneratedValue)
            //    .HasDefaultValueSql("'REFERENCE_' + CONVERT(varchar, NEXT VALUE FOR ReferenceSequence)");

            //modelBuilder.Entity<Employee>()
            //    .Property(e => e.GeneratedValue)
            //    .HasComputedColumnSql(@"SUBSTRING(FirstName, 1, 1) + FamilyName PERSISTED");

            modelBuilder.Entity<Employee>()
                .Property(e => e.GeneratedValue)
                .ValueGeneratedOnAddOrUpdate();

            modelBuilder.Entity<Employee>()
                .Property(e => e.NChar)
                .HasColumnType("NCHAR(10)");

            modelBuilder.Entity<SecondaryIdentity>().ToTable("SecondaryIdentity");
            modelBuilder.Entity<SecondaryIdentity>().Property(e => e.Name).HasMaxLength(100);
            modelBuilder.Entity<SecondaryIdentity>()
                .HasOne(si => si.PrimaryIdentity)
                .WithOne(e => e.OtherIdentity)
                .HasPrincipalKey<Employee>(e => new { e.SSN, e.FirstName, e.FamilyName })
                .HasForeignKey<SecondaryIdentity>(si => new { si.PrimarySSN, si.PrimaryFirstName, si.PrimaryFamilyName })
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<Category>().ToTable("Category");
        }
    }
}
