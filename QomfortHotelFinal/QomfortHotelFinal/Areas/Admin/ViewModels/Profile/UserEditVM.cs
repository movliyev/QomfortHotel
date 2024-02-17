using System.ComponentModel.DataAnnotations.Schema;

namespace QomfortHotelFinal.Areas.Admin.ViewModels
{
    public class UserEditVM
    {
        public string name { get; set; }
        public string surname { get; set; }
        public string phonenumber { get; set; }
        public string image { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
