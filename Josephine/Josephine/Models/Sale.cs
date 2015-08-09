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
        public int EmployeeId { get; set; }
        public DateTime SaleDate { get; set; }
        public int ModelNumber { get; set; }
        public string Color { get; set; }
        public int Size { get; set; }
        public int Quantity { get; set; }
        public int ProductPrice { get; set; }

        public Sale()
        {

        }
        public Sale(OrderProduct sp, int custId, int empId, int ordId)
        {
            this.SaleId = 0;
            this.OrderId = ordId;
            this.ProductId = sp.ProductId;
            this.CustomerId = custId;
            this.EmployeeId = empId;

            this.SaleDate = DateTime.Now;

            this.ModelNumber = sp.ModelNumber;
            this.Color = sp.Color;
            this.Size = sp.Size;
            this.Quantity = sp.Quantity;
            this.ProductPrice = sp.ProductPrice;
        }
    }
}