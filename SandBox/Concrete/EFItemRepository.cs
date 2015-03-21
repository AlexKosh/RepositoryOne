using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SandBox.Abstract;
using SandBox.Models;
using SandBox.Areas.WholesalersAndOrders.Models;
using SandBox.Areas.Warehouse.Models;

namespace SandBox.Concrete
{
    public class EFItemRepository : IItemRepository
    {
        private EFDbContext context = new EFDbContext();
        public IEnumerable<WarehouseItem> IEWarehouseItems
        {
            get { return context.WarehouseDbSet; }
        }
        public IEnumerable<StoreItem> IEStoreItems
        {
            get { return context.StoreDbSet; }
        }
        public IEnumerable<SoldItem> IESoldItems
        {
            get { return context.SoldDbSet; }
        }
        public IEnumerable<OrderData> IEOrderData
        {
            get { return context.OrderDataDbSet; }
        }
        public IEnumerable<OrderItem> IEOrderItem
        {
            get { return context.OrderItemDbSet; }
        }
        public IEnumerable<TailoringItem> IETailoringItem
        { 
            get { return context.TailoringItemDbSet;}
        }
        public IEnumerable<Wholesaler> IEWholesaler
        {
            get { return context.WholesalerDbSet; }
        }

        public void SaveOrder(IEnumerable<IItem> order)
        {
            foreach (var item in order)
            {
                var ItemToEdit = IEWarehouseItems.Where(WhItem =>
                WhItem.ItemNumber == item.ItemNumber
                && WhItem.Size == item.Size
                && WhItem.Color == item.Color).FirstOrDefault();

                if (ItemToEdit != null)
                {
                    ItemToEdit.Quantity += item.Quantity;
                }
                else
                {
                    context.WarehouseDbSet.Add((WarehouseItem)item);
                }
            }
            context.SaveChanges();
        }
        
        public void SellOrder(IEnumerable<WarehouseItem> order)
        {
            foreach (var item in order)
            {
                var ItemsToSell = IEWarehouseItems.Where(WhItem =>
                WhItem.ItemNumber == item.ItemNumber
                && WhItem.Size == item.Size
                && WhItem.Color == item.Color).FirstOrDefault();

                if (ItemsToSell != null)
                {
                    ItemsToSell.Quantity -= item.Quantity;
                }
                
            }
            context.SaveChanges();
        }

        public void SaveItemtoDB(WarehouseItem itemToSave)
        {
            WarehouseItem ItemToEdit = null;

            try
            {
                ItemToEdit = context.WarehouseDbSet.Where(WhItem =>
                WhItem.ItemNumber == itemToSave.ItemNumber
                && WhItem.Size == itemToSave.Size
                && WhItem.Color == itemToSave.Color).Select(x => x).First(); 
            }
            catch (Exception)
            {                
                
            }                                      

            if (ItemToEdit != null)
            {
                ItemToEdit.Quantity += itemToSave.Quantity;
            }
            else
            {
                context.WarehouseDbSet.Add(new WarehouseItem {
                    ItemNumber = itemToSave.ItemNumber,
                    Color = itemToSave.Color,
                    Size = itemToSave.Size,
                    Quantity = itemToSave.Quantity                    
                });
            }

            context.SaveChanges(); 
        }
        public void SaveItemtoDB(StoreItem itemToSave)
        {
            StoreItem ItemToEdit = null;

            try
            {
                ItemToEdit = context.StoreDbSet.Where(WhItem =>
                WhItem.ItemNumber == itemToSave.ItemNumber
                && WhItem.Size == itemToSave.Size
                && WhItem.Color == itemToSave.Color).Select(x => x).First();
            }
            catch (Exception)
            {

            }

            if (ItemToEdit != null)
            {
                ItemToEdit.Quantity += itemToSave.Quantity;
            }
            else
            {
                context.StoreDbSet.Add(new StoreItem
                {
                    ItemNumber = itemToSave.ItemNumber,
                    Color = itemToSave.Color,
                    Size = itemToSave.Size,
                    Quantity = itemToSave.Quantity
                });
            }

            context.SaveChanges();
        }
        public void SaveItemtoDB(TailoringItem itemToSave)
        {
            TailoringItem ItemToEdit = null;

            try
            {
                ItemToEdit = context.TailoringItemDbSet.Where(WhItem =>
                WhItem.ItemNumber == itemToSave.ItemNumber
                && WhItem.Size == itemToSave.Size
                && WhItem.Color == itemToSave.Color).Select(x => x).First();
            }
            catch (Exception)
            {

            }

            if (ItemToEdit != null)
            {
                ItemToEdit.Quantity += itemToSave.Quantity;
            }
            else
            {
                context.TailoringItemDbSet.Add(new TailoringItem
                {
                    ItemNumber = itemToSave.ItemNumber,
                    Color = itemToSave.Color,
                    Size = itemToSave.Size,
                    Quantity = itemToSave.Quantity
                });
            }

            context.SaveChanges();
        }
        public void DeleteItem(int itemID)
        {
            WarehouseItem ItemToRemove = context.WarehouseDbSet.Find(itemID);
            if (ItemToRemove != null)
            {
                context.WarehouseDbSet.Remove(ItemToRemove);
                context.SaveChanges();
            }
        }

