using QomfortHotelFinal.Models;

namespace QomfortHotelFinal.ViewModels
{
    public class OrderVM
    {
        public bool? Status { get; set; }
        public decimal TotalPrice { get; set; }

        public List<Reservation>? Reservations { get; set; }
        public DateTime PurchasedAt { get; set; }
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}
