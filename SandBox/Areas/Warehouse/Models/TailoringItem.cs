using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SandBox.Areas.Warehouse.Models
{
    public class TailoringItem
    {
        [Key]
        public int ItemID { get; set; }
        public int ItemNumber { get; set; }
        public string ItemName { get; set; }
        public string Color { get; set; }
        public int Size { get; set; }        
        public int Quantity { get; set; }
    }
}