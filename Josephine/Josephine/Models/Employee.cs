using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Josephine.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime Birthday { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string AlternatePhone { get; set; }
        public string Skype { get; set; }
        public string SocialNetwork { get; set; }
        public string Email { get; set; }
        public string Speciality { get; set; }
        public string Description { get; set; }
        public bool Gender { get; set; }

        //public Employee()
        //{

        //}
        //public Employee(Employee e)
        //{
        //    this.EmployeeId = e.EmployeeId;
        //    this.Name = e.Name;
        //    this.Surname = e.Surname;
        //}
    }
}