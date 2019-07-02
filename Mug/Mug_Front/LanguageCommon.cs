using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web;
using System.Web.Mvc;

namespace Mug_Front
{
    public class LanguageCommon
    {
        public string GetUserLang(string lang)
        {
            if (lang == "")
            {
                lang = "1";
            }
            else if(lang == "zh-TW")
            {
                lang = "2";
            }
            else
            {
                lang = "1";
            }
            return lang;
        }
    }

}