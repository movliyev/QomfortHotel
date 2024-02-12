using QomfortHotelFinal.Models;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace QomfortHotelFinal.Areas.Admin.ViewModels
{
    public class CreateRoomVM
    {
        [Required]
        [MaxLength(100)]
        [MinLength(3,ErrorMessage = "No less than 3 characters")]
        public string Name { get; set; }
        public string Description { get; set; }
        public string DetailDescription { get; set; }
        public decimal Price { get; set; }
        public int Size { get; set; }
        public int Bed { get; set; }
        public int Capacity { get; set; }
        public int CategoryId { get; set; }
        public List<Category> Categorys { get; set; }
        public List<int> Serviceeids { get; set; }
        public List<int> Facilityids { get; set; }
        public int BathRoom { get; set; }
        public bool? Status { get; set; }
        public List<Facility>? Facilitiys { get; set; }
        public List<Servicee>? Servicees { get; set; }
        public IFormFile? MainPhoto { get; set; }
        public List<IFormFile>? Photos { get; set; }


      
       
    }
}
