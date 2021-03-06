﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ExistingDb.Models.Scaffold
{
    public partial class ScaffoldContext : DbContext
    {
        public ScaffoldContext()
        {
        }

        public ScaffoldContext(DbContextOptions<ScaffoldContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Colors> Colors { get; set; }
        public virtual DbSet<Fittings> Fittings { get; set; }
        public virtual DbSet<SalesCampaigns> SalesCampaigns { get; set; }
        public virtual DbSet<ShoeCategoryJunction> ShoeCategoryJunction { get; set; }
        public virtual DbSet<Shoes> Shoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categories>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Colors>(entity =>
            {
                entity.Property(e => e.HighlightColor).IsRequired();

                entity.Property(e => e.MainColor).IsRequired();

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Fittings>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<SalesCampaigns>(entity =>
            {
                entity.HasIndex(e => e.Shoeld)
                    .IsUnique();

                entity.Property(e => e.LaunchDate).HasColumnType("date");

                entity.HasOne(d => d.ShoeldNavigation)
                    .WithOne(p => p.SalesCampaigns)
                    .HasForeignKey<SalesCampaigns>(d => d.Shoeld)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SalesCampaigns_Shoes");
            });

            modelBuilder.Entity<ShoeCategoryJunction>(entity =>
            {
                entity.HasOne(d => d.Category)
                    .WithMany(p => p.ShoeCategoryJunction)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShoeCategoryJunction_Categories");

                entity.HasOne(d => d.Shoe)
                    .WithMany(p => p.ShoeCategoryJunction)
                    .HasForeignKey(d => d.ShoeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShoeCategoryJunction_Shoes");
            });

            modelBuilder.Entity<Shoes>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Color)
                    .WithMany(p => p.Shoes)
                    .HasForeignKey(d => d.ColorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Shoes_Color");

                entity.HasOne(d => d.Fitting)
                    .WithMany(p => p.Shoes)
                    .HasForeignKey(d => d.Fittingid)
                    .HasConstraintName("FK_Shoes_Fittings");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
