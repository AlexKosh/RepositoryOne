using Josephine.Abstract;
using Josephine.Concrete;
using Josephine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Josephine.Controllers
{    
    public class ProductionController : Controller
    {
        private const int QUILTING_CAT = 1, CUTTING_CAT = 11;

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


        //public void pplRecipe()
        //{
        //    Recipe rcp = new Recipe();
        //    rcp.Name = "Стежка Вика светлый беж 100";
        //    rcp.RecipeCategory = 1;
        //    rcp.ResultItemId = 88;
        //    rcp.ResultName = "Вика";
        //    rcp.ResultQuantity = 1;
        //    rcp.UnitsOfMeasurement = "м";
        //    //rcp.RecipeId = 0;
        //    List<RecipeItem> rcpList = new List<RecipeItem>();
        //    RecipeItem rItem = new RecipeItem();
        //    rItem.ItemId = 53;
        //    rItem.ItemCategory = 2;
        //    //rItem.RecipeId = rcp.RecipeId;
        //    rItem.Name = "Плащевка";
        //    rItem.UnitsOfMeasurement = "м";
        //    rItem.Quantity = -1;
        //    rcpList.Add(rItem);

        //    rItem = new RecipeItem();
        //    rItem.ItemId = 92;
        //    rItem.ItemCategory = 4;
        //    //rItem.RecipeId = rcp.RecipeId;
        //    rItem.Name = "Силикон 100";
        //    rItem.UnitsOfMeasurement = "м";
        //    rItem.Quantity = -1;
        //    rcpList.Add(rItem);

        //    rItem = new RecipeItem();
        //    rItem.ItemId = 104;
        //    rItem.ItemCategory = 5;
        //    //rItem.RecipeId = rcp.RecipeId;
        //    rItem.Name = "Артикул 331";
        //    rItem.UnitsOfMeasurement = "м";
        //    rItem.Quantity = -1;
        //    rcpList.Add(rItem);

        //    rItem = new RecipeItem();
        //    rItem.ItemId = 76;
        //    rItem.ItemCategory = 6;
        //    rItem.Name = "Нитка PolyArt";
        //    //rItem.RecipeId = rcp.RecipeId;
        //    rItem.UnitsOfMeasurement = "м";
        //    rItem.Quantity = -6;
        //    rcpList.Add(rItem);                        

        //    rcp.RecipeItems = rcpList;

        //    repository.AddRecipeToDb(rcp);
        //}
        public JsonResult getRecipes()
        {
            List<Recipe> result = new List<Recipe>();
            using (EFDbContext ct = new EFDbContext())
            {
                result = ct.Recipe.Include("RecipeItems").ToList();
            }                       

            //var result =
            //    from item in repository.Recipes
            //    orderby item.ItemCategory
            //    group item by item.RecipeId into newGroup
            //    orderby newGroup.Key
            //    select newGroup;

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult getFirstRcpByCategory(int cat)
        {
            if (cat != 1)
            {
                int[] r = new int[0];
                return Json(r, JsonRequestBehavior.AllowGet);
            }
            IEnumerable<RecipeItem> result;

            try
            {
                int recipeId = repository.Recipes.Where(x => x.RecipeCategory == cat).Select(x => x.RecipeId).First();
                result = repository.RecipeItems.Where(x => x.RecipeId == recipeId);
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
        public void postRecipeData(Recipe d)
        {
            repository.AddRecipeToDb(d);
        }
        public void postItem(MainWarehouse d)
        {

            repository.AddMainWhItemToDb(d);
        }
        public void postCut(Cut d)
        {
            repository.AddCutToDb(d);
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
        public JsonResult getTasksForQuilting()
        {
            List<ProductionTask> result = new List<ProductionTask>();

            ProductionTask runningTask = repository.ProductionTasks.Where(x =>
                x.TaskCategory == QUILTING_CAT
                && x.isCompleted == 1)
                .FirstOrDefault();

            if (runningTask != null)
            {
                ProductionTask nextTask = repository.ProductionTasks.Where(x => x.TaskCategory == QUILTING_CAT
                    && x.isCompleted == 0)
                    .OrderBy(x => x.Priority)
                    .First();
                result.Add(runningTask);
                result.Add(nextTask);
            }
            else
            {
                result = repository.ProductionTasks.Where(x => x.TaskCategory == QUILTING_CAT)
                .OrderBy(x => x.Priority)
                .Select(x => x).Take(2).ToList();
            }
                        

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getCuttingRecipes()
        {
            List<Recipe> result = new List<Recipe>();
            using (EFDbContext ct = new EFDbContext())
            {
                result = ct.Recipe.Include("RecipeItems").Where(x => x.RecipeCategory == CUTTING_CAT).ToList();
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public void postCuttingTask(ProductionTask d)
        {
            repository.AddProductionTaskToDb(d);
        }
    }
}