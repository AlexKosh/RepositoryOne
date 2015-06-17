using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Josephine.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int ModelNumber { get; set; }
        public string Color { get; set; }
        public int Size { get; set; }
        public int Quantity { get; set; }

        public Store ToStore()
        {
            Store st = new Store(this.ModelNumber, this.Name, this.Color, this.Size, this.Quantity);
            return st;
        }

        public Warehouse ToWarehouse()
        {
            Warehouse wh = new Warehouse(this.ModelNumber, this.Name, this.Color, this.Size, this.Quantity);
            return wh;
        }
    }
}