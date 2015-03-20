using SandBox.Abstract;
using SandBox.Areas.Warehouse.Models;
using SandBox.Models;
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

        List<WarehouseItem> orderList = new List<WarehouseItem>();
        ItemVM<WarehouseItem> orderVM = new ItemVM<WarehouseItem>();
        ItemVM<WarehouseItem> dbModel = new ItemVM<WarehouseItem>();
        ItemVM<TailoringItem> tailoringVM = new ItemVM<TailoringItem>();

        public ActionResult Index()
        {
            dbModel = repository.MakeItemVM(repository.IEWarehouseItems);
            Session["dbModel"] = dbModel;
            return View(dbModel);
        }

        public ActionResult PickItem(int model, string color, int size, int amount)
        {
            dbModel = (ItemVM<WarehouseItem>)Session["dbModel"];            

            if (Session["OrderList"] != null)
            {
                orderList = (List<WarehouseItem>)Session["OrderList"];
            }
            
            var itemToAdd = orderList.FirstOrDefault(x => x.ItemNumber == model && x.Color == color && x.Size == size);

            if (amount <= 0)
            {
                TempData["Error"] = "This item what you've picked is ended!";
            }
            else
            {
                if (itemToAdd != null)
                {
                    itemToAdd.Quantity += 1;
                }
                else
                {
                    orderList.Add(new WarehouseItem
                    {
                        ItemNumber = model,
                        Color = color,
                        Size = size,
                        Quantity = 1
                    });
                }

            }            

            Session["OrderList"] = orderList;
            Session["OrderToView"] = repository.MakeItemVM(orderList,
                new List<int>(orderList.Select(x => x.ItemNumber).Distinct()),
                dbModel.itemSizes,
                dbModel.itemColors); 

            string stringUrl = Request.UrlReferrer.AbsolutePath;
  
            return Redirect(stringUrl);
        }

        public ActionResult MoveItemsToStore()
        {
            if (Session["OrderList"] == null || ((List<WarehouseItem>)Session["OrderList"]).Count == 0)
            {
                TempData["Error"] = "Order list is empty!";
            }
            else
            {
                repository.MoveItemsFromWhToSt((List<WarehouseItem>)Session["OrderList"]);
                Session["OrderList"] = null;
            }            
            return RedirectToAction("Index");
        }

        public ActionResult MoveItemsToWh()
        {
            if (Session["OrderList"] == null || ((List<WarehouseItem>)Session["OrderList"]).Count == 0)
            {
                TempData["Error"] = "Order list is empty!";
            }
            else
            {
                repository.MoveItemsFromTlrgToWh((List<WarehouseItem>)Session["OrderList"]);
                Session["OrderList"] = null;
            }
            return RedirectToAction("Index");
        }

        public ActionResult DeleteFromOrder(int model, string color, int size)
        {
            if (Session["OrderToView"] != null)
            {
                var itemToDelete = ((ItemVM<WarehouseItem>)Session["OrderToView"])
                .itemsList[model].Find(x => x.Color == color && x.Size == size);
                ((ItemVM<WarehouseItem>)Session["OrderToView"]).itemsList[model].Remove(itemToDelete);
                ((List<WarehouseItem>)Session["OrderList"]).Remove(itemToDelete);
            }   
            return RedirectToAction("Index");
        }

        public ActionResult ClearOrderList()
        {
            Session["OrderList"] = null;
            Session["OrderToView"] = null;

            string stringUrl = Request.UrlReferrer.AbsolutePath;
            return Redirect(stringUrl);
        }

        [HttpGet]
        public ActionResult AddToTailoringWh( int itemNumber = 0, string color = "" )
        {            
            return View();
        }
        [HttpPost]
        public ActionResult AddToTailoringWh(int itemNumber = 0, string color = "",
            int size = 0, int quantity = 0 )
        {
            TempData["itemNumber"] = itemNumber;
            TempData["color"] = color;
            TempData["quantity"] = quantity;

            TailoringItem itemtoAdd = new TailoringItem()
            {
                ItemNumber = itemNumber,
                Color = color,
                Size = size,
                Quantity = quantity
            };

            repository.SaveItemtoDB(itemtoAdd);            
            
            return View();
        }
        public ActionResult TailoringWarehouse()
        {
            if (Session["dbModel"] == null)
            {
                dbModel = repository.MakeItemVM(repository.IEWarehouseItems);
            }
            else
            {
                dbModel = (ItemVM<WarehouseItem>)Session["dbModel"];
            }

            tailoringVM = repository.MakeItemVM(repository.IETailoringItem, dbModel.itemNumbersFull, 
                dbModel.itemSizes, dbModel.itemColors);
            return View(tailoringVM);
        }
    }
}