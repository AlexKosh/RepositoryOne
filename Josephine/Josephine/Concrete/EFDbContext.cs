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
    }
}