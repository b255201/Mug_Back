using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mug.Service;
using Mug.Service.Interface;
using Mug.Dao;
using Mug.Service.UI;

namespace Mug.Controllers
{
    public class AboutController : Controller
    {
        private IAboutPageService AboutPageService = new AboutPageService();
        // GET: About
        public ActionResult Index()
        {
            return View();
        }

        #region Search 關於查詢
        [HttpPost]
        public ActionResult Search()
        {
            var q = AboutPageService.GetAll().ToList();
            int totalLen = Convert.ToInt16(q.Count());
            var result = from Row in q
                         select new AboutPage
                         {
                             Id = Row.Id,
                             Title = Row.Title,
                             Description = Row.Description,
                             Image = Row.Image,
                             Enable = Row.Enable,
                             //Time = DBNull.Value.Equals(Row.CreateTime) ? "" : DateTimeExtension.ToYMD(Row.CreateTime)
                         };
            JQueryDataTableResponse<AboutPage> jqDataTableRs = JQueryDataTableHelper<AboutPage>.GetResponse(1, totalLen, totalLen, result.ToList());
            return Json(jqDataTableRs);
        }
        #endregion

        #region Insert
        [HttpGet]
        public ActionResult Insert()
        {
            return View();
        }
        #endregion

        #region Insert 大圖新增
        [HttpPost]
        public ActionResult Insert(FormCollection form, HttpPostedFileBase Image)
        {
            bool Enable = true;
            if (form["Enable"] != "on")
            {
                Enable = false;
            }
            var q = AboutPageService.GetAll().ToList();
            var result = (from Row in q
                          select new AboutPage
                          {
                              Id = Row.Id,
                              Title = Row.Title,
                              Description = Row.Description,
                              Image = Row.Image,
                              Enable = Row.Enable,
                              CreateTime = Row.CreateTime
                          }).OrderByDescending(x => x.Id);
            int i = 1;
            if(q.Count() !=0)
            {
                if (!String.IsNullOrEmpty(result.First().Id.ToString()))
                {
                    var MaxId = int.Parse(result.First().Id.ToString());
                    i = MaxId + 1;
                }
            }
          
            AboutPage _AboutPage = new AboutPage();
            _AboutPage.Id = i;
            _AboutPage.Title = form["Title"];
            _AboutPage.Description = form["Description"];
            _AboutPage.Enable = Enable;
            _AboutPage.CreateTime = DateTime.Now;
            int checkEnable = result.Where(x => x.Enable == true).Count();
            if (checkEnable > 0 && Enable == true)
            {
                var key = result.Where(x => x.Enable == true).First().Id;
                ViewBag.ErrMessage = "目前編號"+ key + "已是啟用狀態";
                return View(_AboutPage);
            }

            if (Image != null)
            {
                //判斷圖片名稱是否重複
                var rptimg = (from Row in q
                              select new AboutPage
                              {
                                  Image = Row.Image
                              }).Where(x => x.Image == Image.FileName);
                if (rptimg != null)
                {
                    if (rptimg.Count() != 0)
                    {
                        ViewBag.ErrMessage = "已有圖片名稱：" + Image.FileName + "，請檢查";
                        return View(_AboutPage);
                    }
                }
                string strPath = Request.PhysicalApplicationPath + @"Image\About\" + Image.FileName;
                Image.SaveAs(strPath);
                _AboutPage.Image = Image.FileName;
            }
            var Message= AboutPageService.Create(_AboutPage);
            if (Message.Success == true)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ErrMessage = "請洽程式管理人員" + Message.Exception.ToString() ;
                return View(_AboutPage);
            }
        }
        #endregion

        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            var _About = AboutPageService.GetByID(id);
            ViewBag.Enable = _About.Enable.ToString();
            ViewBag.Image = _About.Image.ToString();
            return View(_About);
        }
        #region 大圖修改
        [HttpPost]
        public ActionResult Edit(FormCollection form, HttpPostedFileBase Image)
        {
            bool Enable = true;
            if (form["Enable"] != "on")
            {
                Enable = false;
            }
            AboutPage _About = AboutPageService.GetByID(int.Parse(form["Id"]));
            _About.Title = form["Title"];
            _About.Description = form["Description"];
            _About.Enable = Enable;
            _About.ModifyTime = DateTime.Now;

            var checkEnable = _About.Enable;
            if (checkEnable == true)
            {
                var q = AboutPageService.GetAll().Where(x => x.Enable == true);
                var key = q.First().Id;
                ViewBag.Image = _About.Image.ToString();
               ViewBag.ErrMessage = "目前編號" + key + "已是啟用狀態";
                return View(_About);
            }

            if (Image != null)
            {
                var q = AboutPageService.GetAll().ToList();
                //判斷圖片名稱是否重複
                var rptimg = (from Row in q
                              select new AboutPage
                              {
                                  Image = Row.Image
                              }).Where(x => x.Image == Image.FileName);
                if (rptimg != null)
                {
                    if (rptimg.Count() != 0)
                    {
                        ViewBag.Image = _About.Image.ToString();
                        ViewBag.ErrMessage = "已有圖片名稱：" + Image.FileName + "，請檢查";
                        return View(_About);
                    }
                    else
                    {
                        string OrgImgName = form["OrgImgName"].ToString();
                        if (OrgImgName != "")
                        {
                            string strPath1 = string.Format("~/Image/About/{0}", OrgImgName);
                            var fullPath = Request.MapPath(strPath1);
                            System.IO.File.Delete(fullPath);
                        }
                    }
                }
                string strPath = Request.PhysicalApplicationPath + @"Image\About\" + Image.FileName;
                Image.SaveAs(strPath);
                _About.Image = Image.FileName;
            }
            var Message = AboutPageService.Update(_About);
            if (Message.Success == true)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ErrMessage = "請洽程式管理人員" + Message.Exception.ToString();
                return View(_About);
            }
        }
        #endregion
        public ActionResult GetImageFile(string fileName)
        {
            return File("~/Image/About/" + fileName, "image/jpg");
        }
    }
}