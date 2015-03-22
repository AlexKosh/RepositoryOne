using SandBox.Abstract;
using SandBox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SandBox.Areas.Store.Controllers
{
    public class OnStoresStockController : Controller
    {
        private IItemRepository repository;
        public OnStoresStockController(IItemRepository repo)
        {
            this.repository = repo;
        }
        ItemVM<StoreItem> StoreWhModel = new ItemVM<StoreItem>();
        List<StoreItem> orderList = new List<StoreItem>();

        // GET: Store/OnStoresStock
        public ActionResult Index()
        {
            try
            {
                repository.IEStoreItems.First();
            }
            catch (Exception)
            {
                repository.Populate();
                TempData["Success"] = "Db populated";
            }

            if (Session["StoreWhModel"] == null)
            {
                Session["StoreWhModel"] = StoreWhModel = repository.MakeItemVM(repository.IEStoreItems);
            }
            else
            {
                StoreWhModel = (ItemVM<StoreItem>)Session["StoreWhModel"];
            }
            return View(StoreWhModel);
        }

        public ActionResult PickItem(int model, string color, int size, int amount)
        {
            if (Session["StoreWhModel"] == null)
            {
                Session["StoreWhModel"] = StoreWhModel = repository.MakeItemVM(repository.IEStoreItems);
            }
            else
            {
                StoreWhModel = (ItemVM<StoreItem>)Session["StoreWhModel"];
            }

            if (Session["OrderListStore"] != null)
            {
                orderList = (List<StoreItem>)Session["OrderListStore"];
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
                    orderList.Add(new StoreItem
                    {
                        ItemNumber = model,
                        Color = color,
                        Size = size,
                        Quantity = 1
                    });
                }

            }

            Session["OrderListStore"] = orderList;
            Session["OrderToViewStore"] = repository.MakeItemVM(orderList,
                new List<int>(orderList.Select(x => x.ItemNumber).Distinct()),
                StoreWhModel.itemSizes,
                StoreWhModel.itemColors);

            string stringUrl = Request.UrlReferrer.AbsolutePath;

            return Redirect(stringUrl);
        }
        public ActionResult DeleteFromOrder(int model, string color, int size)
        {
            if (Session["OrderToViewStore"] != null)
            {
                var itemToDelete = ((ItemVM<StoreItem>)Session["OrderToViewStore"])
                .itemsList[model].Find(x => x.Color == color && x.Size == size);
                ((ItemVM<StoreItem>)Session["OrderToViewStore"]).itemsList[model].Remove(itemToDelete);
                ((List<StoreItem>)Session["OrderListStore"]).Remove(itemToDelete);
            } 
            string stringUrl = Request.UrlReferrer.AbsolutePath;
            return Redirect(stringUrl);
        }

        public ActionResult ClearOrderList()
        {
            Session["OrderListStore"] = null;
            Session["OrderToViewStore"] = null;

            string stringUrl = Request.UrlReferrer.AbsolutePath;
            return Redirect(stringUrl);
        }
    }
}