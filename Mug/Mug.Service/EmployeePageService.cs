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
    public class EmployeePageService : IEmployeePageService
    {
        private IRepository<Employee> repository = new GenericRepository<Employee>();


        public IResult Create(Employee instance)
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

        public IResult Update(Employee instance)
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

        public IResult Delete(int EmpId)
        {
            IResult result = new Result(false);

            if (!this.IsExists(EmpId))
            {
                result.Message = "找不到資料";
            }

            try
            {
                var instance = this.GetByID(EmpId);
                this.repository.Delete(instance);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Exception = ex;
            }
            return result;
        }

        public bool IsExists(int EmpId)
        {
            return this.repository.GetAll().Any(x => x.EmpId == EmpId);
        }

        public Employee GetByID(int EmpId)
        {
            return this.repository.Get(x => x.EmpId == EmpId);
        }
        public Employee GetByAccountID(string Account)
        {
            return this.repository.Get(x => x.Account == Account);
        }

        public IEnumerable<Employee> GetAll()
        {
            return this.repository.GetAll();
        }
        public IEnumerable<Employee> GetByEmpoylee(int EmpId)
        {
            return this.repository.GetAll().Where(x => x.EmpId == EmpId);
        }
    }
}

