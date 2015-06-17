using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Josephine.Models
{
    public class Warehouse : Product
    {
        public Warehouse()
        {

        }
        public Warehouse(int m, string n, string c, int s, int q)
        {
            this.ModelNumber = m;
            this.Name = n;
            this.Color = c;
            this.Size = s;
            this.Quantity = q;
        }
        
    }
}