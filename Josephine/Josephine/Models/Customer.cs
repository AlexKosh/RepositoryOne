using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Josephine.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Nickname { get; set; }
        public bool Gender { get; set; }
        public DateTime Birthday { get; set; }
        public int Age { get; set; }

        public DateTime FirstMet { get; set; }

        public string Notations { get; set; }
        public string locationOfTrade { get; set; }
        public string PhoneNumber { get; set; }
        public string AlternatePhone { get; set; }
        public string Email { get; set; }
        public string Skype { get; set; }
        public string OtherContact { get; set; }

        public DateTime lastPurchase { get; set; }
        public bool isInformed { get; set; }
        public int Balance { get; set; }

        public int MoneySpent { get; set; }
        public int Level { get; set; }
    }
}