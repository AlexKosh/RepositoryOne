using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SandBox.Areas.WholesalersAndOrders.Models
{
    public class Order
    {
        [Key]
        public int ItemID { get; set; }
        public int OrderID { get; set; }
        public int WholesalerID { get; set; }        
        public string Name { get; set; }
        public int ItemNumber { get; set; }
        public string Color { get; set; }
        public int Size { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public bool Delivery { get; set; }
        public string DeliveryAddress { get; set; }
        public DateTime DeliveryDateTime { get; set; }
    }
}