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
    public class HomeController : Controller
    {
        private IHomePageService HomePageService = new HomePageService();
        public ActionResult Index()
        {
            return View();
        }
        #region Search 大圖查詢
        [HttpPost]
        public ActionResult Search()
        {
 
            //ID >1 首頁大圖
            var q = HomePageService.GetAll().Where(i=>i.Opt== "首頁大圖").ToList();
            int totalLen = Convert.ToInt16(q.Count());
            var result = from Row in q
                         select new HomePage
                         {
                             Id = Row.Id,
                             Title = Row.Title,
                             Description = Row.Description,
                             Image = Row.Image,
                             Enable = Row.Enable,
                           //  Time = DBNull.Value.Equals(Row.CreateTime) ?"":DateTimeExtension.ToYMD(Row.CreateTime)
                           
                         };
            JQueryDataTableResponse<HomePage> jqDataTableRs = JQueryDataTableHelper<HomePage>.GetResponse(1, totalLen, totalLen, result.ToList());
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
            var q = HomePageService.GetAll().ToList();
            var result = (from Row in q
                          select new HomePage
                          {
                              Id = Row.Id,
                              Title = Row.Title,
                              Description = Row.Description,
                              Image = Row.Image,
                              Enable = Row.Enable,
                              CreateTime = Row.CreateTime
                          }).OrderByDescending(x => x.Id);
            var MaxId = int.Parse(result.First().Id.ToString());
            int i = MaxId + 1;
            HomePage _HomePage = new HomePage();
            _HomePage.Id = i;
            _HomePage.Title = form["Title"];
            _HomePage.Description = form["Description"];
            _HomePage.Enable = Enable;
            _HomePage.CreateTime = DateTime.Now;
            _HomePage.Opt = "首頁大圖";
            if (Image != null)
            {
                //判斷圖片名稱是否重複
                var rptimg = (from Row in q
                              select new HomePage
                              {
                                  Image = Row.Image
                              }).Where(x => x.Image == Image.FileName);
                if (rptimg != null)
                {
                    if (rptimg.Count() != 0)
                    {
                        ViewBag.ErrMessage = "已有圖片名稱：" + Image.FileName + "，請檢查";
                        return View(_HomePage);
                    }
                }
                string strPath = Request.PhysicalApplicationPath + @"Image\Home\" + Image.FileName;
                Image.SaveAs(strPath);
                _HomePage.Image = Image.FileName;
            }      
             var Message=  HomePageService.Create(_HomePage);  
            if (Message.Success == true)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ErrMessage = "請洽程式管理人員" + Message.Exception.ToString();
                return View(_HomePage);
            }
        }
        #endregion


        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            var _HomePage = HomePageService.GetByID(id);
            ViewBag.Enable = _HomePage.Enable.ToString();
            ViewBag.Image = _HomePage.Image.ToString();
            return View(_HomePage);
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
            HomePage _HomePage = HomePageService.GetByID(int.Parse(form["Id"]));
            _HomePage.Title = form["Title"];
            _HomePage.Description = form["Description"];
            _HomePage.Enable = Enable;
            _HomePage.ModifyTime = DateTime.Now;
            _HomePage.Opt = "首頁大圖";       
            if (Image != null)
            {
                var q = HomePageService.GetAll().ToList();
                //判斷圖片名稱是否重複
                var rptimg = (from Row in q
                                select new HomePage
                                {
                                    Image = Row.Image
                                }).Where(x => x.Image == Image.FileName);
                if (rptimg != null)
                {
                    if (rptimg.Count() != 0)
                    {
                        ViewBag.Image = _HomePage.Image.ToString();
                        ViewBag.ErrMessage = "已有圖片名稱：" + Image.FileName + "，請檢查";
                        return View(_HomePage);
                    }
                    else
                    {
                        string OrgImgName = form["OrgImgName"].ToString();
                        if (OrgImgName != "")
                        {
                            string strPath1 = string.Format("~/Image/Home/{0}", OrgImgName);
                            var fullPath = Request.MapPath(strPath1);
                            System.IO.File.Delete(fullPath);
                        }
                    }
                }          
                string strPath = Request.PhysicalApplicationPath + @"Image\Home\" + Image.FileName;
                Image.SaveAs(strPath);
                _HomePage.Image = Image.FileName;
            }            
           var Message= HomePageService.Update(_HomePage);
            if (Message.Success == true)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ErrMessage = "請洽程式管理人員" + Message.Exception.ToString();
                return View(_HomePage);
            }
        }
        #endregion

        #region Delete
        [HttpPost]
        public ActionResult Delete(string Id)
        {
            try
            {
                HomePage _HomePage = HomePageService.GetByID(int.Parse(Id));
                if (!String.IsNullOrEmpty(_HomePage.Image))
                {
                    string strPath = string.Format("~/Image/Home/{0}", _HomePage.Image);
                    var fullPath = Request.MapPath(strPath);
                    System.IO.File.Delete(fullPath);
                }             
                HomePageService.Delete(int.Parse(Id));
                return Json(new { Status = "0", StatusDesc = "刪除成功" });
            }
            catch (Exception err)
            {

                return Json(new { Status = "2", StatusDesc = "刪除失敗,請洽管理人員" + err.Message });
            }

        }
        #endregion


        public ActionResult Cooperation()
        {
            return View();
        }

        #region CooperationSearch 合作廠商圖片
        [HttpPost]
        public ActionResult CooperationSearch()
        {
             //ID >1 合作廠商
            var q = HomePageService.GetAll().Where(i=>i.Opt== "合作廠商").ToList();
            int totalLen = Convert.ToInt16(q.Count());
            var result = from Row in q
                         select new HomePage
                         {
                             Id = Row.Id,
                             Title = Row.Title,
                             Description = Row.Description,
                             Image = Row.Image,
                             Enable = Row.Enable,
                             CreateTime=Row.CreateTime
                         };
            JQueryDataTableResponse<HomePage> jqDataTableRs = JQueryDataTableHelper<HomePage>.GetResponse(1, totalLen, totalLen, result.ToList());
            return Json(jqDataTableRs);
        }
        #endregion
        [HttpGet]
        public ActionResult CooperationEdit(int id = 0)
        {
            var _HomePage = HomePageService.GetByID(id);
            if (!String.IsNullOrEmpty(_HomePage.Image))
            {
                ViewBag.Image = _HomePage.Image.ToString();
            }
            return View(_HomePage);
        }
        #region 大圖修改
        [HttpPost]
        public ActionResult CooperationEdit(FormCollection form, HttpPostedFileBase Image)
        {
    
            HomePage _HomePage = HomePageService.GetByID(int.Parse(form["Id"]));
            _HomePage.Title = form["Title"];
            _HomePage.Description = form["Description"];
            _HomePage.Enable = true;
            _HomePage.CreateTime = DateTime.Now;
            _HomePage.ModifyTime = DateTime.Now;
            _HomePage.Opt = "合作廠商";
            if (Image != null)
            {
                var q = HomePageService.GetAll().ToList();
                //判斷圖片名稱是否重複
                var rptimg = (from Row in q
                              select new HomePage
                              {
                                  Image = Row.Image
                              }).Where(x => x.Image == Image.FileName);
                if (rptimg != null)
                {
                    if (rptimg.Count() != 0)
                    {
                        ViewBag.Image = _HomePage.Image.ToString();
                        ViewBag.ErrMessage = "已有圖片名稱：" + Image.FileName + "，請檢查";
                        return View(_HomePage);
                    }
                    else
                    {
                        string OrgImgName = form["OrgImgName"].ToString();
                        if (OrgImgName != "")
                        {
                            string strPath1 = string.Format("~/Image/Home/{0}", OrgImgName);
                            var fullPath = Request.MapPath(strPath1);
                            System.IO.File.Delete(fullPath);
                        }
                    }
                }
                string strPath = Request.PhysicalApplicationPath + @"Image\Home\" + Image.FileName;
                Image.SaveAs(strPath);
                _HomePage.Image = Image.FileName;
            }
            var Message = HomePageService.Update(_HomePage);
            if (Message.Success == true)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ErrMessage = "請洽程式管理人員" + Message.Exception.ToString();
                return View(_HomePage);
            }

            return RedirectToAction("Cooperation");
        }
        #endregion


        public ActionResult GetImageFile(string fileName)
        {
            return File("~/Image/Home/" + fileName, "image/png");
        }

    }
}