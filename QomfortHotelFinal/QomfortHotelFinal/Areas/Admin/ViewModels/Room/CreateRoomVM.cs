using QomfortHotelFinal.Models;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace QomfortHotelFinal.Areas.Admin.ViewModels
{
    public class CreateRoomVM
    {
        [Required]
        [MaxLength(100,ErrorMessage = "No more than 100 characters")]
        [MinLength(3,ErrorMessage = "No less than 3 characters")]
        public string Name { get; set; }
        [Required]
        [MaxLength(256, ErrorMessage = "No more than 256 characters")]
        [MinLength(3, ErrorMessage = "No less than 3 characters")]
        public string Description { get; set; }
        [Required]
        [MaxLength(256, ErrorMessage = "No more than 256 characters")]
        [MinLength(3, ErrorMessage = "No less than 3 characters")]
        public string DetailDescription { get; set; }
        [Required]

        public decimal Price { get; set; }
        [Required]

        public int Size { get; set; }
        [Required]

        public int Bed { get; set; }
        [Required]

        public int Capacity { get; set; }
        public bool Status { get; set; }
        public int CategoryId { get; set; }
        public List<Category>? Categorys { get; set; }
        public List<int> Serviceeids { get; set; }
        public List<int> Facilityids { get; set; }
        public int BathRoom { get; set; }
        public List<Facility>? Facilitiys { get; set;}
        public List<Servicee>? Servicees { get; set; }
        public IFormFile? MainPhoto { get; set; }
        public List<IFormFile>? Photos { get; set; }
            
       
    }
}
