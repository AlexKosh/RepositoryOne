using Josephine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Josephine.Abstract
{
    public interface IProductRepository
    {
        IEnumerable<Store> Products { get; }
        IEnumerable<Warehouse> Warehouse { get; }

        void populate(int mN, string n, string[] c, int[] s, int p);
    }
}
