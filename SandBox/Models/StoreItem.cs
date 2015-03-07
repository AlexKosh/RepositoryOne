using SandBox.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SandBox.Models
{
    public class StoreItem : IItem
    {       
        [Key]
        public int ItemID { get; set; }
        public string Name { get; set; }
        public int ItemNumber { get; set; }
        public string Color { get; set; }
        public int Size { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }

        public WarehouseItem ToWarehouse(StoreItem storeItem)
        {
            WarehouseItem whItem = new WarehouseItem();
            whItem.Name = storeItem.Name;
            whItem.ItemNumber = storeItem.ItemNumber;
            whItem.Color = storeItem.Color;
            whItem.Size = storeItem.Size;
            whItem.Quantity = storeItem.Quantity;
            whItem.Price = storeItem.Price;

            return whItem;
        }
        public SoldItem ToSold(StoreItem storeItem)
        {
            SoldItem soldItem = new SoldItem();
            soldItem.Name = storeItem.Name;
            soldItem.ItemNumber = storeItem.ItemNumber;
            soldItem.Color = storeItem.Color;
            soldItem.Size = storeItem.Size;
            soldItem.Quantity = storeItem.Quantity;
            soldItem.Price = storeItem.Price;

            return soldItem;
        }
    }
}