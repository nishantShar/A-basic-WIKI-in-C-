using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication11.Filters
{
    public class AuthorizeUser:ActionFilterAttribute ,IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["isLoggedIn"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary {
                    { "Controller","Home"},
                      { "Action","LoginView"}
                });
            }
            base.OnActionExecuting(filterContext);
        }
    }
}