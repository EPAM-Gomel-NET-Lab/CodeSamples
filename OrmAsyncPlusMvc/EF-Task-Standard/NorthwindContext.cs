using System;
using EF_Task_Standard.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EF_Task_Standard
{
    public class NorthwindContext : DbContext
    {
        public Guid Identity { get; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<CreditCard> CreditCards { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<EmployeeTerritory> EmployeeTerritories { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetails> OrderDetails { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<Shipper> Shippers { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<Territory> Territories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=.;database=Northwind;trusted_connection=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ////ModelBuilderExtensions.RemovePluralizingTableNameConvention(modelBuilder);
            modelBuilder.Entity<EmployeeTerritory>().HasKey(c => new
            {
                c.EmployeeID,
                c.TerritoryID
            });
            modelBuilder.Entity<OrderDetails>().HasKey(c => new
            {
                c.OrderID,
                c.ProductID
            });
            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// ??????
        /// </summary>
        private static class ModelBuilderExtensions
        {
            public static void RemovePluralizingTableNameConvention(ModelBuilder modelBuilder)
            {
                foreach (var entity in modelBuilder.Model.GetEntityTypes())
                {
                    entity.SetTableName(entity.DisplayName());
                }
            }
        }
    }
}
