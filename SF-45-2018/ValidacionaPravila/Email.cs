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
    public class Email:ValidationRule
    
        {
            public static Regex regex = new Regex(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                                    + "@"
                                    + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$", RegexOptions.IgnoreCase);

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

