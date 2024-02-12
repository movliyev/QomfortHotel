using System.ComponentModel.DataAnnotations.Schema;

namespace QomfortHotelFinal.Areas.Admin.ViewModels
{
    public class CreateSlideVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
