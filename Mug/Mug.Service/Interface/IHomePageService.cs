using Mug.Dao;
using Mug.Service.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

 

namespace Mug.Service.Interface
{
    public interface IHomePageService
    {
        IResult Create(HomePage instance);

        IResult Update(HomePage instance);

        IResult Delete(int Id);

        bool IsExists(int Id);

        HomePage GetByID(int Id);

        IEnumerable<HomePage> GetAll();

        IEnumerable<HomePage> GetByHomePage(int ID);
    }
}
