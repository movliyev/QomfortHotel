using System.Text.RegularExpressions;

namespace QomfortHotelFinal.Utilities.Extensions
{
    public static class StringFormat
    {
        public static bool isDigit(this string Name)
        {
            return Name.Any(char.IsDigit);
        }
        public static string Capitalize(this string name)
        {
            name=name.Trim();
            name=name.Substring(0,1).ToUpper()+name.Substring(1).ToLower();
            return name;
        }
        public static bool CheckEmail(this string Email)
        {
            string emailregex = @"^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$";
            Regex regex = new Regex(emailregex);
            return regex.IsMatch(Email);

        }
        public static bool CheckPhoneNumber(this string Phone)
        {
            string phone = @"^\s*(?:\+?(\d{1,3}))?[-. (]*(\d{3})[-. )]*(\d{3})[-. ]*(\d{4})(?: *x(\d+))?\s*$";
            Regex regex = new Regex(phone);
            return regex.IsMatch(phone);

        }
    }
}
