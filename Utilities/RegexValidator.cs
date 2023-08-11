using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;

namespace FactoryAPI.Utilities
{
    static public class RegexValidator
    {
        public static bool IsValidName(string name)
        {
            Regex regex = new("^[a - zA - Zа - яА - Я] +[-]?[a - zA - Zа - яА - Я] *$");
            return regex.IsMatch(name);
        }

        public static bool IsValidCompanyName(string name)
        {
            Regex regex = new("^(ОАО)?(ООО)?(ЗАО)?[\\s]*([а-яА-Я-\\sA-Za-z]+)?([\"][а-яА-Я-\\sA-Za-z]+[\"])?$");
            return regex.IsMatch(name);
        }

        public static bool IsValidPhone_number(string value)
        {
            Regex regex = new("^[+]?[0-9]{1,3}([(][0-9]{2,3}[)])?([0-9]{2,3})?[0-9]{3}[-]?[0-9]{2}[-]?[0-9]{2}$");
            return regex.IsMatch(value);
        }
    }
}
