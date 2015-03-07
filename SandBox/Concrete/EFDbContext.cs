using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using SandBox.Models;

namespace SandBox.Concrete
{
    public class EFDbContext : DbContext
    {
        public DbSet<WarehouseItem> WarehouseDbSet { get; set; }
        public DbSet<StoreItem> StoreDbSet { get; set; }
        public DbSet<SoldItem> SoldDbSet { get; set; }       
    }
}