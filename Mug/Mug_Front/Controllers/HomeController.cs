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
    public class HomeController : Controller
    {
        private IBloggerService BloggerService = new BloggerService();
        private ILanguageService LanguageService = new LanguageService();
        private IArticleService ArticleService = new ArticleService();

        public ActionResult Index()
        {

            //首頁大圖
            DaoHelper helper = new DaoHelper();
            var result = helper.GetHome();
            result= result.Where(x => x.Categore == "首頁大圖" || x.Categore == "成品展覽").ToList();


            //result = result.Where(x => x.Enable == true);
            //HttpCookie cookie = Request.Cookies["_culture"];
            //if (cookie != null)
            //{
            //    LanguageCommon LangUser = new LanguageCommon();
            //    string LangKey=LangUser.GetUserLang(cookie.Value.ToString());
            //    result = result.Where(x => x.Id == int.Parse(LangKey)).ToList();
            //}
            //else
            //{
            //    result = result.Where(x => x.Id == 1).ToList();
            //}
            return View(result.ToList());
        }

        public ActionResult GetImageFile(string fileName)
        {
           // return File("C:/Users/Administrator/Desktop/MUG_Back/Mug/Mug/Image/Home/" + fileName, "image/png");
            return File("C:/Users/Back/Image/Home/" + fileName, "image/png");
        }

    }
}