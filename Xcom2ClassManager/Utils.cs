using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xcom2ClassManager
{
    public static class Utils
    {
        public static int? parseStringToInt(string str)
        {
            int result = 0;

            if (string.IsNullOrEmpty(str))
            {
                return null;
            }

            if (!int.TryParse(str, out result))
            {
                return null;
            }

            return result;
        }

        public static string encaseStringInQuotes(string str)
        {
            return "\"" + str + "\"";
        }

        public static string getIndexString(int index)
        {
            return "[" + index.ToString() + "]";
        }
    }
}
