using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SandBox.Models;

namespace SandBox.Abstract
{
    public interface IItemRepository
    {
        IEnumerable<WarehouseItem> IEWarehouseItems { get; }

        IEnumerable<StoreItem> IEStoreItems { get; }

        IEnumerable<SoldItem> IESoldItems { get; }

        void SaveOrder(IEnumerable<IItem> order);

        void SellOrder(IEnumerable<WarehouseItem> order);

        void Populate();

        void SaveItemtoDB(WarehouseItem item);

        void DeleteItem(int itemID);

        ItemVM MakeItemVM(IEnumerable<WarehouseItem> ieItem);
        ItemVM MakeItemVM(IEnumerable<WarehouseItem> itemParam,
            List<int> FullNumbersParam,
            Dictionary<int, List<int>> SizesParam,
            Dictionary<int, List<string>> ColorsParam);
    }
}
