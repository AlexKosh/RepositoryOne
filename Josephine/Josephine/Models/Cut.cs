using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Josephine.Models
{
    public class Cut
    {
        [Key]
        public int CutId { get; set; }
        public int ProductId { get; set; }
        public int ModelNumber { get; set; }        
        public string Name { get; set; }
        public int Size { get; set; }
        public string Color { get; set; }
        public int Quantity { get; set; }
    }
}