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

        //public void pplMainWh()
        //{
        //    List<MainWarehouse> mwh = new List<MainWarehouse>();

        //    MainWarehouse mwhItem = new MainWarehouse(2, "Плащевка", "Синий", 500, "метр");
        //    mwh.Add(mwhItem);
        //    mwhItem = new MainWarehouse(2, "Плащевка", "Светлый беж", 700, "метр");
        //    mwh.Add(mwhItem);
        //    mwhItem = new MainWarehouse(2, "Плащевка", "Темный беж", 300, "метр");
        //    mwh.Add(mwhItem);
        //    mwhItem = new MainWarehouse(2, "Плащевка", "Черный", 200, "метр");
        //    mwh.Add(mwhItem);
        //    mwhItem = new MainWarehouse(3, "Артикул 575", "Черный", 200, "метр");
        //    mwh.Add(mwhItem);
        //    mwhItem = new MainWarehouse(3, "Артикул 675", "Синий", 400, "метр");
        //    mwh.Add(mwhItem);
        //    mwhItem = new MainWarehouse(7, "Змейка 10см", "Синий", 60, "шт.");
        //    mwh.Add(mwhItem);
        //    mwhItem = new MainWarehouse(7, "Змейка 10см", "Светлый беж", 110, "шт.");
        //    mwh.Add(mwhItem);
        //    mwhItem = new MainWarehouse(7, "Змейка 10см", "Темный беж", 40, "шт.");
        //    mwh.Add(mwhItem);
        //    mwhItem = new MainWarehouse(7, "Змейка 10см", "Черный", 20, "шт.");
        //    mwh.Add(mwhItem);
        //    mwhItem = new MainWarehouse(7, "Змейка 80см", "Синий", 30, "шт.");
        //    mwh.Add(mwhItem);
        //    mwhItem = new MainWarehouse(7, "Змейка 80см", "Светлый беж", 60, "шт.");
        //    mwh.Add(mwhItem);
        //    mwhItem = new MainWarehouse(7, "Змейка 80см", "Темный беж", 20, "шт.");
        //    mwh.Add(mwhItem);
        //    mwhItem = new MainWarehouse(7, "Змейка 80см", "Черный", 20, "шт.");
        //    mwh.Add(mwhItem);
        //    mwhItem = new MainWarehouse(7, "Шнурок для капюшона", "Черный", 20, "шт.");
        //    mwh.Add(mwhItem);
        //    mwhItem = new MainWarehouse(7, "Шнурок для капюшона", "Синий", 30, "шт.");
        //    mwh.Add(mwhItem);
        //    mwhItem = new MainWarehouse(7, "Шнурок для капюшона", "Светлый беж", 50, "шт.");
        //    mwh.Add(mwhItem);
        //    mwhItem = new MainWarehouse(7, "Шнурок для капюшона", "Темный беж", 20, "шт.");
        //    mwh.Add(mwhItem);
        //    mwhItem = new MainWarehouse(7, "Кнопка скрытая", "метал", 400, "шт.");
        //    mwh.Add(mwhItem);
        //    mwhItem = new MainWarehouse(7, "Змейка 20см", "Синий", 30, "шт.");
        //    mwh.Add(mwhItem);
        //    mwhItem = new MainWarehouse(7, "Змейка 20см", "Светлый беж", 55, "шт.");
        //    mwh.Add(mwhItem);
        //    mwhItem = new MainWarehouse(7, "Змейка 20см", "Темный беж", 20, "шт.");
        //    mwh.Add(mwhItem);
        //    mwhItem = new MainWarehouse(7, "Змейка 20см", "Черный", 10, "шт.");
        //    mwh.Add(mwhItem);
        //    mwhItem = new MainWarehouse(6, "Нитка PolyArt", "Синий", 1000, "метр");
        //    mwh.Add(mwhItem);
        //    mwhItem = new MainWarehouse(6, "Нитка PolyArt", "Светлый беж", 1500, "метр");
        //    mwh.Add(mwhItem);
        //    mwhItem = new MainWarehouse(6, "Нитка PolyArt", "Темный беж", 700, "метр");
        //    mwh.Add(mwhItem);
        //    mwhItem = new MainWarehouse(6, "Нитка PolyArt", "Черный", 500, "метр");
        //    mwh.Add(mwhItem);
        //    mwhItem = new MainWarehouse(6, "Нитка-резинка 60", "Белый", 1000, "метр");
        //    mwh.Add(mwhItem);
        //    mwhItem = new MainWarehouse(6, "Бирка Prunel", "Черный", 1000, "шт.");
        //    mwh.Add(mwhItem);
        //    mwhItem = new MainWarehouse(6, "Бирка Made in", "Черный", 1000, "шт.");
        //    mwh.Add(mwhItem);
        //    mwhItem = new MainWarehouse(6, "Бирка № размер", "Белый", 1000, "шт.");
        //    mwh.Add(mwhItem);
        //    mwhItem = new MainWarehouse(6, "Бирка № швеи", "Белый", 1000, "шт.");
        //    mwh.Add(mwhItem);
        //    mwhItem = new MainWarehouse(6, "Бирка условия эксплуатации", "Белый", 1000, "шт.");
        //    mwh.Add(mwhItem);
        //    mwhItem = new MainWarehouse(6, "Бирка условия эксплуатации", "Белый", 1000, "шт.");
        //    mwh.Add(mwhItem);
        //    mwhItem = new MainWarehouse(8, "Чернобурка", "Чернобурка", 1000, "шт.");
        //    mwh.Add(mwhItem);
        //    mwhItem = new MainWarehouse(1, "Вика", "Синий", 100, "метр");
        //    mwh.Add(mwhItem);
        //    mwhItem = new MainWarehouse(1, "Вика", "Светлый беж", 200, "метр");
        //    mwh.Add(mwhItem);
        //    mwhItem = new MainWarehouse(1, "Вика", "Темный беж", 50, "метр");
        //    mwh.Add(mwhItem);
        //    mwhItem = new MainWarehouse(1, "Вика", "Черный", 50, "метр");
        //    mwh.Add(mwhItem);
        //    mwhItem = new MainWarehouse(4, "Силикон 100", "б/ц", 3000, "метр");
        //    mwh.Add(mwhItem);
        //    mwhItem = new MainWarehouse(9, "Пакеты 120", "б/ц", 500, "шт.");
        //    mwh.Add(mwhItem);
        //    mwhItem = new MainWarehouse(9, "Пакеты 100", "б/ц", 500, "шт.");
        //    mwh.Add(mwhItem);
        //    mwhItem = new MainWarehouse(9, "Ярлык Prunel", "Черный", 2500, "шт.");
        //    mwh.Add(mwhItem);
        //    mwhItem = new MainWarehouse(9, "Наклейка размер 44", "б/ц", 400, "шт.");
        //    mwh.Add(mwhItem);
        //    mwhItem = new MainWarehouse(9, "Наклейка размер 46", "б/ц", 400, "шт.");
        //    mwh.Add(mwhItem);
        //    mwhItem = new MainWarehouse(9, "Наклейка размер 48", "б/ц", 400, "шт.");
        //    mwh.Add(mwhItem);
        //    mwhItem = new MainWarehouse(9, "Наклейка размер 50", "б/ц", 400, "шт.");
        //    mwh.Add(mwhItem);
        //    mwhItem = new MainWarehouse(9, "Наклейка № модели 435", "б/ц", 2400, "шт.");
        //    mwh.Add(mwhItem);
        //    mwhItem = new MainWarehouse(9, "Наклейка № модели 431", "б/ц", 400, "шт.");
        //    mwh.Add(mwhItem);
        //    mwhItem = new MainWarehouse(9, "Пакеты 120", "б/ц", 500, "шт.");
        //    mwh.Add(mwhItem);
        //    mwhItem = new MainWarehouse(9, "Тремпель", "Черный", 5000, "шт.");
        //    mwh.Add(mwhItem);
        //    mwhItem = new MainWarehouse(5, "Артикул 331", "б/ц", 200, "метр");
        //    mwh.Add(mwhItem);
        //    mwhItem = new MainWarehouse(5, "Артикул 241", "б/ц", 200, "метр");
        //    mwh.Add(mwhItem);

        //    repository.populateMainWh(mwh);
        //}
        public JsonResult getRecipe()
        {
            List<Recipe> rcpList = new List<Recipe>();
            Recipe rcp = new Recipe(53, 2, 1, "Стежка Вика синий 100", 1, "м");
            rcpList.Add(rcp);
            rcp = new Recipe(92, 4, 1, "Стежка Вика синий 100", 1, "м");
            rcpList.Add(rcp);
            rcp = new Recipe(104, 5, 1, "Стежка Вика синий 100", 1, "м");
            rcpList.Add(rcp);
            rcp = new Recipe(76, 6, 1, "Стежка Вика синий 100", 6, "м");
            rcpList.Add(rcp);

            rcp = new Recipe(92, 4, 3, "Пальто Вика синий 100", 1, "м");
            rcp.RecipeId = 1;
            rcpList.Add(rcp);
            rcp = new Recipe(53, 2, 3, "Пальто Вика синий 100", 1, "м");
            rcp.RecipeId = 1;
            rcpList.Add(rcp);
            rcp = new Recipe(76, 6, 3, "Стежка Вика синий 100", 6, "м");
            rcp.RecipeId = 1;
            rcpList.Add(rcp);
            rcp = new Recipe(104, 5, 3, "Пальто Вика синий 100", 1, "м");
            rcp.RecipeId = 1;
            rcpList.Add(rcp);
            rcp = new Recipe(57, 3, 3, "Пальто Вика синий 100", 1, "м");
            rcp.RecipeId = 1;
            rcpList.Add(rcp);
            rcp = new Recipe(88, 1, 3, "Пальто Вика синий 100", 1, "м");
            rcp.RecipeId = 1;
            rcpList.Add(rcp);
            rcp = new Recipe(59, 7, 3, "Пальто Вика синий 100", 2, "шт.");
            rcp.RecipeId = 1;
            rcpList.Add(rcp);
            rcp = new Recipe(63, 7, 3, "Пальто Вика синий 100", 1, "шт.");
            rcp.RecipeId = 1;
            rcpList.Add(rcp);
            rcp = new Recipe(68, 7, 3, "Пальто Вика синий 100", 1, "шт.");
            rcp.RecipeId = 1;
            rcpList.Add(rcp);
            rcp = new Recipe(93, 9, 3, "Пальто Вика синий 100", 1, "шт.");
            rcp.RecipeId = 1;
            rcpList.Add(rcp);
            rcp = new Recipe(103, 9, 3, "Пальто Вика синий 100", 1, "шт.");
            rcp.RecipeId = 1;
            rcpList.Add(rcp);

            var result =
                from item in rcpList
                orderby item.ItemCategory
                group item by item.RecipeId into newGroup
                orderby newGroup.Key
                select newGroup;

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getRecipesCategories()
        {
            int[] array = { 1, 10, 11, 77 };

            return Json(array, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getFirstRcpByCategory(int cat)
        {
            List<Recipe> rcpList = new List<Recipe>();
            Recipe rcp = new Recipe(53, 2, 1, "Стежка Вика синий 100", 1, "м");
            rcpList.Add(rcp);
            rcp = new Recipe(92, 4, 1, "Стежка Вика синий 100", 1, "м");
            rcpList.Add(rcp);
            rcp = new Recipe(104, 5, 1, "Стежка Вика синий 100", 1, "м");
            rcpList.Add(rcp);
            rcp = new Recipe(76, 6, 1, "Стежка Вика синий 100", 6, "м");
            rcpList.Add(rcp);

            rcp = new Recipe(92, 4, 11, "Пальто Вика синий 100", 1, "м");
            rcp.RecipeId = 1;
            rcpList.Add(rcp);
            rcp = new Recipe(53, 2, 11, "Пальто Вика синий 100", 1, "м");
            rcp.RecipeId = 1;
            rcpList.Add(rcp);
            rcp = new Recipe(76, 6, 11, "Стежка Вика синий 100", 6, "м");
            rcp.RecipeId = 1;
            rcpList.Add(rcp);
            rcp = new Recipe(104, 5, 11, "Пальто Вика синий 100", 1, "м");
            rcp.RecipeId = 1;
            rcpList.Add(rcp);
            rcp = new Recipe(57, 3, 11, "Пальто Вика синий 100", 1, "м");
            rcp.RecipeId = 1;
            rcpList.Add(rcp);
            rcp = new Recipe(88, 1, 11, "Пальто Вика синий 100", 1, "м");
            rcp.RecipeId = 1;
            rcpList.Add(rcp);
            rcp = new Recipe(59, 7, 11, "Пальто Вика синий 100", 2, "шт.");
            rcp.RecipeId = 1;
            rcpList.Add(rcp);
            rcp = new Recipe(63, 7, 11, "Пальто Вика синий 100", 1, "шт.");
            rcp.RecipeId = 1;
            rcpList.Add(rcp);
            rcp = new Recipe(68, 7, 11, "Пальто Вика синий 100", 1, "шт.");
            rcp.RecipeId = 1;
            rcpList.Add(rcp);
            rcp = new Recipe(93, 9, 11, "Пальто Вика синий 100", 1, "шт.");
            rcp.RecipeId = 1;
            rcpList.Add(rcp);
            rcp = new Recipe(103, 9, 11, "Пальто Вика синий 100", 1, "шт.");
            rcp.RecipeId = 1;
            rcpList.Add(rcp);

            
            IEnumerable<Recipe> result;          

            try
            {
                result = rcpList.Where(x => x.RecipeCategory == cat);
            }
            catch (Exception)
            {
                
                throw;
            }
            
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public void postTaskData(ProductionTask d)
        {
            repository.AddProductionTaskToDb(d);
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