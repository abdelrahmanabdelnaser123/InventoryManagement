using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagement.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Infrastructure.Context
{
   
        public class InventoryDbContext : IdentityDbContext<ApplicationUser>
        {
            public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options)
            {
                ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            }

            public DbSet<Product> Products { get; set; }
            public DbSet<InventoryTransaction> InventoryTransactions { get; set; }
            public DbSet<Warehouse> Warehouses { get; set; }
            public DbSet<WarehouseProduct> WarehouseProducts { get; set; } 

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

         
            builder.Entity<WarehouseProduct>()
                .HasOne(wp => wp.Warehouse)
                .WithMany(w => w.WarehouseProducts)
                .HasForeignKey(wp => wp.WarehouseId);

            builder.Entity<WarehouseProduct>()
                .HasOne(wp => wp.Product)
                .WithMany(p => p.WarehouseProducts)
                .HasForeignKey(wp => wp.ProductId);

           
            builder.Entity<WarehouseProduct>()
                .HasOne(wp => wp.CreatedByUser)
                .WithMany()
                .HasForeignKey(wp => wp.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

           
            builder.Entity<InventoryTransaction>()
                .HasOne(it => it.Product)
                .WithMany()
                .HasForeignKey(it => it.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

      
            builder.Entity<InventoryTransaction>()
                .HasOne(it => it.Warehouse)
                .WithMany()
                .HasForeignKey(it => it.WarehouseId)
                .OnDelete(DeleteBehavior.Restrict);

          
            builder.Entity<InventoryTransaction>()
                .HasOne(it => it.User)
                .WithMany()
                .HasForeignKey(it => it.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Product>()
                .HasOne(p => p.CreatedByUser)
                .WithMany()
                .HasForeignKey(p => p.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Warehouse>()
                .HasOne(w => w.CreatedByUser)
                .WithMany()
                .HasForeignKey(w => w.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
    }

