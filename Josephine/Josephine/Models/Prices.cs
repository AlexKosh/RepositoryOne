using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Josephine.Models
{
    public class Prices
    {
        public int Id { get; set; }
        public int ModelNumber { get; set; }
        public int Price { get; set; }
        public DateTime Date { get; set; }
    }
}