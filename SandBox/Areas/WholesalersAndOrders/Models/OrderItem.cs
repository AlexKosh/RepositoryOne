using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SandBox.Models
{
    public class OrderItem
    {
        [Key]
        public int OrderItemID { get; set; }
        public int OrderID { get; set; }
        public string ItemName { get; set; }
        public int ItemNumber { get; set; }
        public string Color { get; set; }
        public int Size { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
    }
}