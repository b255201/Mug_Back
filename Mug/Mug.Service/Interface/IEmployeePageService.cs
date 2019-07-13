using Mug.Dao;
using Mug.Service.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

 

namespace Mug.Service.Interface
{
    public interface IEmployeePageService
    {
        IResult Create(Employee instance);

        IResult Update(Employee instance);

        IResult Delete(int Id);

        bool IsExists(int Id);

        Employee GetByID(int Id);

        IEnumerable<Employee> GetAll();

        Employee GetByAccountID(string Account);
        
        IEnumerable<Employee> GetByEmpoylee(int ID);
    }
}
