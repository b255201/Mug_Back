using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mug.Dao
{
  public  class SqlCommand
    {
        DbContext db = new DbContext("MugFactoryEntities");
        // GET: About
        public string GetBySeq()
        {
            try
            {
                var db = new MugFactoryEntities();
                string sql = "update SerialNumber set SeqId=SeqId + 1";
                string SerialNumber = "";
                using (var context = new MugFactoryEntities())
                {
                    var SeqId = context.Database.SqlQuery<int>("SELECT SeqId FROM SerialNumber").ToList();
                    context.Database.ExecuteSqlCommand(sql);
                     SerialNumber = SeqId.First().ToString();
                }
               
                return SerialNumber;
            }
            catch (Exception err)
            {
                return err.ToString();
            }
        }
        public string DeleteAllArticle(string Post_Id)
        {
            try
            {
                var db = new MugFactoryEntities();
                string sql = "delete from Article where Post_Id="+ Post_Id;
                string Message = "";
                using (var context = new MugFactoryEntities())
                {
                    context.Database.ExecuteSqlCommand(sql);
                    Message = "0";
                }
                return Message;
            }
            catch (Exception err)
            {
                return err.ToString();
            }
        }

        public string TranGetArticleBlog(string Post_Id)
        {
            string Message = "";
            try
            {
                string sql = "delete from Article where Post_Id=" + Post_Id;
                string blogSql = "delete from Blogger where Blog_Id=" + Post_Id;

                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Database.ExecuteSqlCommand(sql);
                        db.Database.ExecuteSqlCommand(blogSql);
                        transaction.Commit();
                        Message = "0";
                    }
                    catch (Exception ex)
                    {
                        Message = ex.ToString();
                        transaction.Rollback();
                    }
                }
                return Message;
            }
            catch (Exception err)
            {
                return Message;
            }
        }


    }
}
