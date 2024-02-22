using QomfortHotelFinal.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace QomfortHotelFinal.Areas.Admin.ViewModels
{
    public class CreateRoomVM
    {
        [Required(ErrorMessage = "A RoomTitle must be included")]
        [MaxLength(100,ErrorMessage = "No more than 100 characters")]
        [MinLength(3,ErrorMessage = "No less than 3 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "A Description must be included")]
        [MaxLength(256, ErrorMessage = "No more than 256 characters")]
        [MinLength(3, ErrorMessage = "No less than 3 characters")]
        public string Description { get; set; }
        [Required(ErrorMessage = "A Description must be included")]
        [MaxLength(256, ErrorMessage = "No more than 256 characters")]
        [MinLength(3, ErrorMessage = "No less than 3 characters")]
        public string DetailDescription { get; set; }
        [Required(ErrorMessage = "A Price must be included")]
        [Range(1, 100000000, ErrorMessage = "There is no number less than 1 or more than 100000000")]

        public decimal Price { get; set; }
        [Required(ErrorMessage = "A size must be included")]
        [Range(1, 500, ErrorMessage = "There is no number less than 1 or more than 500")]

        public int Size { get; set; }
        [Required(ErrorMessage = "A Bed must be included")]
        [Range(1, 20, ErrorMessage = "There is no number less than 1 or more than 20")]

        public int Bed { get; set; }
        [Required(ErrorMessage = "A Capacity must be included")]
        [Range(1,50,ErrorMessage = "There is no number less than 1 or more than 50")]
        public int Capacity { get; set; }
        
        public bool Status { get; set; }

        public int CategoryId { get; set; }
        public List<Category>? Categorys { get; set; }
        public List<int> Serviceeids { get; set; }
        public List<int> Facilityids { get; set; }
        [Range(1, 20, ErrorMessage = "There is no number less than 1 or more than 20")]
        [Required(ErrorMessage = "A Bathroom must be included")]

        public int BathRoom { get; set; }
       
        public List<Facility>? Facilitiys { get; set;}
      
        public List<Servicee>? Servicees { get; set; }
        [Required(ErrorMessage = "A FacilityName must be included")]
        [NotMapped]
        public IFormFile? MainPhoto { get; set; }
        [Required(ErrorMessage = "A FacilityName must be included")]
        [NotMapped]

        public List<IFormFile>? Photos { get; set; }
            
       
    }
}
