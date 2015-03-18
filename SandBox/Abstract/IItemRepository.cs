using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SandBox.Models;
using SandBox.Areas.WholesalersAndOrders.Models;
using SandBox.Areas.Warehouse.Models;

namespace SandBox.Abstract
{
    public interface IItemRepository
    {
        IEnumerable<WarehouseItem> IEWarehouseItems { get; }

        IEnumerable<StoreItem> IEStoreItems { get; }

        IEnumerable<SoldItem> IESoldItems { get; }

        IEnumerable<Wholesaler> IEWholesaler { get; }

        IEnumerable<OrderData> IEOrderData { get; }

        IEnumerable<OrderItem> IEOrderItem { get; }

        IEnumerable<TailoringItem> IETailoringItem { get; }

        void SaveOrder(IEnumerable<IItem> order);

        void SellOrder(IEnumerable<WarehouseItem> order);

        void Populate();

        void SaveItemtoDB(WarehouseItem item);

        void DeleteItem(int itemID);

        //overload MakeItemVM method for WarehouseItem
        ItemVM<WarehouseItem> MakeItemVM(IEnumerable<WarehouseItem> ieItem);
        ItemVM<WarehouseItem> MakeItemVM(IEnumerable<WarehouseItem> itemParam,
            List<int> FullNumbersParam,
            Dictionary<int, List<int>> SizesParam,
            Dictionary<int, List<string>> ColorsParam);

        //overload MakeItemVM method for StoreItem
        ItemVM<StoreItem> MakeItemVM(IEnumerable<StoreItem> ieItem);

        //Sets the price for the model 
        void SetPrice(int itemNumber, int newPrice);

        //Moves items from Warehouse to Store
        void MoveItemsFromWhToSt(List<WarehouseItem> orderListParam);
    }
}
