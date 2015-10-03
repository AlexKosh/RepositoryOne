using Josephine.Abstract;
using Josephine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Josephine.Controllers
{    
    public class ProductionController : Controller
    {
        private IProductRepository repository;
        public ProductionController(IProductRepository repo)
        {
            this.repository = repo;
        }

        // GET: Production
        public ActionResult Index()
        {
            return View();
        }

        public void pplMainWh()
        {
            List<MainWarehouse> mwh = new List<MainWarehouse>();

            MainWarehouse mwhItem = new MainWarehouse(1, "Плащевка", "Синий", 500, "метр");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(1, "Плащевка", "Светлый беж", 700, "метр");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(1, "Плащевка", "Темный беж", 300, "метр");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(1, "Плащевка", "Черный", 200, "метр");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(2, "Змейка 10см", "Синий", 60, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(2, "Змейка 10см", "Светлый беж", 110, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(2, "Змейка 10см", "Темный беж", 40, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(2, "Змейка 10см", "Черный", 20, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(2, "Змейка 80см", "Синий", 30, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(2, "Змейка 80см", "Светлый беж", 60, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(2, "Змейка 80см", "Темный беж", 20, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(2, "Змейка 80см", "Черный", 20, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(2, "Шнурок для капюшона", "Черный", 20, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(2, "Шнурок для капюшона", "Синий", 30, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(2, "Шнурок для капюшона", "Светлый беж", 50, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(2, "Шнурок для капюшона", "Темный беж", 20, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(2, "Кнопка скрытая", "метал", 400, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(2, "Змейка 20см", "Синий", 30, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(2, "Змейка 20см", "Светлый беж", 55, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(2, "Змейка 20см", "Темный беж", 20, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(2, "Змейка 20см", "Черный", 10, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(3, "Нитка PolyArt", "Синий", 1000, "метр");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(3, "Нитка PolyArt", "Светлый беж", 1500, "метр");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(3, "Нитка PolyArt", "Темный беж", 700, "метр");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(3, "Нитка PolyArt", "Черный", 500, "метр");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(3, "Нитка-резинка 60", "Белый", 1000, "метр");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(3, "Бирка Prunel", "Черный", 1000, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(3, "Бирка Made in", "Черный", 1000, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(3, "Бирка № размер", "Белый", 1000, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(3, "Бирка № швеи", "Белый", 1000, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(3, "Бирка условия эксплуатации", "Белый", 1000, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(3, "Бирка условия эксплуатации", "Белый", 1000, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(4, "Чернобурка", "Чернобурка", 1000, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(5, "Вика", "Синий", 100, "метр");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(5, "Вика", "Светлый беж", 200, "метр");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(5, "Вика", "Темный беж", 50, "метр");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(5, "Вика", "Черный", 50, "метр");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(6, "Силикон 100", "б/ц", 3000, "метр");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(7, "Пакеты 120", "б/ц", 500, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(7, "Пакеты 100", "б/ц", 500, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(7, "Ярлык Prunel", "Черный", 2500, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(7, "Наклейка размер 44", "б/ц", 400, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(7, "Наклейка размер 46", "б/ц", 400, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(7, "Наклейка размер 48", "б/ц", 400, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(7, "Наклейка размер 50", "б/ц", 400, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(7, "Наклейка № модели 435", "б/ц", 2400, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(7, "Наклейка № модели 431", "б/ц", 400, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(7, "Пакеты 120", "б/ц", 500, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(7, "Тремпель", "Черный", 5000, "шт.");
            mwh.Add(mwhItem);

            repository.populateMainWh(mwh);            
        }

        public JsonResult getMainWh()
        {
            var data =
                from item in repository.MainWh
                group item by item.CategoryId into newGroup
                orderby newGroup.Key
                select newGroup;

            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}