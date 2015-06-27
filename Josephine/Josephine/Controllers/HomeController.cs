using Josephine.Abstract;
using Josephine.Models;
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
        
        public ActionResult Index()
        {
            if (false)
            {
                repository.populate(
                    417,
                    "Vika",
                    new string[] { "Black", "Blue", "Dark Biege", "Pearl" },
                    new int[] { 44, 46, 48, 50, 52, 54, 56, 58, 60 },
                    680);

                repository.populate(
                    423,
                    "Vera",
                    new string[] { "Black", "Blue", "Dark Biege", "Pearl" },
                    new int[] { 44, 46, 48, 50, 52, 54, 56, 58 },
                    710);

                repository.populate(
                    431,
                    "Nadya",
                    new string[] { "Blue", "Dark Biege", "Pearl" },
                    new int[] { 44, 46, 48, 50, 52, 54 },
                    740);

                repository.populate(
                    403,
                    "Nika",
                    new string[] { "Main", "BlueBerry", "Terra", "Lilac" },
                    new int[] { 48, 50, 52, 54, 56, 58 },
                    185);
            }
            return View();
        }
        public JsonResult Store()
        {  
            //local variable for decrease requests to db, contains all db data from table 'Products'
            var tempDbData = repository.Store;
            //initialize variable that will returns
            DataNotation data = new DataNotation();
            //local temp variable that helps to compose 'data'
            Annotation notation = new Annotation();

            List<IEnumerable<Product>> tempModelData = new List<IEnumerable<Product>>();
                        
            //This list helps us to make a structure of the data, list contains all model numbers
            List<int> modelNumbers = tempDbData.Select(x => x.ModelNumber).Distinct().ToList();            
            
            //this loop makes structured data for every model that we have
            foreach (int model in modelNumbers)
            {
                //Collection of product per one modelNumber, for example, it may be all products of 417 modelNumber
                //It is a temp local variable that have created for dercrease LINQ-requests to 'tempDbData' 
                var tempModel = tempDbData.Where(x => x.ModelNumber == model).Select(x => x);

                //Collection of collors for this model                
                notation.Colors = tempModel.Select(x => x.Color).Distinct().OrderBy(x => x).ToList();
                notation.Sizes = tempModel.Select(x => x.Size).Distinct().ToList();
                data.DataNotations.Add(model.ToString(), notation);
                                
                foreach (string color in notation.Colors)
                {                    
                    var tempModelDataPerColor = tempModel.Where(x => x.Color == color).Select(x => x).OrderBy(x => x.Size);
                    tempModelData.Add(tempModelDataPerColor);
                }

                data.Data.Add(tempModelData);
                tempModelData = new List<IEnumerable<Product>>();
                notation = new Annotation();                                
            }
            
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Warehouse()
        {
            //local variable for decrease requests to db, contains all db data from table 'Products'
            var tempDbData = repository.Warehouse;
            //initialize variable that will returns
            DataNotation data = new DataNotation();
            //local temp variable that helps to compose 'data'
            Annotation notation = new Annotation();

            List<IEnumerable<Product>> tempModelData = new List<IEnumerable<Product>>();

            //This list helps us to make a structure of the data, list contains all model numbers
            List<int> modelNumbers = tempDbData.Select(x => x.ModelNumber).Distinct().ToList();

            //this loop makes structured data for every model that we have
            foreach (int model in modelNumbers)
            {
                //Collection of product per one modelNumber, for example, it may be all products of 417 modelNumber
                //It is a temp local variable that have created for dercrease LINQ-requests to 'tempDbData' 
                var tempModel = tempDbData.Where(x => x.ModelNumber == model).Select(x => x);

                //Collection of collors for this model                
                notation.Colors = tempModel.Select(x => x.Color).Distinct().OrderBy(x => x).ToList();
                notation.Sizes = tempModel.Select(x => x.Size).Distinct().ToList();
                data.DataNotations.Add(model.ToString(), notation);

                foreach (string color in notation.Colors)
                {
                    var tempModelDataPerColor = tempModel.Where(x => x.Color == color).Select(x => x).OrderBy(x => x.Size);
                    tempModelData.Add(tempModelDataPerColor);
                }

                data.Data.Add(tempModelData);
                tempModelData = new List<IEnumerable<Product>>();
                notation = new Annotation();
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Orders()
        {
            OrderData data = new OrderData(repository.OrderInfo, repository.OrderProduct);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Customers()
        {
            return Json(repository.Customers, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Employees()
        {
            return Json(repository.Employees, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Prices()
        {
            return Json(repository.Prices, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult leftTable()
        {
            return PartialView();
        }
        public PartialViewResult rightTable()
        {
            return PartialView();
        }
        public PartialViewResult createOrder()
        {
            return PartialView();
        }

        [HttpPost]
        public JsonResult processOrder(OrderData d)
        {
            OrderData nd = new OrderData();
            nd.OrderProduct = d.OrderProduct.Where(x => x.Quantity > 1).Select(x => x);
            d.OrderProduct = nd.OrderProduct;
            return Json(d, JsonRequestBehavior.AllowGet);
        }

        public class DataNotation
        {            
            public DataNotation()
            {
                Data = new List<List<IEnumerable<Product>>>();
                DataNotations = new Dictionary<string, Annotation>();                
            }
            public List<List<IEnumerable<Product>>> Data { get; set; }
            public Dictionary<string, Annotation> DataNotations { get; set; }                       
        }
        public class Annotation
        {
            public Annotation()
            {                
                Colors = new List<string>();
                Sizes = new List<int>();
            }            
            public List<string> Colors { get; set; }
            public List<int> Sizes { get; set; }
        }
        public class OrderData
        {
            public OrderData()
            {
                OrderInfo = new List<OrderInfo>();
                OrderProduct = new List<OrderProduct>();
            }
            public OrderData(IEnumerable<OrderInfo> oi, IEnumerable<OrderProduct> op)
            {
                OrderInfo = oi;
                OrderProduct = op;
            }
            public IEnumerable<OrderInfo> OrderInfo { get; set; }
            public IEnumerable<OrderProduct> OrderProduct { get; set; }
        }
    }
}