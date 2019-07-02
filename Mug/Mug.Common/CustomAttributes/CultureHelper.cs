using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Mug.Common.CustomAttributes
{
    public enum LanguageEnum
    {

        [Description("zh-TW")]
        English = 2,
        [Description("ja-JP")]
        Japanese = 3
    }

    class CultureHelper
    {
    }
}
