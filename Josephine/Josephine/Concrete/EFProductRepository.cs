using Josephine.Abstract;
using Josephine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Josephine.Concrete
{
    public class EFProductRepository : IProductRepository
    {
        private EFDbContext context = new EFDbContext();
        public IEnumerable<Store> Store
        {
            get { return context.Store; }
        }
        public IEnumerable<Warehouse> Warehouse
        {
            get { return context.Warehouse; }
        }
        public IEnumerable<OrderInfo> OrderInfo { get { return context.OrderInfo; }}
        public IEnumerable<OrderProduct> OrderProduct { get { return context.OrderProduct; }}
        public IEnumerable<Sale> Sale { get { return context.Sale; }}
        public IEnumerable<Customer> Customers { get { return context.Customer; }}
        public IEnumerable<Employee> Employees { get { return context.Employee; }}
        public IEnumerable<Prices> Prices { get { return context.Prices; }}

        public void populate(int mN, string n, string[] c, int[] s, int p)
        {
            //int[] sizes = { 44, 46, 48, 50, 52, 54 };
            //string name = "Vika";
            //int modelNumber = 417;
            //string[] colors = { "Blue", "Black", "White", "Red" };

            int[] sizes = s;
            string name = n;
            int modelNumber = mN;
            string[] colors = c;

            Store newJacket = new Store();
            int i = 0;

            newJacket.Name = name;
            newJacket.ModelNumber = modelNumber;
            newJacket.Price  = p;

            foreach (string color in colors)
            {
                foreach (int size in sizes)
                {
                    newJacket.Color = color;
                    newJacket.Size = size;
                    newJacket.Quantity = 2;

                    context.Store.Add(newJacket);
                    context.SaveChanges();
                    context.Warehouse.Add(newJacket.ToWarehouse());
                    context.SaveChanges();
                }
            }
        }

        public void AddDataToDb<T>(T d)
        {               
            System.Type type = d.GetType();
            

            switch (type.Name.ToString())
            {
                case "Employee":
                    Employee e = d as Employee;
                    if (e.EmployeeId == 0)
                    {
                        context.Employee.Add(e);
                    }
                    break; 
                case "Customer":
                    Customer c = d as Customer;
                    if (c.CustomerId == 0)
                    {
                        context.Customer.Add(c);
                    }
                    break;

                default:
                    break;
            }

            context.SaveChanges();
        }

        public interface IHasId
        {
            int EmployeeId { get; }
        }
    }
}