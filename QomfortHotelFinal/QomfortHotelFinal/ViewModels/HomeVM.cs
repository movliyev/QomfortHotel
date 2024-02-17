using QomfortHotelFinal.Models;
using System.ComponentModel.DataAnnotations;

namespace QomfortHotelFinal.ViewModels
{
    public class HomeVM
    {
        public List<Room> Rooms { get; set; }
        public List<Slide> Slides { get; set; }
        public List<Servicee> Servicees { get; set; }
        public HomeAbout HomeAbouts { get; set; }
        public List<Gallery> Galleries { get; set; }
        public List<Blog> Blogs { get; set; }
        public List<Testimonial> Testimonials { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ArrivalDate { get; set; } = null;

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DeparturDate { get; set; } = null;

        [Required]

        public int Adult { get; set; }
        public int Children { get; set; }

        public IList<Room> Room { get; set; } = new List<Room>();
    }
}
