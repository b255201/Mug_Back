using Mug_Front.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Mug_Front.Helper
{
    public class DaoHelper
    {
        DbContext db = new DbContext("MugFactoryEntities");
        #region 取得首頁文章
        public List<ArticleViewModel> GetHome()
        {
            List<ArticleViewModel> Data = null;
            try
            {
                string sql = "select Blog_id,Image,Enable,Categore from Blogger where   Enable=1";
                return db.Database.SqlQuery<ArticleViewModel>(sql).ToList();
            }
            catch (Exception err)
            {
                return Data;
                throw;
            }
        }
        #endregion

        #region 取得關於頁
        public List<ArticleViewModel> GetAbout()
        {
            List<ArticleViewModel> Data = null;
            try
            {
                string sql = "select Blog_id,Title,Image,Enable,Categore,Sub_Title,Contents,Id from Blogger b left join Article a on b.Blog_id=a.Post_Id where  Enable=1";
                return db.Database.SqlQuery<ArticleViewModel>(sql).ToList();
            }
            catch (Exception err)
            {
                return Data;
                throw;
            }
        }
        #endregion

        #region 取得服務頁
        public List<ArticleViewModel> GetService()
        {
            List<ArticleViewModel> Data = null;
            try
            {
                string sql = "select Blog_id,Title,Image,Enable,Categore,Sub_Title,Contents,Id,Category_Opt from Blogger b left join Article a on b.Blog_id=a.Post_Id where  Enable=1";
                return db.Database.SqlQuery<ArticleViewModel>(sql).ToList();
            }
            catch (Exception err)
            {
                return Data;
                throw;
            }
        }
        #endregion

        #region 取得產品頁
        public List<ArticleViewModel> GetProduct()
        {
            List<ArticleViewModel> Data = null;
            try
            {
                string sql = "select Blog_id,Title,Image,Enable,Categore,Sub_Title,Contents,Id,Category_Opt from Blogger b left join Article a on b.Blog_id=a.Post_Id where  Enable=1 ";
      
                return db.Database.SqlQuery<ArticleViewModel>(sql).ToList();
            }
            catch (Exception err)
            {
                return Data;
                throw;
            }
        }
        #endregion
    }
}