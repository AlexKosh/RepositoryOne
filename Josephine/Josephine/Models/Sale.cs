using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Josephine.Models
{
    public class Sale
    {
        [Key]
        public int SaleId { get; set; }
        public int OrderId { get; set; }   
        public int ProductId { get; set; }             
        public int CustomerId { get; set; }
        public DateTime SaleDate { get; set; }
        public int Quantity { get; set; }
    }
}