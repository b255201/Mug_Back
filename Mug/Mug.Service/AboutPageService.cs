﻿using System;
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
    public class AboutPageService : IAboutPageService
    {
        private IRepository<AboutPage> repository = new GenericRepository<AboutPage>();


        public IResult Create(AboutPage instance)
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

        public IResult Update(AboutPage instance)
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

        public IResult Delete(int categoryID)
        {
            IResult result = new Result(false);

            if (!this.IsExists(categoryID))
            {
                result.Message = "找不到資料";
            }

            try
            {
                var instance = this.GetByID(categoryID);
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

        public AboutPage GetByID(int Id)
        {
            return this.repository.Get(x => x.Id == Id);
        }

        public IEnumerable<AboutPage> GetAll()
        {
            return this.repository.GetAll();
        }
        public IEnumerable<AboutPage> GetByAboutPage(int Id)
        {
            return this.repository.GetAll().Where(x => x.Id == Id);
        }
    }
}

