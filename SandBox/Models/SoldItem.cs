using SandBox.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SandBox.Models
{
    public class SoldItem : IItem
    {
        public SoldItem()
        {
            Time = new DateTime();
        }

        [Key]
        public int ItemID { get; set; }
        public string Name { get; set; }
        public int ItemNumber { get; set; }
        public string Color { get; set; }
        public int Size { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }        
        public DateTime Time { get; set; }

        public WarehouseItem ToWarehouse(SoldItem soldItem)
        {
            WarehouseItem whItem = new WarehouseItem();
            whItem.Name = soldItem.Name;
            whItem.ItemNumber = soldItem.ItemNumber;
            whItem.Color = soldItem.Color;
            whItem.Size = soldItem.Size;
            whItem.Quantity = soldItem.Quantity;
            whItem.Price = soldItem.Price;

            return whItem;
        }

        public StoreItem ToStore(SoldItem soldItem)
        {
            StoreItem storeItem = new StoreItem();
            storeItem.Name = soldItem.Name;
            storeItem.ItemNumber = soldItem.ItemNumber;
            storeItem.Color = soldItem.Color;
            storeItem.Size = soldItem.Size;
            storeItem.Quantity = soldItem.Quantity;
            storeItem.Price = soldItem.Price;

            return storeItem;
        }

    }
}