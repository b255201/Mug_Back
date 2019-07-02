using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mug_Front.Controllers
{
    public class LanguageController : Controller
    {
        // GET: Language
        public ActionResult SetCulture(string culture)
        {
  
            // Validate input 
            //  culture = CultureHelper.GetImplementedCulture(culture);

            // Save culture in a cookie 
            HttpCookie cookie = Request.Cookies["_culture"];

            if (cookie != null)
            {
                // update cookie value 
                if (culture == null)
                {
                    cookie.Value = "";
                }
                else
                {
                    cookie.Value = culture;
                }
            }
            else
            {
                cookie = new HttpCookie("_culture");
                if (culture == null)
                {
                    cookie.Value = "";
                }
                else
                {
                    // create cookie value            
                    cookie.Value = culture;
                }
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);

            return RedirectToAction("Index", "Home");
        }
    }
}