        public ItemVM<WarehouseItem> MakeItemVM(IEnumerable<WarehouseItem> ieItem)
        {
            ItemVM<WarehouseItem> itemVM = new ItemVM<WarehouseItem>();

            itemVM.itemNumbers = ieItem
                .Select(x => x.ItemNumber).Distinct().ToList();

            List<string> listCol = new List<string>();
            itemVM.itemColors = new Dictionary<int, List<string>>();
            List<int> listSizes = new List<int>();
            itemVM.itemSizes = new Dictionary<int, List<int>>();
            List<WarehouseItem> itemsList = new List<WarehouseItem>();
            itemVM.itemsList = new Dictionary<int, List<WarehouseItem>>();
                        
            foreach (int modelNumber in itemVM.itemNumbers)
            {

                listCol = ieItem
                    .Where(x => x.ItemNumber == modelNumber)
                    .Select(x => x.Color).Distinct().ToList();

                itemVM.itemColors.Add(modelNumber, listCol);

                listSizes = ieItem
                    .Where(x => x.ItemNumber == modelNumber)
                    .Select(x => x.Size).Distinct().ToList();

                itemVM.itemSizes.Add(modelNumber, listSizes);

                itemsList = ieItem
                    .Where(x => x.ItemNumber == modelNumber)
                    .Select(x => x).ToList();
                itemsList.OrderBy(x => x.Size);

                itemVM.itemsList.Add(modelNumber, itemsList);
            }
            return itemVM;
        }

        //overload MakeItemVM method with 1 param for StoreItem
        public ItemVM<StoreItem> MakeItemVM(IEnumerable<StoreItem> ieItem)
        {
            ItemVM<StoreItem> itemVM = new ItemVM<StoreItem>();

            itemVM.itemNumbers = ieItem
                .Select(x => x.ItemNumber).Distinct().ToList();

            List<string> listCol = new List<string>();
            itemVM.itemColors = new Dictionary<int, List<string>>();
            List<int> listSizes = new List<int>();
            itemVM.itemSizes = new Dictionary<int, List<int>>();
            List<StoreItem> itemsList = new List<StoreItem>();
            itemVM.itemsList = new Dictionary<int, List<StoreItem>>();

            foreach (int modelNumber in itemVM.itemNumbers)
            {

                listCol = ieItem
                    .Where(x => x.ItemNumber == modelNumber)
                    .Select(x => x.Color).Distinct().ToList();

                itemVM.itemColors.Add(modelNumber, listCol);

                listSizes = ieItem
                    .Where(x => x.ItemNumber == modelNumber)
                    .Select(x => x.Size).Distinct().ToList();

                itemVM.itemSizes.Add(modelNumber, listSizes);

                itemsList = ieItem
                    .Where(x => x.ItemNumber == modelNumber)
                    .Select(x => x).ToList();

                itemVM.itemsList.Add(modelNumber, itemsList);
            }
            return itemVM;
        }

