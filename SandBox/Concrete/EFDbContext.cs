using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using SandBox.Models;
using SandBox.Areas.WholesalersAndOrders.Models;
using SandBox.Areas.Warehouse.Models;

namespace SandBox.Concrete
{
    public class EFDbContext : DbContext
    {
        public DbSet<WarehouseItem> WarehouseDbSet { get; set; }
        public DbSet<StoreItem> StoreDbSet { get; set; }
        public DbSet<SoldItem> SoldDbSet { get; set; }
        public DbSet<OrderData> OrderDataDbSet { get; set; }
        public DbSet<OrderItem> OrderItemDbSet { get; set; }
        public DbSet<Wholesaler> WholesalerDbSet { get; set; }
        public DbSet<TailoringItem> TailoringItemDbSet { get; set; }
    }
}