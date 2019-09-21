using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mug.Service;
using Mug.Service.Interface;
using Mug.Dao;
using Mug.Service.UI;
using Mug.Models;
using Mug.Attribute;
using Mug.HtmlHelper;
using System.IO;
using System.Data.Entity;

namespace Mug.Controllers
{
    [AuthenticateUser]
    public class ExhabitionController : Controller
    {

        private IBloggerService BloggerService = new BloggerService();
        private ILanguageService LanguageService = new LanguageService();
        private IArticleService ArticleService = new ArticleService();
        DbContext db = new DbContext("MugFactoryEntities");

        // GET: HomeTest
        public ActionResult Index()
        {
            LinqTable optHelper = new LinqTable();
            ViewBag.SelectList = optHelper.option();
            return View();
        }

        #region Search 大圖查詢
        [HttpPost]
        public ActionResult Search()
        {

            LinqTable optHelper = new LinqTable();
            var result = optHelper.BlogHelper();
            result = result.Where(x => x.Categore == "成品展覽");

            int totalLen = Convert.ToInt16(result.Count());
            JQueryDataTableResponse<ArticleViewModel> jqDataTableRs = JQueryDataTableHelper<ArticleViewModel>.GetResponse(1, totalLen, totalLen, result.ToList());
            return Json(jqDataTableRs);
        }
        #endregion


        [HttpGet]
        public ActionResult Insert()
        {
            return View();
        }

        #region blogger 新增
        [HttpPost]
        public ActionResult Insert(FormCollection form)
        {
            try
            {
                bool Enable = true;
                if (form["Enable"] != "true")
                {
                    Enable = false;
                }
                var q = BloggerService.GetAll().ToList();
                Blogger _Blogger = new Blogger();
                //get image file
                HttpPostedFileBase Image = Request.Files[0];
                ImageHelper imgHelper = new ImageHelper();

                string Guid = imgHelper.GetGuid();
                if (Image != null)
                {
                    //判斷圖片名稱是否重複            
                    string strPath = Request.PhysicalApplicationPath + @"Image\Home\" + Guid;
                    Image.SaveAs(strPath);
                    _Blogger.Image = Guid;
                }
                //取號
                SqlCommand _GetSerialNumber = new SqlCommand();
                string Blog_id = _GetSerialNumber.GetBySeq();
                _Blogger.Blog_id = int.Parse(Blog_id);
                _Blogger.Enable = Enable;
                _Blogger.CreateTime = DateTime.Now;
                _Blogger.Categore = "成品展覽";


                //新增到temp
                db.Set<Blogger>().Add(_Blogger);

                //順便把articel 也新增
                var art = ArticleService.GetAll().ToList();
                var result = (from r in art
                              select new Article
                              {
                                  Article_ID = r.Article_ID,
                              }).OrderByDescending(x => x.Article_ID);
                int MaxId = 1;
                if (result.Count() != 0)
                {
                    MaxId = int.Parse(result.First().Article_ID.ToString());
                    MaxId = MaxId + 1;
                }
                // LanguageService
                var lang = LanguageService.GetAll().ToList();
                foreach (var i in lang)
                {
                    Article _Article = new Article();
                    _Article.Post_Id = _Blogger.Blog_id;
                    _Article.Title = "";
                    _Article.Sub_Title = "";
                    _Article.Contents = "";
                    _Article.Article_ID = MaxId;
                    _Article.Id = int.Parse(i.Id.ToString());
                    //新增到temp
                    db.Set<Article>().Add(_Article);
                    //序號加一
                    MaxId = MaxId + 1;
                }

                //做交易
                db.SaveChanges();
                return Json(new { Status = "0", Message = "新增成功，編號是：" + Blog_id, SeqID = Blog_id });

            }
            catch (Exception err)
            {
                return Json(new { Status = "1", Message = "新增失敗" + err.Message });
            }

        }
        #endregion

        #region Delete
        [HttpPost]
        public ActionResult Delete(string Id)
        {
            try
            {
                SqlCommand _GetSerialNumber = new SqlCommand();
                Blogger _Blogger = BloggerService.GetByID(int.Parse(Id));
                //取得文章跟blog 資料
                string message = _GetSerialNumber.TranGetArticleBlog(Id);
                if (!String.IsNullOrEmpty(_Blogger.Image))
                {
                    string strPath = string.Format("~/Image/Home/{0}", _Blogger.Image);
                    var fullPath = Request.MapPath(strPath);
                    System.IO.File.Delete(fullPath);
                }
                //刪除到temp
                if (message == "0")
                {
                    return Json(new { Status = "0", StatusDesc = "刪除成功" });
                }
                else
                {
                    return Json(new { Status = "1", StatusDesc = "刪除失敗,請洽管理人員" + message });
                }
            }
            catch (Exception err)
            {

                return Json(new { Status = "2", StatusDesc = "刪除失敗,請洽管理人員" + err.Message });
            }

        }
        #endregion
        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            var _Blogger = BloggerService.GetByID(id);
            ViewBag.Enable = _Blogger.Enable.ToString();
            ViewBag.Image = _Blogger.Image.ToString();
            //取所有文章
            ViewBag.Article = ArticleService.GetAll().Where(x => x.Post_Id == id).ToList();
            return View(_Blogger);
        }

        #region 大圖修改
        [HttpPost]
        public ActionResult Edit(FormCollection form)
        {
            bool Enable = true;
            if (form["Enable"] != "true")
            {
                Enable = false;
            }
            HttpPostedFileBase Image = null;
            if (Request.Files.Count != 0)
            {
                Image = Request.Files[0];
            }

            Blogger _Blogger = BloggerService.GetByID(int.Parse(form["Blog_id"]));
            _Blogger.Enable = Enable;
            ImageHelper imgHelper = new ImageHelper();
            if (Image != null)
            {
                //先刪除原本的
                string DelPath = string.Format("~/Image/Home/{0}", _Blogger.Image);
                var fullPath = Request.MapPath(DelPath);
                System.IO.File.Delete(fullPath);

                //再新增
                string Guid = imgHelper.GetGuid();
                string strPath = Request.PhysicalApplicationPath + @"Image\Home\" + Guid;
                Image.SaveAs(strPath);
                _Blogger.Image = Guid;
            }
            var Message = BloggerService.Update(_Blogger);
            if (Message.Success == true)
            {
                return Json(new { Status = "0", Message = "更新成功" });
            }
            else
            {
                return Json(new { Status = "1", Message = "請洽程式管理人員" + Message.Exception.ToString() });
            }
        }
        #endregion
    }
}