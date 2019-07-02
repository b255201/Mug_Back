using Mug.Dao;
using Mug.Service.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

 

namespace Mug.Service.Interface
{
    public interface ILanguageService
    {
        IResult Create(Language instance);

        IResult Update(Language instance);

        IResult Delete(int Id);

        bool IsExists(int Id);

        Language GetByID(int Id);

        IEnumerable<Language> GetAll();

        IEnumerable<Language> GetByLanguage(int ID);
    }
}
