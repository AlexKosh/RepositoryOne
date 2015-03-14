using SandBox.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SandBox.Models
{
    public class ItemVM
    {
        List<int> intList = new List<int>();
        public ItemVM()
        {
            itemNumbers = new List<int>();
        }
        public List<int> itemNumbers { get; set; }
        public List<int> itemNumbersFull { get; set; }
        public Dictionary<int, List<int>> itemSizes { get; set; }
        public Dictionary<int, List<string>> itemColors { get; set; }
        public Dictionary<int, List<WarehouseItem>> itemsList { get; set; }        
    }
}