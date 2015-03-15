using SandBox.Abstract;
using SandBox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SandBox.Controllers
{
    public class OrderController : Controller
    {
        public List<WarehouseItem> orders = new List<WarehouseItem>() 
        {
            new WarehouseItem { ItemNumber = 417, Name = "Vika", Color = "Blue", Size = 44, Price = 680, Quantity = 1344 },
            new WarehouseItem { ItemNumber = 417, Name = "Vika", Color = "Blue", Size = 48, Price = 680, Quantity = 1348 },
            new WarehouseItem { ItemNumber = 417, Name = "Vika", Color = "Pearl", Size = 48, Price = 680, Quantity = 448 },
            new WarehouseItem { ItemNumber = 417, Name = "Vika", Color = "Blue", Size = 56, Price = 680, Quantity = 1356 },
            new WarehouseItem { ItemNumber = 423, Name = "Vera", Color = "Pearl", Size = 56, Price = 710, Quantity = 456 },
            new WarehouseItem { ItemNumber = 417, Name = "Vika", Color = "Black", Size = 46, Price = 680, Quantity = 13146 },
            new WarehouseItem { ItemNumber = 423, Name = "Vera", Color = "Pearl", Size = 48, Price = 710, Quantity = 448 },
            new WarehouseItem { ItemNumber = 423, Name = "Vera", Color = "Blue", Size = 56, Price = 710, Quantity = 1356 },
            new WarehouseItem { ItemNumber = 423, Name = "Vera", Color = "Black", Size = 46, Price = 710, Quantity = 13146 },
            new WarehouseItem { ItemNumber = 423, Name = "Vera", Color = "Pearl", Size = 52, Price = 710, Quantity = 452 },
            new WarehouseItem { ItemNumber = 431, Name = "Nadia", Color = "Pearl", Size = 52, Price = 740, Quantity = 452 },
            new WarehouseItem { ItemNumber = 431, Name = "Nadia", Color = "Pearl", Size = 54, Price = 740, Quantity = 0454 }
        };
        
        private IItemRepository repository;
        public OrderController(IItemRepository repo)
        {
            this.repository = repo;
        }

        ItemVM dbModel = new ItemVM();
        ItemVM ordersModel = new ItemVM();
        WarehouseItem selectedItem = new WarehouseItem();

        public ActionResult Index()
        {
            if (Session["Ord"] != null)
            {
                 orders = (List<WarehouseItem>)Session["Ord"];
            }            
            dbModel = repository.MakeItemVM(repository.IEWarehouseItems);
            ordersModel = repository.MakeItemVM(orders, dbModel.itemNumbers, dbModel.itemSizes, dbModel.itemColors);

            return View(ordersModel);
        }               
        
        public RedirectToRouteResult SelectModel(int id = 0)
        {
            Session["SelectedModelNumber"] = id;      
            return RedirectToAction("Index");
        }

        public RedirectToRouteResult SelectColor(string id = "")
        {
            Session["SelectedColor"] = id;
            return RedirectToAction("Index");
        }
        public RedirectToRouteResult SelectSize(int id = 0)
        {
            Session["SelectedSize"] = id;

            if (Session["Ord"] != null)
            {
                orders = (List<WarehouseItem>)Session["Ord"];
            } 

            orders.Add(new WarehouseItem
            {
                ItemNumber = (int)Session["SelectedModelNumber"],
                Color = Session["SelectedColor"].ToString(),
                Size = id,
                Quantity = 1
            });
            Session["Ord"] = orders;
            return RedirectToAction("Index");
        }  
    }
}