namespace QomfortHotelFinal.Areas.Memmber.ViewModels
{
    public class UserEditVM
    {
        public string name { get; set; }
        public string surname { get; set; }

        public string password { get; set; }
        public string confirmpassword { get; set; }
        public string email { get; set; }
        public string phonenumber { get; set; }
        public string? image { get; set; }
        public IFormFile? Photo { get; set; }
    }
}
