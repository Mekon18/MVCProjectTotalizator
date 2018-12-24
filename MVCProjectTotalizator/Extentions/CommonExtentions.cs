using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCProjectTotalizator
{
    public static class CommonExtentions
    {
        public static string ReplaceSeporator(this double d)
        {
            return d.ToString().Replace(",", ".");
        }
    }
}