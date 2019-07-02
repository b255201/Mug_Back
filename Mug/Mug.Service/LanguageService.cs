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
    public class LanguageService : ILanguageService
    {
        private IRepository<Language> repository = new GenericRepository<Language>();


        public IResult Create(Language instance)
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

        public IResult Update(Language instance)
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

        public IResult Delete(int Id)
        {
            IResult result = new Result(false);

            if (!this.IsExists(Id))
            {
                result.Message = "找不到資料";
            }

            try
            {
                var instance = this.GetByID(Id);
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
            return this.repository.GetAll().Any(x => x.Id == Id);
        }

        public Language GetByID(int Id)
        {
            return this.repository.Get(x => x.Id == Id);
        }

        public IEnumerable<Language> GetAll()
        {
            return this.repository.GetAll();
        }
        public IEnumerable<Language> GetByLanguage(int Id)
        {
            return this.repository.GetAll().Where(x => x.Id == Id);
        }
    }
}

