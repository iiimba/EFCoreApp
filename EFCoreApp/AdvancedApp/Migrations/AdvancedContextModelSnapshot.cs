﻿// <auto-generated />
using AdvancedApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AdvancedApp.Migrations
{
    [DbContext(typeof(AdvancedContext))]
    partial class AdvancedContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.HasSequence("EntityFrameworkHiLoSequence")
                .IncrementsBy(10);

            modelBuilder.Entity("AdvancedApp.Models.Employee", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:HiLoSequenceName", "EntityFrameworkHiLoSequence")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.SequenceHiLo);

                    b.Property<string>("FamilyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SSN")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Salary")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("AdvancedApp.Models.SecondaryIdentity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("InActiveUse")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PrimarySSN")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("PrimarySSN")
                        .IsUnique()
                        .HasFilter("[PrimarySSN] IS NOT NULL");

                    b.ToTable("SecondaryIdentity");
                });

            modelBuilder.Entity("AdvancedApp.Models.SecondaryIdentity", b =>
                {
                    b.HasOne("AdvancedApp.Models.Employee", "PrimaryIdentity")
                        .WithOne("OtherIdentity")
                        .HasForeignKey("AdvancedApp.Models.SecondaryIdentity", "PrimarySSN")
                        .HasPrincipalKey("AdvancedApp.Models.Employee", "SSN");

                    b.Navigation("PrimaryIdentity");
                });

            modelBuilder.Entity("AdvancedApp.Models.Employee", b =>
                {
                    b.Navigation("OtherIdentity");
                });
#pragma warning restore 612, 618
        }
    }
}
