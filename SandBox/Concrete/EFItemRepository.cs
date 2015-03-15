﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SandBox.Abstract;
using SandBox.Models;
using SandBox.Areas.WholesalersAndOrders.Models;

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
        public IEnumerable<Order> IEOrder
        {
            get { return context.OrderDbSet; }
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
            var ItemToEdit = IEWarehouseItems.Where(WhItem =>
                WhItem.ItemNumber == itemToSave.ItemNumber
                && WhItem.Size == itemToSave.Size
                && WhItem.Color == itemToSave.Color).FirstOrDefault();

            if (ItemToEdit != null)
            {
                ItemToEdit.Quantity += itemToSave.Quantity;
            }
            else
            {
                context.WarehouseDbSet.Add(itemToSave);
            }

            context.SaveChanges(); 
        }
        public void SaveItemtoDB(StoreItem itemToSave)
        {
            try
            {
                var ItemToEdit = IEStoreItems.Where(StItem =>
                StItem.ItemNumber == itemToSave.ItemNumber
                && StItem.Size == itemToSave.Size
                && StItem.Color == itemToSave.Color).First();

                ItemToEdit.Quantity += itemToSave.Quantity;

                context.SaveChanges();
            }
            catch (System.InvalidOperationException)
            {
                context.StoreDbSet.Add(itemToSave);
                context.SaveChanges();
            }
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

        public ItemVM MakeItemVM(IEnumerable<WarehouseItem> ieItem)
        {
            ItemVM itemVM = new ItemVM();

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

                itemVM.itemsList.Add(modelNumber, itemsList);
            }
            return itemVM;
        }
        
        public ItemVM MakeItemVM(IEnumerable<WarehouseItem> itemParam,
            List<int> FullNumbersParam,
            Dictionary<int, List<int>> SizesParam,
            Dictionary<int, List<string>> ColorsParam)
        {
            ItemVM itemVM = new ItemVM();            

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

        //populate Db with model 417, 423, 431, 432
        public void Populate()
        {
            List<string> colors = new List<string>() {"Blue", "Pearl", "Black", "Dark Biege"};
            List<int> sizes = new List<int>() {44, 46, 48, 50, 52, 54, 56, 58, 60};

            Dictionary<int, string> modelNames = new Dictionary<int, string>();
            modelNames.Add(417, "Vika");
            modelNames.Add(423, "Vera (coat)");
            modelNames.Add(432, "Vera (jacket)");
            modelNames.Add(431, "Nadia");


            foreach (var jacket in modelNames)
            {
                WarehouseItem whItem = new WarehouseItem();
                StoreItem storeItem = new StoreItem();

                whItem.Name = storeItem.Name = jacket.Value;
                whItem.ItemNumber = storeItem.ItemNumber = jacket.Key;

                foreach (var color in colors)
                {
                    if ((whItem.ItemNumber == 432 || whItem.ItemNumber == 431)
                        && color == "Black")
                    {
                        continue;
                    }
                    whItem.Color = storeItem.Color = color;

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

                        whItem.Size = storeItem.Size = size;
                        whItem.Quantity = storeItem.Quantity = 0;
                        whItem.Price = storeItem.Price = 0;

                        context.WarehouseDbSet.Add(whItem);
                        context.StoreDbSet.Add(storeItem);
                        context.SaveChanges();
                    }

                }
            }
            
        }

        
    }
}