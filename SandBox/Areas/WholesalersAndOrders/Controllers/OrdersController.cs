using SandBox.Abstract;
using SandBox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SandBox.Areas.WholesalersAndOrders.Controllers
{
    public class OrdersController : Controller
    {
        private IItemRepository repository;
        public OrdersController(IItemRepository repo)
        {
            this.repository = repo;
        }
        ItemVM<StoreItem> dbModelOrders = new ItemVM<StoreItem>();
        List<WarehouseItem> ordersList = new List<WarehouseItem>();
        ItemVM<WarehouseItem> dbModelWarehouse = new ItemVM<WarehouseItem>();

        // GET: WholesalersAndOrders/Orders
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddOrder()
        {
            dbModelOrders = repository.MakeItemVM(repository.IEStoreItems);
            return View(dbModelOrders);
        }

        [HttpPost]
        public ActionResult AddOrder(string telNo)
        {
            TempData["Success"] = telNo;
            dbModelOrders = repository.MakeItemVM(repository.IEStoreItems);
            return View(dbModelOrders);
        }

        public ActionResult PickModelNumber(int itemNumb)
        {
            Session["pickedModelNumber"] = itemNumb;
            if (Session["dbModelWarehouse"] == null)
            {
                Session["dbModelWarehouse"] = repository.MakeItemVM(repository.IEWarehouseItems);
            }
            
            string returnUrl = Request.UrlReferrer.AbsolutePath;
            TempData["Success"] = string.Format("{0} {1}", itemNumb, returnUrl);
            return Redirect(returnUrl);
        }

        public ActionResult PickItem(int model, string color, int size, int amount)
        {
            if (Session["dbModelWarehouse"] == null)
            {
                Session["dbModelWarehouse"] = dbModelWarehouse = repository.MakeItemVM(repository.IEWarehouseItems);
            }
            else
            {
                dbModelWarehouse = (ItemVM<WarehouseItem>)Session["dbModelWarehouse"];
            }

            if (Session["OrdersList"] != null)
            {
                ordersList = (List<WarehouseItem>)Session["OrdersList"];
            }

            var itemToAdd = ordersList.FirstOrDefault(x => x.ItemNumber == model && x.Color == color && x.Size == size);

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
                    ordersList.Add(new WarehouseItem
                    {
                        ItemNumber = model,
                        Color = color,
                        Size = size,
                        Quantity = 1
                    });
                }

            }

            Session["OrdersList"] = ordersList;
            Session["OrdersListToView"] = repository.MakeItemVM(ordersList,
                new List<int>(ordersList.Select(x => x.ItemNumber).Distinct()),
                dbModelOrders.itemSizes,
                dbModelOrders.itemColors);

            string returnUrl = Request.UrlReferrer.AbsolutePath;

            return Redirect(returnUrl);
        }
    }
}