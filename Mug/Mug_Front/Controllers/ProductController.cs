using Mug.Service;
using Mug.Service.Interface;
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
            var blog = BloggerService.GetAll();
            var articles = ArticleService.GetAll();
            //首頁大圖
            var result = from b in blog
                         join a in articles
                          on b.Blog_id equals a.Post_Id
                         where b.Categore == "產品頁" && b.Category_Opt== ProName && b.Enable == true
                         orderby b.Blog_id
                         select new ArticleViewModel
                         {
                             Blog_id = b.Blog_id,
                             Title = a.Title,
                             Image = b.Image,
                             Enable = b.Enable,
                             Categore = b.Categore,
                             Category_Opt = b.Category_Opt,
                             Sub_Title = a.Sub_Title,
                             Contents = a.Contents,
                             Id = a.Id
                         };

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
                    proNmae = "其他";
                    break;
                default:
                    proNmae = "馬克杯";
                    break;
            }
            return proNmae;
        }
    }
  
}