using SandBox.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SandBox.Models
{
    public class ItemVM<T>
    {
        List<int> intList = new List<int>();
        public ItemVM()
        {
            itemNumbers = new List<int>();
            itemsList = new Dictionary<int, List<T>>();
        }
        public List<int> itemNumbers { get; set; }
        public List<int> itemNumbersFull { get; set; }
        public Dictionary<int, List<int>> itemSizes { get; set; }
        public Dictionary<int, List<string>> itemColors { get; set; }
        public Dictionary<int, List<T>> itemsList { get; set; }       
    }
}