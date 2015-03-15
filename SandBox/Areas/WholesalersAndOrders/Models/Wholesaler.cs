using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SandBox.Areas.WholesalersAndOrders.Models
{
    public class Wholesaler
    {
        public int WholesalerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string AlternatePhoneNumber { get; set; }
        public string Address { get; set; }
        public string AlternateAddress { get; set; }        
        public int Points { get; set; }
        public int Level { get; set; }
    }
}