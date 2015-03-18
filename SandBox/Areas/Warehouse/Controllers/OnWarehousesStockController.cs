using SandBox.Abstract;
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

        public ActionResult Index()
        {
            dbModel = repository.MakeItemVM(repository.IEWarehouseItems);
            Session["dbModel"] = dbModel;
            return View(dbModel);
        }

        public ActionResult PickItem(int model, string color, int size)
        {
            dbModel = (ItemVM<WarehouseItem>)Session["dbModel"];
            var targetItemInWh = dbModel.itemsList[model]
                .First(x => x.ItemNumber == model && x.Color == color && x.Size == size);

            if (Session["OrderList"] != null)
            {
                orderList = (List<WarehouseItem>)Session["OrderList"];
            }
            var itemToAdd = orderList.FirstOrDefault(x => x.ItemNumber == model && x.Color == color && x.Size == size);

            if (targetItemInWh.Quantity > 0)
            {
                if (itemToAdd != null)
                {
                    if ((targetItemInWh.Quantity - itemToAdd.Quantity) > 0)
                    {
                        itemToAdd.Quantity += 1;
                    }
                    else
                    {
                        TempData["Error"] = "This item what you've picked is ended!";
                    }
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

                Session["OrderList"] = orderList;
                Session["OrderToView"] = repository.MakeItemVM(orderList,
                    new List<int>(orderList.Select(x => x.ItemNumber).Distinct()),
                    dbModel.itemSizes,
                    dbModel.itemColors);                
            }
            else
            {
                TempData["Error"] = "This item what you've picked is ended!";
            }          
  
            return RedirectToAction("Index");
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
    }
}