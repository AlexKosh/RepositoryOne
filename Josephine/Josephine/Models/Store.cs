using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Josephine.Models
{
    public class Store : Product
    {
        public Store()
        {

        }
        public Store(int m, string n, string c, int s, int q, int p = 9900000)
        {
            this.ModelNumber = m;
            this.Name = n;
            this.Color = c;
            this.Size = s;
            this.Quantity = q;
            this.Price = p;
        }

        public int Price { get; set; }        
    }
}