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
        IEnumerable<MainWarehouse> MainWh { get; }
        IEnumerable<Recipe> Recipes { get; }
        IEnumerable<RecipeItem> RecipeItems { get; }
        IEnumerable<ProductionTask> ProductionTasks { get; }
        IEnumerable<TaskItem> TaskItem { get; }
        IEnumerable<Cut> Cut { get; }

        void populateWhAndSt(int mN, string n, string[] c, int[] s, int p);
        void deleteWhAndSt();
        void populatePrices(int[] mN, int[] p);
        void deletePrices();
        void populateCust(Customer[] c);
        void deleteCust();
        void populateEmployee(Employee[] e);
        void deleteEmployee();
        void populateOrders(OrderInfo[] oi, OrderProduct[] op);
        void deleteOrders();
        void populateMainWh(List<MainWarehouse> mvh);
        void deleteMainWh();

        void AddDataToDb<T>(T d);
        int AddOrderInfoToDb(OrderInfo oi);
        void AddOrderProductsToDb(IEnumerable<OrderProduct> op, int oId);
        void AddSoldProductsToDb(IEnumerable<OrderProduct> op, int customerId, int employeeId, int orderId);
        
        
        void AddMainWhItemToDb(MainWarehouse item);
        void AddProductionTaskToDb(ProductionTask task);
        void AddRecipeToDb(Recipe rcp);
        void AddCutToDb(Cut c);
    }
}
