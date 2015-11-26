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
        public IEnumerable<MainWarehouse> MainWh { get { return context.MainWh; }}
        public IEnumerable<Recipe> Recipes { get { return context.Recipe; }}
        public IEnumerable<RecipeItem> RecipeItems { get { return context.RecipeItems; }}
        public IEnumerable<ProductionTask> ProductionTasks { get { return context.ProductionTask; }}
        public IEnumerable<TaskItem> TaskItem { get { return context.TaskItem; }}
        public IEnumerable<Cut> Cut { get { return context.Cut; }}

        public void populateWhAndSt(int mN, string n, string[] c, int[] s, int p)
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
        public void deleteWhAndSt()
        {
            context.Warehouse.RemoveRange(context.Warehouse);
            context.Store.RemoveRange(context.Store);
            context.SaveChanges();
        }

        public void populatePrices(int[] mN, int[] p)
        {
            for (int i = 0; i < mN.Length; i++)
            {
                Prices pr = new Prices();
                pr.ModelNumber = mN[i];
                pr.Price = p[i];
                pr.Date = new DateTime(2015, 9, 24, 20, 11, 50);

                context.Prices.Add(pr);
                
            }

            context.SaveChanges();
        }
        public void deletePrices()
        {
            context.Prices.RemoveRange(context.Prices);
            context.SaveChanges();
        }

        public void populateCust(Customer[] c)
        {
            for (int i = 0; i < c.Length; i++)
            {
                context.Customer.Add(c[i]);
            }
            context.SaveChanges();
        }
        public void deleteCust()
        {
            context.Customer.RemoveRange(context.Customer);
            context.SaveChanges();
        }

        public void populateEmployee(Employee[] e)
        {
            for (int i = 0; i < e.Length; i++)
            {
                context.Employee.Add(e[i]);
            }
            context.SaveChanges();
        }
        public void deleteEmployee()
        {
            context.Employee.RemoveRange(context.Employee);
            context.SaveChanges();
        }

        public void populateOrders(OrderInfo[] oi, OrderProduct[] op)
        {
            context.OrderInfo.AddRange(oi);
            context.SaveChanges();

            int[] ordersId = context.OrderInfo.Select(x => x.OrderId).ToArray();
            for (int i = 0; i < 40; i++)
            {
                op[i].OrderId = ordersId[i];                
            }
            context.OrderProduct.AddRange(op);
            
            context.SaveChanges();
        }
        public void deleteOrders()
        {
            context.OrderInfo.RemoveRange(context.OrderInfo);
            context.OrderProduct.RemoveRange(context.OrderProduct);
            context.SaveChanges();
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
                    Store s = w.ToStore();
                    s.Quantity = 0;

                    itemToEdit = context.Warehouse.FirstOrDefault(x => x.ModelNumber == w.ModelNumber && 
                        x.Color == w.Color && x.Size == w.Size);

                    if (itemToEdit == null)
                    {
                        context.Warehouse.Add(w);
                        context.Store.Add(s);
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


        
        public void populateMainWh(List<MainWarehouse> mwh)
        {
            context.MainWh.AddRange(mwh);
            context.SaveChanges();
        }
        public void AddMainWhItemToDb(MainWarehouse item)
        {
            try
            {
                context.MainWh.Add(item);
                context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void AddProductionTaskToDb(ProductionTask task)
        {
            try
            {                
                context.ProductionTask.Add(task);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }
        public void AddRecipeToDb(Recipe rcp)
        {
            try
            {
                context.Recipe.Add(rcp);
                context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void AddCutToDb(Cut c)
        {
            try
            {
                context.Cut.Add(c);
                context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}