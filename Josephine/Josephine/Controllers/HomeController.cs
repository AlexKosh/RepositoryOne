using Josephine.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Josephine.Controllers
{
    public class HomeController : Controller
    {
        private IProductRepository repository;
        public HomeController(IProductRepository repo)
        {
            this.repository = repo;
        }
        // GET: Home
        public ActionResult Index()
        {
            //repository.populate();
            return View();
        }
        public JsonResult Products()
        {
            return Json(repository.Products, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult Products()
        //{
        //    return View();
        //}
    }
}