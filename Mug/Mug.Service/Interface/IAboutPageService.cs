using Mug.Dao;
using Mug.Service.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

 

namespace Mug.Service.Interface
{
    public interface IAboutPageService
    {
        IResult Create(AboutPage instance);

        IResult Update(AboutPage instance);

        IResult Delete(int Id);

        bool IsExists(int Id);

        AboutPage GetByID(int Id);

        IEnumerable<AboutPage> GetAll();

        IEnumerable<AboutPage> GetByAboutPage(int ID);
    }
}