        //overload MakeItemVM method with 1 param for TailoringItem
        public ItemVM<TailoringItem> MakeItemVM(IEnumerable<TailoringItem> ieItem)
        {
            ItemVM<TailoringItem> itemVM = new ItemVM<TailoringItem>();

            itemVM.itemNumbers = ieItem
                .Select(x => x.ItemNumber).Distinct().ToList();

            List<string> listCol = new List<string>();
            itemVM.itemColors = new Dictionary<int, List<string>>();
            List<int> listSizes = new List<int>();
            itemVM.itemSizes = new Dictionary<int, List<int>>();
            List<TailoringItem> itemsList = new List<TailoringItem>();
            itemVM.itemsList = new Dictionary<int, List<TailoringItem>>();

            foreach (int modelNumber in itemVM.itemNumbers)
            {

                listCol = ieItem
                    .Where(x => x.ItemNumber == modelNumber)
                    .Select(x => x.Color).Distinct().ToList();

                itemVM.itemColors.Add(modelNumber, listCol);

                listSizes = ieItem
                    .Where(x => x.ItemNumber == modelNumber)
                    .Select(x => x.Size).Distinct().ToList();

                itemVM.itemSizes.Add(modelNumber, listSizes);

                itemsList = ieItem
                    .Where(x => x.ItemNumber == modelNumber)
                    .Select(x => x).ToList();
                itemsList.OrderBy(x => x.Size);

                itemVM.itemsList.Add(modelNumber, itemsList);
            }
            return itemVM;
        }
        
        public ItemVM<WarehouseItem> MakeItemVM(IEnumerable<WarehouseItem> itemParam,
            List<int> FullNumbersParam,
            Dictionary<int, List<int>> SizesParam,
            Dictionary<int, List<string>> ColorsParam)
        {
            ItemVM<WarehouseItem> itemVM = new ItemVM<WarehouseItem>();            

            if (FullNumbersParam == null)
            {
                itemVM.itemNumbersFull = itemParam
                 .Select(x => x.ItemNumber).Distinct().ToList();
            }
            else
            {
                itemVM.itemNumbersFull = FullNumbersParam;
            }

            
            itemVM.itemNumbers = itemParam
                 .Select(x => x.ItemNumber).Distinct().ToList();
                                    
            itemVM.itemColors = new Dictionary<int, List<string>>();            
            itemVM.itemSizes = new Dictionary<int, List<int>>();            
            itemVM.itemsList = new Dictionary<int, List<WarehouseItem>>();
            List<WarehouseItem> itemsList = new List<WarehouseItem>();

            itemVM.itemColors = ColorsParam;
            itemVM.itemSizes = SizesParam;            

            foreach (int modelNumber in itemVM.itemNumbers)
            {               
                itemsList = itemParam
                    .Where(x => x.ItemNumber == modelNumber)
                    .Select(x => x).OrderBy(x => x.Size).ToList();

                itemVM.itemsList.Add(modelNumber, itemsList);
            }
            return itemVM;
        }

        //overload MakeItemVM method with 4 param for TailoringItem
        public ItemVM<TailoringItem> MakeItemVM(IEnumerable<TailoringItem> itemParam,
            List<int> FullNumbersParam,
            Dictionary<int, List<int>> SizesParam,
            Dictionary<int, List<string>> ColorsParam)
        {
            ItemVM<TailoringItem> itemVM = new ItemVM<TailoringItem>();

            if (FullNumbersParam == null)
            {
                itemVM.itemNumbersFull = itemParam
                 .Select(x => x.ItemNumber).Distinct().ToList();
            }
            else
            {
                itemVM.itemNumbersFull = FullNumbersParam;
            }


            itemVM.itemNumbers = itemParam
                 .Select(x => x.ItemNumber).Distinct().ToList();

            itemVM.itemColors = new Dictionary<int, List<string>>();
            itemVM.itemSizes = new Dictionary<int, List<int>>();
            itemVM.itemsList = new Dictionary<int, List<TailoringItem>>();
            List<TailoringItem> itemsList = new List<TailoringItem>();

            itemVM.itemColors = ColorsParam;
            itemVM.itemSizes = SizesParam;

            foreach (int modelNumber in itemVM.itemNumbers)
            {
                itemsList = itemParam
                    .Where(x => x.ItemNumber == modelNumber)
                    .Select(x => x).OrderBy(x => x.Size).ToList();

                itemVM.itemsList.Add(modelNumber, itemsList);
            }
            return itemVM;
        }

