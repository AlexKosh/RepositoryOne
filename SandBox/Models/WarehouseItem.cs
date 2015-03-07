using SandBox.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SandBox.Models
{
    public class WarehouseItem : IItem
    {
        [Key]
        public int ItemID { get; set; }
        public string Name { get; set; }
        public int ItemNumber { get; set; }
        public string Color { get; set; }
        public int Size { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }

        public StoreItem ToStore(WarehouseItem whItem)
        {
            StoreItem storeItem = new StoreItem();
            storeItem.Name = whItem.Name;
            storeItem.ItemNumber = whItem.ItemNumber;
            storeItem.Color = whItem.Color;
            storeItem.Size = whItem.Size;
            storeItem.Quantity = whItem.Quantity;
            storeItem.Price = whItem.Price;

            return storeItem;
        }

        public SoldItem ToSold(WarehouseItem whItem)
        {
            SoldItem soldItem = new SoldItem();
            soldItem.Name = whItem.Name;
            soldItem.ItemNumber = whItem.ItemNumber;
            soldItem.Color = whItem.Color;
            soldItem.Size = whItem.Size;
            soldItem.Quantity = whItem.Quantity;
            soldItem.Price = whItem.Price;

            return soldItem;
        }
    }
}