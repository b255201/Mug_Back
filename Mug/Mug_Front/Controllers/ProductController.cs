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
    public class ProductController : Controller
    {
        private IBloggerService BloggerService = new BloggerService();
        private ILanguageService LanguageService = new LanguageService();
        private IArticleService ArticleService = new ArticleService();
        // GET: Product
        public ActionResult Index(string ProId)
        {
            string ProName = ProductId(ProId);

            DaoHelper helper = new DaoHelper();
            //首頁大圖
            var result = helper.GetProduct();

            result = result.Where(x => x.Categore == "產品頁" &&x.Category_Opt== ProName).ToList();

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

            return View();
        }
        public static string ProductId(string ProId)
        {
            string proNmae = "";
            switch (ProId)
            {
                case "1":
                    proNmae = "陶瓷";
                    break;
                case "2":
                    proNmae = "玻璃";
                    break;
                case "3":
                    proNmae = "馬克杯";
                    break;
                case "4":
                    proNmae = "裝飾品";
                    break;
                default:
                    proNmae = "馬克杯";
                    break;
            }
            return proNmae;
        }
    }
  
}