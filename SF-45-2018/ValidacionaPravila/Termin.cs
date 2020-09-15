using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SF_45_2018.ValidacionaPravila
{
    class Termin : ValidationRule

    {
        public static Regex regex = new Regex(@"(((0[1-9])|([1-9])|(1[0-2])):([0-5])(0|5)\s(A|P|a|p)(M|m))");

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string s = value as string;

            if (s == null || String.IsNullOrWhiteSpace(s))
            {
                return new ValidationResult(false, "Ovo polje je obavezno!");
            }
            else if (regex.Match(s).Success)
            {
                return new ValidationResult(true, null);
            }
            else
            {
                return new ValidationResult(false, "Pogresan Format!");
            }
        }
    }
}
