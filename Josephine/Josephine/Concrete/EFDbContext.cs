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
        public DbSet<Store> Products { get; set; }
        public DbSet<Warehouse> Warehouse { get; set; }
    }
}