        public void SetPrice(int itemNumberParam, int newPrice)
        {
            var WhItemsList = IEWarehouseItems
                .Where(x => x.ItemNumber == itemNumberParam)
                .Select(x => x);
            var StItemsList = IEStoreItems
                .Where(x => x.ItemNumber == itemNumberParam)
                .Select(x => x);

            if (WhItemsList != null)
            {
                foreach (var item in WhItemsList)
                {
                    item.Price = newPrice;
                }
            }

            if (StItemsList != null)
            {
                foreach (var item in StItemsList)
                {
                    item.Price = newPrice;
                }
            }  

            context.SaveChanges();
        }

        public void MoveItemsFromWhToSt(List<WarehouseItem> orderListParam)
        {
            using (var tx = context.Database.BeginTransaction())
            {
                try
                {
                    foreach (var item in orderListParam)
                    {
                        var targetItemInWh = IEWarehouseItems
                            .First(x => x.ItemNumber == item.ItemNumber
                            && x.Color == item.Color && x.Size == item.Size);
                        targetItemInWh.Quantity -= item.Quantity;

                        var targetItemInSt = IEStoreItems
                            .First(x => x.ItemNumber == item.ItemNumber
                            && x.Color == item.Color && x.Size == item.Size);
                        targetItemInSt.Quantity += item.Quantity;                        
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
        public void MoveItemsFromTlrgToWh(List<WarehouseItem> orderListParam)
        {
            using (var tx = context.Database.BeginTransaction())
            {
                try
                {
                    foreach (var item in orderListParam)
                    {
                        var targetItemInWh = IETailoringItem
                            .First(x => x.ItemNumber == item.ItemNumber
                            && x.Color == item.Color && x.Size == item.Size);
                        targetItemInWh.Quantity -= item.Quantity;

                        var targetItemInSt = IEWarehouseItems
                            .First(x => x.ItemNumber == item.ItemNumber
                            && x.Color == item.Color && x.Size == item.Size);
                        targetItemInSt.Quantity += item.Quantity;
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

        //populate Db with model 417, 423, 431, 432
        public void Populate()
        {
            List<string> colors = new List<string>() { "Blue", "Pearl", "Black", "Dark Biege" };
            List<int> sizes = new List<int>() { 44, 46, 48, 50, 52, 54, 56, 58, 60 };

            Dictionary<int, string> modelNames = new Dictionary<int, string>();
            modelNames.Add(417, "Vika");
            modelNames.Add(423, "Vera (coat)");
            modelNames.Add(432, "Vera (jacket)");
            modelNames.Add(431, "Nadia");


            foreach (var jacket in modelNames)
            {
                WarehouseItem whItem = new WarehouseItem();
                StoreItem storeItem = new StoreItem();
                TailoringItem tlgItem = new TailoringItem();

                whItem.Name = storeItem.Name = tlgItem.ItemName = jacket.Value;
                whItem.ItemNumber = storeItem.ItemNumber = tlgItem.ItemNumber = jacket.Key;

                foreach (var color in colors)
                {
                    if ((whItem.ItemNumber == 432 || whItem.ItemNumber == 431)
                        && color == "Black")
                    {
                        continue;
                    }
                    whItem.Color = storeItem.Color = tlgItem.Color = color;

                    foreach (var size in sizes)
                    {
                        if (whItem.ItemNumber == 431 && size >= 58)
                        {
                            continue;
                        }
                        if ((whItem.ItemNumber == 423 || whItem.ItemNumber == 432) && size == 60)
                        {
                            continue;
                        }

                        whItem.Size = storeItem.Size = tlgItem.Size = size;
                        whItem.Quantity = storeItem.Quantity = tlgItem.Quantity = 0;
                        whItem.Price = storeItem.Price = 0;

                        SaveItemtoDB(whItem);
                        SaveItemtoDB(storeItem);
                        SaveItemtoDB(tlgItem);                        
                    }

                }
            }
            
        }        
    }
}