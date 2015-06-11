﻿using Josephine.Abstract;
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
            //repository.populate();
            return View();
        }
        public JsonResult Products()
        {  
            //local variable for decrease requests to db, contains all db data from table 'Products'
            var tempDbData = repository.Products;
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
                                
            }
            
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        private class DataNotation
        {            
            public DataNotation()
            {
                Data = new List<List<IEnumerable<Product>>>();
                DataNotations = new Dictionary<string, Annotation>();                
            }
            public List<List<IEnumerable<Product>>> Data { get; set; }
            public Dictionary<string, Annotation> DataNotations { get; set; }                       
        }
        private class Annotation
        {
            public Annotation()
            {                
                Colors = new List<string>();
                Sizes = new List<int>();
            }            
            public List<string> Colors { get; set; }
            public List<int> Sizes { get; set; }
        }
    }
}