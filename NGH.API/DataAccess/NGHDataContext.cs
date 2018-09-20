using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NGH.API.Models;
using Microsoft.Data;

namespace NGH.API.DataAccess
{
    public class NGHDataContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductDiscount> ProductDiscount { get; set; }
        public DbSet<DeliveryNote> DeliveryNotes { get; set; }
        public DbSet<DeliveryNoteLine> DeliveryNoteLines { get; set; }
        public DbSet<StockTransaction> StockTransactions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Status> Status { get; set; }

        public NGHDataContext(DbContextOptions<NGHDataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UIObjectPermission>()
                .HasKey(op => new { op.ObjectId, op.PermissionId });
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.RoleId, ur.UserId });
            modelBuilder.Entity<User>().Ignore(u => u.Password);   
/*
            modelBuilder.Entity<ProductDiscount>()
                .HasOne<Product>(p => p.Product)
                .WithMany(pd => pd.ProductDiscounts)
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductDiscount>()
                .HasOne(p => p.Product)
                .WithMany(pd => pd.ProductDiscounts)
                .OnDelete(DeleteBehavior.Cascade);
*/
        }
    }
}