using Mug.Models;
using Mug.Service;
using Mug.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mug.HtmlHelper
{
    public  class LinqTable
    {
        private IBloggerService BloggerService = new BloggerService();
        private ILanguageService LanguageService = new LanguageService();
        private IArticleService ArticleService = new ArticleService();

        #region 語言選擇
        public List<SelectListItem> option()
        {
            List<SelectListItem> select = null;
            try
            {
                var lang = LanguageService.GetAll();
               select = new List<SelectListItem>();
                foreach (var item in lang)
                {
                    select.Add(new SelectListItem() { Text = item.Lang_val, Value = item.Id.ToString() });
                }
                select.Add(new SelectListItem() { Text = "全部", Value = "" });
                //預設選擇哪一筆
                select.Where(q => q.Value == "1").First().Selected = true;
                select.Where(q => q.Value == "1").First().Selected = true;
                return select;
            }
            catch (Exception err)
            {
                return select;
                throw;
            }
        }
        #endregion

        #region 讀取文章
        public IEnumerable<ArticleViewModel> ArtHelper()
        {
            IEnumerable<ArticleViewModel> result = null;
            try
            {
                //過濾掉 首頁大圖 跟誠品展覽
                var blog = BloggerService.GetAll();
                var articles = ArticleService.GetAll();
                result = (from b in blog
                                  join a in articles
                                  on b.Blog_id equals a.Post_Id
                                  into ps from a in ps.DefaultIfEmpty()
                                  select new ArticleViewModel
                                  {
                                      Blog_id = b.Blog_id,
                                      Title = a.Title,
                                      Image = b.Image,
                                      Enable = b.Enable,
                                      Id = a.Id,
                                      Categore = b.Categore,
                                      Category_Opt = b.Category_Opt
                                  }).ToList();            
                return result;
            }
            catch (Exception err)
            {
                return result;
                throw;
            }
        }
        #endregion

        #region 只有圖片 沒有文章
        public IEnumerable<ArticleViewModel> BlogHelper()
        {
            IEnumerable<ArticleViewModel> result = null;
            try
            {
                var blog = BloggerService.GetAll();
                var articles = ArticleService.GetAll();
                result = (from b in blog                       
                          select new ArticleViewModel
                          {
                              Blog_id = b.Blog_id,
                              Image = b.Image,
                              Enable = b.Enable,
                              Categore = b.Categore,
                              Category_Opt = b.Category_Opt
                          }).ToList();
                return result;
            }
            catch (Exception err)
            {
                return result;
                throw;
            }
        }
        #endregion 
    }
}