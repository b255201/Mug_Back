using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mug.Dao
{
  public  class SqlCommand
    {
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

    }
}
