using QomfortHotelFinal.Models;

namespace QomfortHotelFinal.Areas.Admin.ViewModels.Dashboard
{
    public class DashboardVM
    {
        public List<Room> Rooms { get; set; }
        public List<Message> Messages { get; set; }
        public List<Slide> Slides { get; set; }
        public List<Reservation> Reservations { get; set; }
        public List<Servicee> Services { get; set; }
    }
}
