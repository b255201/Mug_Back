using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Mug.Attribute
{
    public class AuthenticateUserAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (filterContext.HttpContext.Session["EmpId"] == null)
            {
                // filterContext.Result = new RedirectToRouteResult("/Login/Index", null);
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    {"controller","Admin"},
                    {"action","Login"},
                    { "id", UrlParameter.Optional }
                });
            }
        }
    }
}