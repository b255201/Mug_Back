using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mug.Service;
using Mug.Service.Interface;
using Mug.Dao;
using Mug.Service.UI;
namespace Mug.Service.UI
{
   
   public class LanguageOption
    {
        private ILanguageService LanguageService = new LanguageService();

        public IEnumerable<Language> getLanguage()
        {
            var result = LanguageService.GetAll();
            return result;
        }
    }
}
