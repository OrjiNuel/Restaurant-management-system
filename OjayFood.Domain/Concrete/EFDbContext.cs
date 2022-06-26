using Microsoft.AspNet.Identity.EntityFramework;
using OjayFood.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OjayFood.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public EFDbContext() : base("EFDBContext") { }

        public static EFDbContext Create()
        {
            return new EFDbContext();
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<ShippingDetail> Shippingdetails { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<OjayFoodUser> FoodUsers { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Payment>().Property(x => x.Amount).HasPrecision(16, 8);
            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
        }
    }
}
