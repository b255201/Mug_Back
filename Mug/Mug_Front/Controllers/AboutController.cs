using Mug.Service;
using Mug.Service.Interface;
using Mug_Front.Helper;
using Mug_Front.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mug_Front.Controllers
{
    public class AboutController : Controller
    {
        private IBloggerService BloggerService = new BloggerService();
        private ILanguageService LanguageService = new LanguageService();
        private IArticleService ArticleService = new ArticleService();
        // GET: About
        public ActionResult Index()
        {
            DaoHelper helper = new DaoHelper();
            var result = helper.GetAbout();

            result = result.Where(x => x.Categore == "關於頁").ToList();

            HttpCookie cookie = Request.Cookies["_culture"];
            if (cookie != null)
            {
                LanguageCommon LangUser = new LanguageCommon();
                string LangKey = LangUser.GetUserLang(cookie.Value.ToString());
                result = result.Where(x => x.Id == int.Parse(LangKey)).ToList();
            }
            else
            {
                result = result.Where(x => x.Id == 1).ToList();
            }
            return View(result);

        }
    }

  
}