using SandBox.Abstract;
using SandBox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SandBox.Areas.Store.Controllers
{
    public class OnStoresStockController : Controller
    {
        private IItemRepository repository;
        public OnStoresStockController(IItemRepository repo)
        {
            this.repository = repo;
        }
        ItemVM<StoreItem> DbModel = new ItemVM<StoreItem>();        

        // GET: Store/OnStoresStock
        public ActionResult Index()
        {
            try
            {
                repository.IEStoreItems.First();
            }
            catch (Exception)
            {
                repository.Populate();
                TempData["Success"] = "Db populated";
            }

            DbModel = repository.MakeItemVM(repository.IEStoreItems);   
            return View(DbModel);
        }

        public ActionResult PickItem(int model, string color, int size)
        {
            string order = string.Format("Picked: {0} {1} {2}", model, color, size);
            Session["OrderExam"] = order;
            return RedirectToAction("index");
        }
    }
}