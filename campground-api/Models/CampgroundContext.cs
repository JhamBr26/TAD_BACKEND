using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace campground_api.Models;

public partial class CampgroundContext : DbContext
{
    public CampgroundContext()
    {
    }

    public CampgroundContext(DbContextOptions<CampgroundContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Campground> Campgrounds { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Campground>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__campgrou__3213E83F5EB701C5");

            entity.ToTable("campgrounds");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Latitude)
                .HasColumnType("decimal(9, 6)")
                .HasColumnName("latitude");
            entity.Property(e => e.Location)
                .HasMaxLength(200)
                .HasColumnName("location");
            entity.Property(e => e.Longitude)
                .HasColumnType("decimal(9, 6)")
                .HasColumnName("longitude");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("price");
            entity.Property(e => e.Title)
                .HasMaxLength(150)
                .HasColumnName("title");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Campgrounds)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__campgroun__user___398D8EEE");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__images__3213E83FD87E0FCF");

            entity.ToTable("images");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CampgroundId).HasColumnName("campground_id");
            entity.Property(e => e.Filename).HasColumnName("filename");
            entity.Property(e => e.Url).HasColumnName("url");

            entity.HasOne(d => d.Campground).WithMany(p => p.Images)
                .HasForeignKey(d => d.CampgroundId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__images__campgrou__3C69FB99");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__reviews__3213E83FE5F9DD6F");

            entity.ToTable("reviews");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Body).HasColumnName("body");
            entity.Property(e => e.CampgroundId).HasColumnName("campground_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Campground).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.CampgroundId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__reviews__campgro__403A8C7D");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__reviews__user_id__3F466844");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__users__3213E83F02E81379");

            entity.ToTable("users");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .HasColumnName("email");
            entity.Property(e => e.Hash).HasColumnName("hash");
            entity.Property(e => e.Salt).HasColumnName("salt");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
