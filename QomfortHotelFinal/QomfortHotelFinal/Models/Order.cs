namespace QomfortHotelFinal.Models
{
    public class Order
    {
        public int Id { get; set; }
        public bool? Status { get; set; }
        public decimal TotalPrice { get; set; }
        

        public List<Reservation> Reservations { get; set; }
        public DateTime PurchasedAt { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
