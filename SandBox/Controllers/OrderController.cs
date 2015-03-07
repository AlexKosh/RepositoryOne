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
        public List<WarehouseItem> orders = new List<WarehouseItem>();
        
        private IItemRepository repository;
        public OrderController(IItemRepository repo)
        {
            this.repository = repo;
        }

        ItemVM dbModel = new ItemVM();
        ItemVM ordersModel = new ItemVM();

        public ActionResult Index()
        {
            if (Session["BtnOrderVMSession"] != null)
            {
                btnOrdVMSes = (BtnsOrderVM)Session["BtnOrderVMSession"];
            }
            orders.Add(new WarehouseItem { ItemNumber = 417, Name = "Vika", Color = "Blue", Size = 44, Price = 680, Quantity = 1344 });
            orders.Add(new WarehouseItem { ItemNumber = 417, Name = "Vika", Color = "Blue", Size = 48, Price = 680, Quantity = 1348 });
            orders.Add(new WarehouseItem { ItemNumber = 417, Name = "Vika", Color = "Pearl", Size = 48, Price = 680, Quantity = 448 });
            orders.Add(new WarehouseItem { ItemNumber = 417, Name = "Vika", Color = "Blue", Size = 56, Price = 680, Quantity = 1356 });
            orders.Add(new WarehouseItem { ItemNumber = 423, Name = "Vera", Color = "Pearl", Size = 56, Price = 710, Quantity = 456 });
            orders.Add(new WarehouseItem { ItemNumber = 417, Name = "Vika", Color = "Black", Size = 46, Price = 680, Quantity = 13146 });
            orders.Add(new WarehouseItem { ItemNumber = 423, Name = "Vera", Color = "Pearl", Size = 48, Price = 710, Quantity = 448 });
            orders.Add(new WarehouseItem { ItemNumber = 423, Name = "Vera", Color = "Blue", Size = 56, Price = 710, Quantity = 1356 });
            orders.Add(new WarehouseItem { ItemNumber = 423, Name = "Vera", Color = "Black", Size = 46, Price = 710, Quantity = 13146 });
            orders.Add(new WarehouseItem { ItemNumber = 423, Name = "Vera", Color = "Pearl", Size = 52, Price = 710, Quantity = 452 });
            orders.Add(new WarehouseItem { ItemNumber = 431, Name = "Nadia", Color = "Pearl", Size = 52, Price = 740, Quantity = 452 });
            orders.Add(new WarehouseItem { ItemNumber = 431, Name = "Nadia", Color = "Pearl", Size = 54, Price = 740, Quantity = 0454 });


            dbModel = repository.MakeItemVM(repository.IEWarehouseItems);
            ordersModel = repository.MakeItemVM(orders, dbModel.itemNumbers, dbModel.itemSizes, dbModel.itemColors, btnOrdVMSes);

            return View(ordersModel);
        }

        //public ActionResult AddToOrder()
        //{
        //    orders.Add(new WarehouseItem 
        //    { ItemNumber = 417, Name = "Vika", Color = "Blue", Size = 44, Price = 680, Quantity = 1344 });
        //    orders.Add(new WarehouseItem 
        //    { ItemNumber = 417, Name = "Vika", Color = "Blue", Size = 48, Price = 680, Quantity = 1348 });
        //    orders.Add(new WarehouseItem 
        //    { ItemNumber = 417, Name = "Vika", Color = "Pearl", Size = 48, Price = 680, Quantity = 448 });
        //    orders.Add(new WarehouseItem 
        //    { ItemNumber = 417, Name = "Vika", Color = "Blue", Size = 56, Price = 680, Quantity = 1356 });
        //    orders.Add(new WarehouseItem 
        //    { ItemNumber = 423, Name = "Vera", Color = "Pearl", Size = 56, Price = 710, Quantity = 456 });
        //    orders.Add(new WarehouseItem 
        //    { ItemNumber = 417, Name = "Vika", Color = "Black", Size = 46, Price = 680, Quantity = 13146 });
        //    orders.Add(new WarehouseItem
        //    { ItemNumber = 423, Name = "Vera", Color = "Pearl", Size = 48, Price = 710, Quantity = 448 });
        //    orders.Add(new WarehouseItem
        //    { ItemNumber = 423, Name = "Vera", Color = "Blue", Size = 56, Price = 710, Quantity = 1356 });
        //    orders.Add(new WarehouseItem
        //    { ItemNumber = 423, Name = "Vera", Color = "Black", Size = 46, Price = 710, Quantity = 13146 });
        //    orders.Add(new WarehouseItem
        //    { ItemNumber = 423, Name = "Vera", Color = "Pearl", Size = 52, Price = 710, Quantity = 452 });

        //    dbModel = repository.MakeItemVM(repository.IEWarehouseItems);
        //    ordersModel = repository.MakeItemVM(orders, dbModel.itemNumbers, dbModel.itemSizes, dbModel.itemColors, dbModel.BtnsOrderVM);

        //    return View("Index", ordersModel);
        //}
               
        BtnsOrderVM btnOrdVMSes = new BtnsOrderVM();


        public ActionResult SelectItem(int itemNumber)
        {
            if (Session["BtnOrderVMSession"] != null)
            {
                btnOrdVMSes = (BtnsOrderVM)Session["BtnOrderVMSession"];
            }
            
            orders.Add(new WarehouseItem { ItemNumber = 417, Name = "Vika", Color = "Blue", Size = 44, Price = 680, Quantity = 1344 });
            orders.Add(new WarehouseItem { ItemNumber = 417, Name = "Vika", Color = "Blue", Size = 48, Price = 680, Quantity = 1348 });
            orders.Add(new WarehouseItem { ItemNumber = 417, Name = "Vika", Color = "Pearl", Size = 48, Price = 680, Quantity = 448 });
            
            dbModel = repository.MakeItemVM(repository.IEWarehouseItems);
            ordersModel = repository.MakeItemVM(orders, dbModel.itemNumbers, dbModel.itemSizes, dbModel.itemColors, btnOrdVMSes);
                                               
            if (btnOrdVMSes.SelNumbers.ContainsKey(itemNumber))
            {
                if (btnOrdVMSes.SelNumbers[itemNumber])
                {
                    btnOrdVMSes.SelNumbers[itemNumber] = false;
                }
                else
                {
                    btnOrdVMSes.SelNumbers[itemNumber] = true;
                }
            }
            else
            {
                btnOrdVMSes.SelNumbers.Add(itemNumber, true);
            }

            Session.Add("BtnOrderVMSession", btnOrdVMSes);
            
            return View("Index", ordersModel);
        }
    }
}