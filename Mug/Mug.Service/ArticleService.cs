using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mug.Dao;
using Mug.Dao.Repository;
using Mug.Dao.Interface;
using Mug.Service.Interface;
using Mug.Service.Misc;

namespace Mug.Service
{
   public class ArticleService : IArticleService
    {
        private IRepository<Article> repository = new GenericRepository<Article>();


        public IResult Create(Article instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException();
            }

            IResult result = new Result(false);
            try
            {
                this.repository.Create(instance);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Exception = ex;
            }
            return result;
        }

        public IResult Update(Article instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException();
            }

            IResult result = new Result(false);
            try
            {
                this.repository.Update(instance,instance.Article_ID);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Exception = ex;
            }
            return result;
        }

        public IResult Delete(int Blog_id)
        {
            IResult result = new Result(false);

            if (!this.IsExists(Blog_id))
            {
                result.Message = "找不到資料";
            }

            try
            {
                var instance = this.GetByID(Blog_id);
                this.repository.Delete(instance);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Exception = ex;
            }
            return result;
        }

        public bool IsExists(int Id)
        {
            return this.repository.GetAll().Any(x => x.Post_Id == Id);
        }

        //取多個
        public Article GetByID(int Id)
        {
            return this.repository.Get(x => x.Post_Id == Id);
        }

        public Article GetByArt(int Id)
        {
            return this.repository.Get(x => x.Article_ID == Id);
        }

        public IEnumerable<Article> GetAll()
        {
            return this.repository.GetAll();
        }
        public IEnumerable<Article> GetByArticle(int Id)
        {
            return this.repository.GetAll().Where(x => x.Post_Id == Id);
        }
      }
}

