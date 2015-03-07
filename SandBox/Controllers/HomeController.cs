using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SandBox.Models;
using SandBox.Concrete;
using SandBox.Abstract;

namespace SandBox.Controllers
{
    public class HomeController : Controller
    {
        private IItemRepository repository;
        public HomeController(IItemRepository repo)
        {
            this.repository = repo;
        }

        public ActionResult Index()
        {            
            return View(repository.MakeItemVM(repository.IEWarehouseItems));
        }       

    }
}