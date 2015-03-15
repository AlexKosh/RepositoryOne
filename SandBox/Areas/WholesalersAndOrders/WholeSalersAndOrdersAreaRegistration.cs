using System.Web.Mvc;

namespace SandBox.Areas.WholesalersAndOrders
{
    public class WholesalersAndOrdersAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "WholesalersAndOrders";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "WholesalersAndOrders_default",
                "WholesalersAndOrders/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}