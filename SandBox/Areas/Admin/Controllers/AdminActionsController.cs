using SandBox.Abstract;
using SandBox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SandBox.Areas.Admin.Controllers
{
    public class AdminActionsController : Controller
    {
        private IItemRepository repository;
        public AdminActionsController(IItemRepository repo)
        {
            this.repository = repo;
        }
        ItemVM<StoreItem> DbModel = new ItemVM<StoreItem>();

        // GET: Admin/AdminActions
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SetPrice()
        {
            return View("Index");
        }
        [HttpPost]
        public ActionResult SetPrice(int itemNumb, int newPrice)
        {
            Session["itemExample"] = itemNumb;
            Session["priceExample"] = newPrice;
            repository.SetPrice(itemNumb, newPrice);
            return View("Index");
        }
    }
}