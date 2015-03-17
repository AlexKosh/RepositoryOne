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

            if (Session["OrderList"] != null)
            {
                orderList = (List<WarehouseItem>)Session["OrderList"];
            }
                        
            var itemToAdd = orderList.FirstOrDefault(x => x.ItemNumber == model && x.Color == color && x.Size == size);

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
   
            Session["OrderList"] = orderList;
            Session["OrderToView"] = repository.MakeItemVM(orderList, 
                new List<int>(orderList.Select(x => x.ItemNumber).Distinct()),
                dbModel.itemSizes,
                dbModel.itemColors);

            string order = string.Format("Picked: {0} {1} {2}", model, color, size);
            Session["OrderExamWh"] = order;
            return RedirectToAction("Index");
        }

        public ActionResult MoveItemsToStore()
        {
            repository.MoveItemsFromWhToSt((List<WarehouseItem>)Session["OrderList"]);
            Session["OrderList"] = null;
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