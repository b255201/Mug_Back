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
                var blog = BloggerService.GetAll();
                var articles = ArticleService.GetAll();
                 result = (from b in blog
                              join a in articles
                              on new
                              {
                                  Post_Id = b.Blog_id,

                              } equals
                             new { a.Post_Id } into subGrp
                              from s in subGrp.DefaultIfEmpty()
                              select new ArticleViewModel
                              {
                                  Blog_id = b.Blog_id,
                                  Title = s.Title,
                                  Image = b.Image,
                                  Enable = b.Enable,
                                  Id = s.Id,
                                  Categore= b.Categore,
                                  Category_Opt=b.Category_Opt
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