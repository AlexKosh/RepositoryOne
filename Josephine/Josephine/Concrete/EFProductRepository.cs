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
                case "Warehouse":
                    Warehouse w = d as Warehouse;
                    Warehouse itemToEdit = new Warehouse();

                    itemToEdit = context.Warehouse.FirstOrDefault(x => x.ModelNumber == w.ModelNumber && 
                        x.Color == w.Color && x.Size == w.Size);

                    if (itemToEdit == null)
                    {
                        context.Warehouse.Add(w);
                    }
                    else
                    {
                        itemToEdit.Quantity += w.Quantity;
                    }                    
                    break;
                ////case "OrderInfo":
                ////    OrderInfo oi = d as OrderInfo;
                ////    OrderInfo orderToEdit = new OrderInfo();

                ////    orderToEdit = context.OrderInfo.FirstOrDefault(x => x.OrderDate == oi.OrderDate);

                ////    if (orderToEdit == null)
                ////    {
                ////        context.OrderInfo.Add(oi);
                ////    }
                ////    else
                ////    {
                ////        orderToEdit = oi;
                ////    }
                ////    break;
                default:
                    break;
            }

            context.SaveChanges();
        }
        public int AddOrderInfoToDb(OrderInfo oi)
        {
            if (oi.OrderId == 0)
            {
                context.OrderInfo.Add(oi);
                context.SaveChanges();
                return oi.OrderId;
            }
            else
            {
                OrderInfo orderToEdit = new OrderInfo();
                orderToEdit = context.OrderInfo.FirstOrDefault(x => x.OrderId == oi.OrderId);
                orderToEdit = oi;
                context.SaveChanges();
                return orderToEdit.OrderId;
            }
        }
        public void AddOrderProductsToDb(IEnumerable<OrderProduct> op, int oId)
        {
            var itemsToRemove = context.OrderProduct.Where(x => x.OrderId == oId).Select(x => x);

            if (itemsToRemove != null)
            {
                context.OrderProduct.RemoveRange(itemsToRemove);
            }
            using (var tx = context.Database.BeginTransaction())
            {
                try
                {
                    foreach (var item in op)
                    {
                        item.OrderId = oId;
                        context.OrderProduct.Add(item);

                        var itemFromStore = context.Store.Find(item.ProductId);
                        itemFromStore.Quantity -= item.Quantity;                                                
                    }
                    context.SaveChanges();
                    tx.Commit();
                }
                catch (Exception)
                {
                    tx.Rollback();
                }
            }               
        }
        public void AddSoldProductsToDb(IEnumerable<OrderProduct> op, int customerId, int employeeId, int orderId)
        {             
            using (var tx = context.Database.BeginTransaction())
            {
                try
                {
                    foreach (var p in op)
                    {
                        Sale s = new Sale(p, customerId, employeeId, orderId);
                        context.Sale.Add(s);                    
                    }
                    context.SaveChanges();
                    tx.Commit();
                }
                catch (Exception)
                {
                    tx.Rollback();
                }
            }            
        }        
    }
}