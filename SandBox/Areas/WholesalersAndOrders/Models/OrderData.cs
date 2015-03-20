using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SandBox.Areas.WholesalersAndOrders.Models
{
    public class OrderData
    {
        [Key]        
        public int OrderID { get; set; }
        public int WholesalerID { get; set; } 
        public bool Delivery { get; set; }
        public string DeliveryAddress { get; set; }
        public DateTime DeliveryDateTime { get; set; }
        public string Description { get; set; }        
    }
}