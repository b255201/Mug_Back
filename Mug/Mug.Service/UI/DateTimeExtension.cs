using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mug.Service.UI
{
    public class DateTimeExtension
    {
        public static string ToYMD(DateTime? dt)
        {
            if (dt == null)
            {
                return "";
            }
            else
            {
                return dt.Value.ToString("yyyy-MM-dd");
            }
        }
    }
}
