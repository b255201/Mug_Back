using Mug.Dao;
using Mug.Service.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mug.Service.Interface
{
    public interface IBloggerService
    {
        IResult Create(Blogger instance);

        IResult Update(Blogger instance);

        IResult Delete(int Id);

        bool IsExists(int Id);

        Blogger GetByID(int Id);

        IEnumerable<Blogger> GetAll();

        IEnumerable<Blogger> GetByBlogger(int ID);

    }
}
