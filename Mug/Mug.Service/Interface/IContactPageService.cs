using Mug.Dao;
using Mug.Service.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

 

namespace Mug.Service.Interface
{
    public interface IContactService
    {
        IResult Create(Contact instance);

        IResult Update(Contact instance);

        IResult Delete(int Id);

        bool IsExists(int Id);

        Contact GetByID(int Id);

        IEnumerable<Contact> GetAll();

        IEnumerable<Contact> GetByContact(int ID);
    }
}
