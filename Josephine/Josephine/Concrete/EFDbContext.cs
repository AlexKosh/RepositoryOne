using Josephine.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Josephine.Concrete
{
    public class EFDbContext : DbContext
    {
        public DbSet<Store> Store { get; set; }
        public DbSet<Warehouse> Warehouse { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<OrderInfo> OrderInfo { get; set; }
        public DbSet<OrderProduct> OrderProduct { get; set; }
        public DbSet<Sale> Sale { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Prices> Prices { get; set; }

        public DbSet<MainWarehouse> MainWh { get; set; }
        public DbSet<Recipe> Recipe { get; set; }
        public DbSet<ProductionTask> ProductionTask { get; set; }
    }
}