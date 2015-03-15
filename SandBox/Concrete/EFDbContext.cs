using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using SandBox.Models;
using SandBox.Areas.WholesalersAndOrders.Models;

namespace SandBox.Concrete
{
    public class EFDbContext : DbContext
    {
        public DbSet<WarehouseItem> WarehouseDbSet { get; set; }
        public DbSet<StoreItem> StoreDbSet { get; set; }
        public DbSet<SoldItem> SoldDbSet { get; set; }
        public DbSet<Order> OrderDbSet { get; set; }
        public DbSet<Wholesaler> WholesalerDbSet { get; set; }
    }
}