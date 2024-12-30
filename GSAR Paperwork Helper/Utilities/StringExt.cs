using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSAR_Paperwork_Helper.Utilities
{
    public static class StringExt
    {
        public static string EscapeQuotes(this string str)
        {
            if (string.IsNullOrEmpty(str)) { return str; }
            else { return str.Replace("\"", "\"\""); }
        }

    }
}
