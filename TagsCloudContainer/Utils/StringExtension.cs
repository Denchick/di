using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.Utils
{
    public static class StringExtension
    {
        public static string CapitalizeFirstLetter(this string str)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
        }
    }
}
