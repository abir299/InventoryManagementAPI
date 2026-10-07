using System;
using System.Collections.Generic;
using InventoryManagementAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementAPI.Data;

public partial class InventoryManagementDbContext : DbContext
{
    public InventoryManagementDbContext()
    {
    }

    public InventoryManagementDbContext(DbContextOptions<InventoryManagementDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Stock> Stocks { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=InventoryManagementDbContext");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .IsFixedLength();
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsFixedLength();
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsFixedLength();
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .IsFixedLength();
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_Categories");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Products)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_Suppliers");
        });

        modelBuilder.Entity<Stock>(entity =>
        {
            entity.ToTable("Stock");

            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Product).WithMany(p => p.Stocks)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Stock_Products");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.Property(e => e.Address)
                .HasMaxLength(300)
                .IsFixedLength();
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsFixedLength();
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .IsFixedLength();
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsFixedLength();
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.Property(e => e.Remarks)
                .HasMaxLength(500)
                .IsFixedLength();
            entity.Property(e => e.TransactionDate).HasColumnType("datetime");
            entity.Property(e => e.TransactionType)
                .HasMaxLength(20)
                .IsFixedLength();

            entity.HasOne(d => d.Product).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Transactions_Products");

            entity.HasOne(d => d.User).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Transactions_Users");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsFixedLength();
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(250)
                .IsFixedLength();
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.Usename)
                .HasMaxLength(100)
                .IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
