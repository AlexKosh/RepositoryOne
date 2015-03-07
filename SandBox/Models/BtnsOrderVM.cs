using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SandBox.Models
{
    public class BtnsOrderVM
    {
        public BtnsOrderVM()
        {
            SelNumbers = new Dictionary<int, bool>();
            SelSizes = new Dictionary<int, bool>();
            SelColors = new Dictionary<string, bool>();
        }
        public Dictionary<int, bool> SelNumbers { get; set; }
        public Dictionary<string, bool> SelColors { get; set; }
        public Dictionary<int, bool> SelSizes { get; set; }
    }
}