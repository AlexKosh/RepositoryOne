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
        IEnumerable<Store> Store { get; }
        IEnumerable<Warehouse> Warehouse { get; }
        IEnumerable<OrderInfo> OrderInfo { get; }
        IEnumerable<OrderProduct> OrderProduct { get; }
        IEnumerable<Sale> Sale { get; }
        IEnumerable<Customer> Customers { get; }
        IEnumerable<Employee> Employees { get; }
        IEnumerable<Prices> Prices { get; }

        void populate(int mN, string n, string[] c, int[] s, int p);

        void AddDataToDb<T>(T d);
        int AddOrderInfoToDb(OrderInfo oi);
        void AddOrderProductsToDb(IEnumerable<OrderProduct> op, int oId);
    }
}
