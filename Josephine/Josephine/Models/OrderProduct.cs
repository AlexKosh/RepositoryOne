﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Josephine.Models
{
    public class OrderProduct
    {
        [Key, Column(Order = 0)]
        public int OrderId { get; set; }
        [Key, Column(Order = 1)]
        public int ProductId { get; set; }
        public int ModelNumber { get; set; }
        public string Color { get; set; }
        public int Size { get; set; }
        public int Quantity { get; set; }
        public int ProductPrice { get; set; }
        public int ProductDiscount { get; set; }
    }
}