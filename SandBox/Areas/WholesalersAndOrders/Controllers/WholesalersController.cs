using SandBox.Abstract;
using SandBox.Areas.WholesalersAndOrders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SandBox.Areas.WholesalersAndOrders.Controllers
{
    public class WholesalersController : Controller
    {
        private IItemRepository repository;
        public WholesalersController(IItemRepository repo)
        {
            this.repository = repo;
        }

        // GET: WholesalersAndOrders/Wholesalers
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddWholesaler()
        {                       
            return View();
        }

        [HttpPost]
        public ActionResult AddWholesaler(
            string name, string lastName, string city, string tel,
            string altTel, string address, string altAddress )
        {
            Wholesaler person = new Wholesaler()
            {
                FirstName = name,
                LastName = lastName,
                City = city,
                PhoneNumber = tel,
                AlternatePhoneNumber = altTel,
                Address = address,
                AlternateAddress = altAddress
            };
            string result = repository.AddWholesaler(person);

            if (result != "Done")
            {
                TempData["Error"] = result;
            }
            
            
            return RedirectToAction("Index");
        }
    }
}