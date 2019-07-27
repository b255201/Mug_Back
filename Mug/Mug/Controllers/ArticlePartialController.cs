using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mug.Service;
using Mug.Service.Interface;
using Mug.Dao;
using Mug.Service.UI;
using Mug.Attribute;

namespace Mug.Controllers
{
    [AuthenticateUser]
    public class ArticlePartialController : Controller
    {
        private IArticleService ArticleService = new ArticleService();
        private ILanguageService LanguageService = new LanguageService();

        // GET: ArticlePartial
        public ActionResult Index()
        {
            return View();
        }

        #region Create, Article
        /// <summary>
        /// 取得新增的部份檢視
        /// </summary>
        /// <returns>部份檢視</returns>
        [HttpGet]
        public ActionResult ArticleCreate()
        {
            var q = LanguageService.GetAll();
            var result = (from Row in q
                          select new Language
                          {
                              Id = Row.Id,
                              Lang_val = Row.Lang_val,
                              Lang_text = Row.Lang_text,
                          }).OrderBy(x => x.Id).ToList();
            return PartialView("ArticleCreate",result);
        }
        #endregion



        #region  文章 新增
        [HttpPost]
        public ActionResult Insert(FormCollection form)
        {
            try
            {
                int Post_Id= int.Parse(form["Blog_id"]);
                int lang_Id = int.Parse(form["lang_Id"]);
                var q = ArticleService.GetAll().ToList();
                var result =  (from Row in q
                              select new Article
                              {
                                  Id= Row.Id,
                                  Post_Id = Row.Post_Id,
                                  Article_ID = Row.Article_ID,                              
                              }).Where(x=>x.Post_Id== Post_Id && x.Id== lang_Id);
            
                Article _Article = new Article();
                _Article.Article_ID= int.Parse(result.First().Article_ID.ToString());
                _Article.Post_Id = int.Parse(result.First().Post_Id.ToString());
                _Article.Title = form["Title"];
               // _Article.Sub_Title = form["SubTitle"];
                _Article.Contents = form["Contents"];
                _Article.CreateTime = DateTime.Now;
                _Article.Id = int.Parse(form["lang_Id"]);
                var Message = ArticleService.Update(_Article);
                if (Message.Success == true)
                {
                    return Json(new { Status = "0", Message = "新增成功", LangKey = lang_Id });
                }
                else
                {
                    return Json(new { Status = "1", Message = "請洽程式管理人員" + Message.Exception.ToString() });
                }
            }
            catch (Exception err)
            {
                return Json(new { Status = "1", Message = "新增失敗" + err.Message });
            }

        }
        #endregion

        #region  文章 編輯
        [HttpPost]
        public ActionResult Edit(FormCollection form)
        {
            try
            {
                int Article_ID = int.Parse(form["Article_ID"]);
                Article _Article = ArticleService.GetByArt(Article_ID);
                _Article.Title = form["Title"];
                _Article.Sub_Title = form["SubTitle"];
                _Article.Contents = form["Contents"];
                _Article.Article_ID= Article_ID;
                var Message = ArticleService.Update(_Article);
                if (Message.Success == true)
                {
                    return Json(new { Status = "0", Message = "更新成功" });
                }
                else
                {
                    return Json(new { Status = "1", Message = "請洽程式管理人員" + Message.Exception.ToString() });
                }
            }
            catch (Exception err)
            {
                return Json(new { Status = "1", Message = "更新失敗" + err.Message });
            }

        }
        #endregion



        #region Create, Article Ckeditor
        /// <summary>
        /// 取得新增的部份檢視
        /// </summary>
        /// <returns>部份檢視</returns>
        [HttpGet]
        public ActionResult ArticleCreateCkeditor()
        {
            var q = LanguageService.GetAll();
            var result = (from Row in q
                          select new Language
                          {
                              Id = Row.Id,
                              Lang_val = Row.Lang_val,
                              Lang_text = Row.Lang_text,
                          }).OrderBy(x => x.Id).ToList();
            return PartialView("ArticleCreateCkeditor", result);
        }
        #endregion



        #region  文章 新增
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult InsertCkeditor(FormCollection form)
        {
            try
            {
                var Contents = form["Contents"];

                int Post_Id = int.Parse(form["Blog_id"]);
                int lang_Id = int.Parse(form["lang_Id"]);
                var q = ArticleService.GetAll().ToList();
                var result = (from Row in q
                              select new Article
                              {
                                  Id = Row.Id,
                                  Post_Id = Row.Post_Id,
                                  Article_ID = Row.Article_ID,
                              }).Where(x => x.Post_Id == Post_Id && x.Id == lang_Id);

                Article _Article = new Article();
                _Article.Article_ID = int.Parse(result.First().Article_ID.ToString());
                _Article.Post_Id = int.Parse(result.First().Post_Id.ToString());
                _Article.Title = form["Title"];
                _Article.Contents = form["Contents"];
                _Article.CreateTime = DateTime.Now;
                _Article.Id = int.Parse(form["lang_Id"]);
                var Message = ArticleService.Update(_Article);
                if (Message.Success == true)
                {
                    return Json(new { Status = "0", Message = "新增成功",LangKey= lang_Id });
                  
                }
                else
                {
                    return Json(new { Status = "1", Message = "請洽程式管理人員" + Message.Exception.ToString() });
                }
            }
            catch (Exception err)
            {
                return Json(new { Status = "1", Message = "新增失敗" + err.Message });
            }
        }
        #endregion

        #region  文章 編輯
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditCkeditor(FormCollection form)
        {
            try
            {
                int Article_ID = int.Parse(form["Article_ID"]);
                Article _Article = ArticleService.GetByArt(Article_ID);
                _Article.Title = form["Title"];
                _Article.Sub_Title = form["SubTitle"];
                _Article.Contents = form["Contents"];
                _Article.Article_ID = Article_ID;
                var Message = ArticleService.Update(_Article);
                if (Message.Success == true)
                {
                    return Json(new { Status = "0", Message = "更新成功" });
                }
                else
                {
                    return Json(new { Status = "1", Message = "請洽程式管理人員" + Message.Exception.ToString() });
                }
            }
            catch (Exception err)
            {
                return Json(new { Status = "1", Message = "更新失敗" + err.Message });
            }

        }
        #endregion

        public new HttpContextBase httpContext
        {
            get
            {
                HttpContextWrapper context =
                    new HttpContextWrapper(System.Web.HttpContext.Current);
                return (HttpContextBase)context;
            }
        }

    }
}