using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace Hotel_Managment.MVC_Qomfort_Project.Utilities.Extensions
{
    public static class StringFormat
    {
        public static bool isDigit(this string Name)
        {
            return Name.Any(char.IsDigit); 
        }
        public static bool CheckEmail(this string Email)
        {
            string emailregex = @"^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$";
            Regex regex = new Regex(emailregex);
            return regex.IsMatch(Email);

        }
    }
}
