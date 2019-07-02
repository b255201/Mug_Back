using Mug.Dao;
using Mug.Service.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

 

namespace Mug.Service.Interface
{
    public interface IArticleService
    {
        IResult Create(Article instance);

        IResult Update(Article instance);

        IResult Delete(int Id);

        bool IsExists(int Id);

        Article GetByID(int Id);

        Article GetByArt(int Id);

        IEnumerable<Article> GetAll();

        IEnumerable<Article> GetByArticle(int ID);
    }
}
