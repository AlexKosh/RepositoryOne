using SandBox.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SandBox.Models
{
    public class OrderList
    {
        public OrderList()
        {
            OrderItems = new List<WarehouseItem>();
        }

        public List<WarehouseItem> OrderItems;
    }
}