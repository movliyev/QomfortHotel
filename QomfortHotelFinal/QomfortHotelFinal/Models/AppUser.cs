using Microsoft.AspNetCore.Identity;

namespace QomfortHotelFinal.Models
{
    public class AppUser:IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public string? UserImage { get; set; }
        public string? PhoneNumber { get; set; }
        public List<Reservation>? Reservations { get; set; }
    }
}
