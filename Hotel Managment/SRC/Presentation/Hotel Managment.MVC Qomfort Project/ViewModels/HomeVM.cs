using Hotel_Managment.Domain.Entities;

namespace Hotel_Managment.MVC_Qomfort_Project.ViewModels
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

    }
}
