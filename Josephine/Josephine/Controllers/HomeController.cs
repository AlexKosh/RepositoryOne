using Josephine.Abstract;
using Josephine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            bool isDbEmpty = checkDbForEmpty();

            if (isDbEmpty)
            {
                return View("AskDemoData");
            }
            else
            {
                return View();
            }                       
        }

        // --------------------------------Demo part
        //these methods are intended for creating demo-information for this app
        public void pplPrices()
        {
            int[] modelNumbers = {423, 431, 435, 436, 437};
            int[] prices = {670, 730, 730, 980, 1200};

            repository.populatePrices(modelNumbers, prices);
        }
        public void delPrices()
        {
            repository.deletePrices();
        }

        public void pplWhAndSt()
        {
            repository.populateWhAndSt(431,
                    "Nadia",
                    new string[] { "Blue", "Dark Biege", "Pearl" },
                    new int[] { 44, 46, 48, 50, 52, 54, 56, 58 },
                    730);

                repository.populateWhAndSt(435,
                    "Vika",
                    new string[] { "Black", "Blue", "Dark Biege", "Pearl" },
                    new int[] { 44, 46, 48, 50, 52, 54, 56, 58, 60 },
                    730);

                repository.populateWhAndSt(
                        423,
                        "Vera",
                        new string[] { "Black", "Blue", "Dark Biege", "Pearl" },
                        new int[] { 50, 52, 54, 56, 58 },
                        670);

                repository.populateWhAndSt(
                        436,
                        "Veronika",
                        new string[] { "Blue", "Grey", "Cherry" },
                        new int[] { 44, 46, 48, 50, 52, 54, 56 },
                        980);

                repository.populateWhAndSt(
                        437,
                        "Snezana",
                        new string[] { "Blue", "Grey", "Cherry" },
                        new int[] { 44, 46, 48, 50, 52, 54, 56, 58 },
                        1200);        
        }
        public void delWhAndSt()
        {
            repository.deleteWhAndSt();
        }

        public void pplCust()
        {
            Customer c1 = new Customer();
            c1.Age = 0;
            c1.AlternatePhone = "063-777-55-21";
            c1.Balance = -150;
            c1.Birthday = new DateTime(1972, 10, 15);
            c1.CustomerId = 0;
            c1.Email = "Irina72@mail.ru";
            c1.FirstMet = new DateTime(2014, 04, 15);
            c1.Gender = false;
            c1.isInformed = true;
            c1.lastPurchase = new DateTime(2015, 9, 10);
            c1.Level = 5;
            c1.locationOfTrade = "Днепропетровск";
            c1.MoneySpent = 19000;
            c1.Name = "Ирина";
            c1.PhoneNumber = "099-543-22-19";
            c1.Speciality = "рынок";
            c1.Surname = "Иосифор";

            Customer c2 = new Customer();
            c2.Age = 0;
            c2.AlternatePhone = "050-55-225-71";
            c2.Balance = -50;
            c2.Birthday = new DateTime(1962, 05, 5);
            c2.CustomerId = 0;
            c2.Email = "Misha62@gmail.com";
            c2.FirstMet = new DateTime(2013, 09, 10);
            c2.Gender = true;
            c2.isInformed = true;
            c2.lastPurchase = new DateTime(2015, 8, 7);
            c2.Level = 7;
            c2.locationOfTrade = "Киев";
            c2.MoneySpent = 48000;
            c2.Name = "Михаил";
            c2.PhoneNumber = "073-999-22-87";
            c2.Speciality = "магазин";
            c2.Surname = "Карлсруа";

            Customer c3 = new Customer();
            c3.Age = 0;
            c3.AlternatePhone = "098-441-58-93";
            c3.Balance = 500;
            c3.Birthday = new DateTime(1984, 07, 12);
            c3.CustomerId = 0;
            c3.Email = "Katia84@gmail.com";
            c3.FirstMet = new DateTime(2015, 01, 10);
            c3.Gender = false;
            c3.isInformed = false;
            c3.lastPurchase = new DateTime(2015, 5, 7);
            c3.Level = 3;
            c3.locationOfTrade = "Львов";
            c3.MoneySpent = 12000;
            c3.Name = "Катерина";
            c3.PhoneNumber = "098-549-11-54";
            c3.Speciality = "магазин";
            c3.Surname = "Кентербери";

            Customer c4 = new Customer();
            c4.Age = 0;
            c4.AlternatePhone = "050-111-52-73";
            c4.Balance = 300;
            c4.Birthday = new DateTime(1980, 04, 9);
            c4.CustomerId = 0;
            c4.Email = "Vera80@gmail.com";
            c4.FirstMet = new DateTime(2013, 10, 15);
            c4.Gender = false;
            c4.isInformed = false;
            c4.lastPurchase = new DateTime(2014, 9, 12);
            c4.Level = 4;
            c4.locationOfTrade = "Одесса";
            c4.MoneySpent = 18000;
            c4.Name = "Катерина";
            c4.PhoneNumber = "099-777-21-44";
            c4.Speciality = "опт";
            c4.Surname = "Слежане";

            Customer c5 = new Customer();
            c5.Age = 0;
            c5.AlternatePhone = "050-888-44-22";
            c5.Balance = 150;
            c5.Birthday = new DateTime(1968, 04, 25);
            c5.CustomerId = 0;
            c5.Email = "Nadia68@gmail.com";
            c5.FirstMet = new DateTime(2012, 08, 16);
            c5.Gender = false;
            c5.isInformed = false;
            c5.lastPurchase = new DateTime(2015, 1, 21);
            c5.Level = 7;
            c5.locationOfTrade = "Донецк";
            c5.MoneySpent = 62000;
            c5.Name = "Надежда";
            c5.PhoneNumber = "093-222-11-54";
            c5.Speciality = "рынок";
            c5.Surname = "фон Бок";

            Customer c6 = new Customer();
            c6.Age = 0;
            c6.AlternatePhone = "063-889-11-43";
            c6.Balance = 0;
            c6.Birthday = new DateTime(1967, 08, 22);
            c6.CustomerId = 0;
            c6.Email = "Zoran67@gmail.com";
            c6.FirstMet = new DateTime(2014, 08, 14);
            c6.Gender = true;
            c6.isInformed = true;
            c6.lastPurchase = new DateTime(2015, 9, 20);
            c6.Level = 5;
            c6.locationOfTrade = "Луцк";
            c6.MoneySpent = 36500;
            c6.Name = "Зорян";
            c6.PhoneNumber = "099-119-71-65";
            c6.Speciality = "интернет";
            c6.Surname = "Монтгомери";

            Customer c7 = new Customer();
            c7.Age = 0;
            c7.AlternatePhone = "063-451-87-91";
            c7.Balance = 0;
            c7.Birthday = new DateTime(1988, 1, 29);
            c7.CustomerId = 0;
            c7.Email = "maria88@gmail.com";
            c7.FirstMet = new DateTime(2015, 02, 21);
            c7.Gender = false;
            c7.isInformed = false;
            c7.lastPurchase = new DateTime(2015, 8, 27);
            c7.Level = 1;
            c7.locationOfTrade = "Винница";
            c7.MoneySpent = 8000;
            c7.Name = "Мария";
            c7.PhoneNumber = "093-554-17-99";
            c7.Speciality = "сеть-магазин";
            c7.Surname = "Орхам";

            Customer[] customers = {c1, c2, c3, c4, c5, c6, c7};
            repository.populateCust(customers);
        }
        public void delCust()
        {
            repository.deleteCust();
        }

        public void pplEmp()
        {
            Employee e1 = new Employee();
            e1.Address = "Анкиано, близ Винчи";
            e1.AlternatePhone = "099-999-99-99";
            e1.Birthday = new DateTime(1952, 04, 15);
            e1.Email = "leanard52@gmail.com";
            e1.EmployeeId = 0;
            e1.Gender = true;
            e1.HireDate = new DateTime(2012, 04, 21);
            e1.Name = "Леонардо";
            e1.PhoneNumber = "099-999-99-98";
            e1.Speciality = "Менеджер";
            e1.Surname = "да Винчи";

            Employee e2 = new Employee();
            e2.Address = "Капрезе-Микеланджело, близ Ареццо";
            e2.AlternatePhone = "077-777-77-77";
            e2.Birthday = new DateTime(1975, 02, 25);
            e2.Email = "michel75@gmail.com";
            e2.EmployeeId = 0;
            e2.Gender = true;
            e2.HireDate = new DateTime(2013, 09, 01);
            e2.Name = "Микеланджело";
            e2.PhoneNumber = "077-777-77-76";
            e2.Speciality = "Продавец";
            e2.Surname = "Буонарроти";

            Employee e3 = new Employee();
            e3.Address = "Флоренция";
            e3.AlternatePhone = "055-555-55-55";
            e3.Birthday = new DateTime(1986, 07, 1);
            e3.Email = "donato86@gmail.com";
            e3.EmployeeId = 0;
            e3.Gender = true;
            e3.HireDate = new DateTime(2014, 01, 8);
            e3.Name = "Донателло";
            e3.PhoneNumber = "055-555-55-54";
            e3.Speciality = "Доставка";
            e3.Surname = "ди Никколо ди Бетто Барди"; 

            Employee e4 = new Employee();
            e4.Address = "Урбино";
            e4.AlternatePhone = "011-111-11-11";
            e4.Birthday = new DateTime(1983, 03, 28);
            e4.Email = "raffaello83@gmail.com";
            e4.EmployeeId = 0;
            e4.Gender = true;
            e4.HireDate = new DateTime(2015, 01, 21);
            e4.Name = "Рафаэль";
            e4.PhoneNumber = "099-999-99-98";
            e4.Speciality = "Упаковщик";
            e4.Surname = "Санти";

            Employee[] employees = { e1, e2, e3, e4 };
            repository.populateEmployee(employees);
        }
        public void delEmp()
        {
            repository.deleteEmployee();
        }

        public void pplOrders()
        {
            OrderInfo[] oi = new OrderInfo[40];
            OrderProduct[] op = new OrderProduct[40];

            int[] customersId = repository.Customers.Select(x => x.CustomerId).ToArray();
            int[] employeesId = repository.Employees.Select(x => x.EmployeeId).ToArray();

            string[] adr = {"Киев, ул. Героев Днепра", "Львов, ул. Привокзальная", "Харьков, Центральный рынок", "Киев, Святошино", 
                           "Одесса, 7-ой км", "Винница", "Ужгород", "Луцк", "Черновцы", "Краматорск"};
            OrderInfo oi1 = new OrderInfo();
            Random r = new Random();
            Random days = new Random();
            int d = 0;

            for (int i = 0; i < 40; i++)
            {
                oi1 = new OrderInfo();                
                d = days.Next(0, 8);

                oi1.CustomerId = customersId[r.Next(0, customersId.Length)];
                oi1.EmployeeId = employeesId[r.Next(0, employeesId.Length)];
                if (i < 13)
                {
                    oi1.ShippingMethod = "1";
                    oi1.ShipFrom = null;
                    oi1.PaymentMethod = "Наличные";
                    oi1.isDelivered = "заберут";
                }
                if (i < 30 && i > 12)
                {
                    oi1.ShippingMethod = "2";
                    oi1.ShipFrom = null;                    
                    oi1.PaymentMethod = "Водитель";
                    oi1.isDelivered = "отнести";
                }
                if (i > 30)
                {
                    oi1.ShippingMethod = "3";
                    oi1.ShipFrom = null;
                    oi1.PaymentMethod = "Банк";
                    oi1.isDelivered = "отнести";
                }
                oi1.ShipAddress = adr[r.Next(0, adr.Length)];             
                oi1.OrderDate = DateTime.Now.AddDays(-2);
                oi1.ShipmentDateMin = DateTime.Now.AddDays(d).AddHours(r.Next(-10, 10));
                oi1.ShipmentDateMax = oi1.ShipmentDateMin.AddHours(2);

                if (i == 13 || i == 20)
                {
                    oi1.Priority = 1;
                }
                else
                {                    
                    oi1.Priority = 0;
                }
                if (i % 8 == 0)
                {
                    oi1.isPacked = "без упаковки";
                }
                else
                {
                    oi1.isPacked = "не упакован";
                }
                oi1.Paid = 0;
                oi1.OrderDiscount = 0;
                oi1.OrderCost = days.Next(1, 45) * 1050;
                oi1.isPaid = "не оплачен";                
                oi1.isResolved = false;

                oi[i] = oi1;
            }

            OrderProduct op1 = new OrderProduct();            
            for (int i = 0; i < 40; i++)
            {
                op1 = new OrderProduct();
                op1.Color = "Blue";
                op1.ModelNumber = 423;                
                op1.ProductPrice = 670;
                op1.Quantity = 1;
                op1.Size = 50;
                op1.ProductId = repository.Warehouse.Where(x => x.Size == op1.Size &&
                    x.Color == op1.Color &&
                    x.ModelNumber == op1.ModelNumber).Select(x => x.ProductId).First();
                op[i] = op1;
            }

            repository.populateOrders(oi, op);
        }
        public void delOrders()
        {
            repository.deleteOrders();
        }

        public void pplMainWh()
        {
            List<MainWarehouse> mwh = new List<MainWarehouse>();

            MainWarehouse mwhItem = new MainWarehouse(2, "Плащевка", "Синий", 500, "метр");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(2, "Плащевка", "Светлый беж", 700, "метр");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(2, "Плащевка", "Темный беж", 300, "метр");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(2, "Плащевка", "Черный", 200, "метр");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(3, "Артикул 575", "Черный", 200, "метр");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(3, "Артикул 675", "Синий", 400, "метр");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(7, "Змейка 10см", "Синий", 60, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(7, "Змейка 10см", "Светлый беж", 110, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(7, "Змейка 10см", "Темный беж", 40, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(7, "Змейка 10см", "Черный", 20, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(7, "Змейка 80см", "Синий", 30, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(7, "Змейка 80см", "Светлый беж", 60, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(7, "Змейка 80см", "Темный беж", 20, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(7, "Змейка 80см", "Черный", 20, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(7, "Шнурок для капюшона", "Черный", 20, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(7, "Шнурок для капюшона", "Синий", 30, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(7, "Шнурок для капюшона", "Светлый беж", 50, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(7, "Шнурок для капюшона", "Темный беж", 20, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(7, "Кнопка скрытая", "метал", 400, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(7, "Змейка 20см", "Синий", 30, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(7, "Змейка 20см", "Светлый беж", 55, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(7, "Змейка 20см", "Темный беж", 20, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(7, "Змейка 20см", "Черный", 10, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(6, "Нитка PolyArt", "Синий", 1000, "метр");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(6, "Нитка PolyArt", "Светлый беж", 1500, "метр");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(6, "Нитка PolyArt", "Темный беж", 700, "метр");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(6, "Нитка PolyArt", "Черный", 500, "метр");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(6, "Нитка-резинка 60", "Белый", 1000, "метр");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(6, "Бирка Prunel", "Черный", 1000, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(6, "Бирка Made in", "Черный", 1000, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(6, "Бирка № размер", "Белый", 1000, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(6, "Бирка № швеи", "Белый", 1000, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(6, "Бирка условия эксплуатации", "Белый", 1000, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(6, "Бирка условия эксплуатации", "Белый", 1000, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(8, "Чернобурка", "Чернобурка", 1000, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(1, "Вика", "Синий", 100, "метр");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(1, "Вика", "Светлый беж", 200, "метр");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(1, "Вика", "Темный беж", 50, "метр");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(1, "Вика", "Черный", 50, "метр");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(4, "Силикон 100", "б/ц", 3000, "метр");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(9, "Пакеты 120", "б/ц", 500, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(9, "Пакеты 100", "б/ц", 500, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(9, "Ярлык Prunel", "Черный", 2500, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(9, "Наклейка размер 44", "б/ц", 400, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(9, "Наклейка размер 46", "б/ц", 400, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(9, "Наклейка размер 48", "б/ц", 400, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(9, "Наклейка размер 50", "б/ц", 400, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(9, "Наклейка № модели 435", "б/ц", 2400, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(9, "Наклейка № модели 431", "б/ц", 400, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(9, "Пакеты 120", "б/ц", 500, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(9, "Тремпель", "Черный", 5000, "шт.");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(5, "Артикул 331", "б/ц", 200, "метр");
            mwh.Add(mwhItem);
            mwhItem = new MainWarehouse(5, "Артикул 241", "б/ц", 200, "метр");
            mwh.Add(mwhItem);

            repository.populateMainWh(mwh);
        }
        public void delMainWh()
        {
            repository.deleteMainWh();
        }

        public JsonResult allDemo()
        {
            pplWhAndSt();
            pplPrices();
            pplEmp();
            pplCust();
            pplOrders();
            pplMainWh();

            checkDbForEmpty();

            return Json(Session["isDbsEmpty"], JsonRequestBehavior.AllowGet);
        }
        public JsonResult someDemo()
        {
            if (repository.Warehouse.Count() == 0)
            {
                pplWhAndSt();
            }

            if (repository.Store.Count() == 0)
            {
                pplWhAndSt();
            }

            if (repository.Prices.Count() == 0)
            {
                pplPrices();
            }

            if (repository.Customers.Count() == 0)
            {
                pplCust();
            }

            if (repository.Employees.Count() == 0)
            {
                pplEmp();
            }

            if (repository.OrderInfo.Count() == 0)
            {
                pplOrders();
            }

            if (repository.MainWh.Count() == 0)
            {
                pplMainWh();
            }

            checkDbForEmpty();
            return Json(Session["isDbsEmpty"], JsonRequestBehavior.AllowGet);
        }

        private bool checkDbForEmpty()
        {
            //{ wh, st, prices, cust, empl, ordInf, mainWh }
            bool[] isDbEmpty = { false, false, false, false, false, false, false };
            
            if (repository.Warehouse.Count() == 0)
            {
                isDbEmpty[0] = true;
            }

            if (repository.Store.Count() == 0)
            {
                isDbEmpty[1] = true;
            }

            if (repository.Prices.Count() == 0)
            {
                isDbEmpty[2] = true;
            }

            if (repository.Customers.Count() == 0)
            {
                isDbEmpty[3] = true;
            }

            if (repository.Employees.Count() == 0)
            {
                isDbEmpty[4] = true;
            }

            if (repository.OrderInfo.Count() == 0)
            {
                isDbEmpty[5] = true;
            }

            if (repository.MainWh.Count() == 0)
            {
                isDbEmpty[6] = true;
            }

            Session["isDbsEmpty"] = isDbEmpty;

            if (isDbEmpty[0] == true || isDbEmpty.Distinct().Count() == 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public JsonResult isDbsEmpty()
        {
            checkDbForEmpty();
            return Json(Session["isDbsEmpty"], JsonRequestBehavior.AllowGet);
        }
        public ActionResult askDemoData()
        {
            return View("AskDemoData");
        }
        //--------------------------------End of demo part

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
                notation.Sizes = tempModel.Select(x => x.Size).Distinct().OrderBy(x => x).ToList();
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
            OrderData data = new OrderData();
            
            DateTime today = DateTime.Now;            
            today = new DateTime(today.Year, today.Month, today.Day);
            data.OrderInfo = repository.OrderInfo.Where(x => x.ShipmentDateMax > today).Select(x => x);
                        
            List<OrderProduct> result = new List<OrderProduct>();
            var orders = data.OrderInfo.ToList();
            for (int i = 0; i < orders.Count; i++)
            {
                var temp = repository.OrderProduct.Where(x => x.OrderId == orders[i].OrderId).Select(x => x);
                result.AddRange(temp);
            }
            
            data.OrderProduct = result;
            

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
        public JsonResult Sales()
        {
            return Json(repository.Sale, JsonRequestBehavior.AllowGet);
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
        public PartialViewResult navButtons()
        {
            return PartialView();
        }

        public JsonResult getModelNames()
        {
            //int[] modelNumbs = repository.Warehouse.Select(x => x.ModelNumber).Distinct().ToArray();
            //string[] modelNames = new string[modelNumbs.Length];
            //for (int i = 0; i < modelNames.Length; i++)
            //{
            //    modelNames[i] = repository.Warehouse.Where(x => x.ModelNumber == modelNumbs[i]).Select(x => x.Name).First();
            //}
            //Dictionary<int, string> result = new Dictionary<int, string>();
            //for (int i = 0; i < modelNames.Length; i++)
            //{
            //    result.Add(modelNumbs[i], modelNames[i]);
            //}

            var res = repository.Warehouse.Select(x => new { x.ModelNumber, x.Name}).Distinct();

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult processOrder(OrderData d)
        {
            int ordId = repository.AddOrderInfoToDb(d.OrderInfo.First());

            repository.AddOrderProductsToDb(d.OrderProduct, ordId);
            
            return Json(d, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult newEmployee(Employee d)
        {
            repository.AddDataToDb<Employee>(d);
            Employee data = repository.Employees.First(x => x.HireDate == d.HireDate);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult newCustomer(Customer d)
        {
            repository.AddDataToDb<Customer>(d);
            Customer data = repository.Customers.First(x => x.FirstMet == d.FirstMet);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public void newProduct(Warehouse d)
        {
            repository.AddDataToDb<Warehouse>(d);
        }
        [HttpPost]
        public JsonResult sale(OrderData d)
        {
            OrderInfo orderInfo = d.OrderInfo.First();
            int custId = orderInfo.CustomerId;
            int empId = orderInfo.EmployeeId;
            int ordId = repository.AddOrderInfoToDb(orderInfo);            

            repository.AddOrderProductsToDb(d.OrderProduct, ordId);

            repository.AddSoldProductsToDb(d.OrderProduct, custId, empId, ordId);

            return Json(repository.Sale, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult getSalesData(DateForSalesData d) {
            d.minDate = new DateTime(d.minDate.Year, d.minDate.Month, d.minDate.Day);
            d.maxDate = new DateTime(d.maxDate.Year, d.maxDate.Month, d.maxDate.Day + 1);

            var salesData = repository.Sale.Where(x => x.SaleDate >= d.minDate && x.SaleDate <= d.maxDate);

            if (salesData.Count() == 0)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

            return Json(salesData, JsonRequestBehavior.AllowGet);
        }

        public class DateForSalesData
        {
            public DateTime minDate { get; set; }
            public DateTime maxDate { get; set; }
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