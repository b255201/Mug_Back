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
   public class BloggerService : IBloggerService
    {
        private IRepository<Blogger> repository = new GenericRepository<Blogger>();


        public IResult Create(Blogger instance)
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

        public IResult Update(Blogger instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException();
            }

            IResult result = new Result(false);
            try
            {
                this.repository.Update(instance);
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
            return this.repository.GetAll().Any(x => x.Blog_id == Id);
        }

        public Blogger GetByID(int Id)
        {
            return this.repository.Get(x => x.Blog_id == Id);
        }

        public IEnumerable<Blogger> GetAll()
        {
            return this.repository.GetAll();
        }
        public IEnumerable<Blogger> GetByBlogger(int Id)
        {
            return this.repository.GetAll().Where(x => x.Blog_id == Id);
        }
      }
}

