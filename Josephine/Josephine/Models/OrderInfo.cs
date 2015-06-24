﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Josephine.Models
{
    public class OrderInfo
    {
        [Key]
        public int OrderId { get; set; }
        public int EmployeeId { get; set; }
        public int CustomerId { get; set; }

        public DateTime OrderDate { get; set; }
        public DateTime ShipmentDateMin { get; set; }
        public DateTime ShipmentDateMax { get; set; }

        public string ShipAddress { get; set; }        
        public string OrderNotation { get; set; }
        public string ShipFrom { get; set; }
        public string ShippingMethod { get; set; }
        public int Priority { get; set; }
        public int OrderRecievingCode { get; set; }

        public string PaymentMethod { get; set; }
        public int OrderCost { get; set; }
        public int OrderDiscount { get; set; }
        public bool isPaid { get; set; }
    }
}