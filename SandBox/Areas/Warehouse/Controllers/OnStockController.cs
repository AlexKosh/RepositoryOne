using SandBox.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SandBox.Areas.Warehouse.Controllers
{
    public class OnWarehousesStockController : Controller
    {
        private IItemRepository repository;
        public OnWarehousesStockController(IItemRepository repo)
        {
            this.repository = repo;
        }

        public ActionResult Index()
        {            
            return View(repository.MakeItemVM(repository.IEWarehouseItems));
        }
    }
}