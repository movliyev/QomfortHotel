namespace QomfortHotelFinal.Models
{
    public class Message
    {
        public int Id { get; set; }

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public string Subject { get; set; }
        public string Messages { get; set; }
        public DateTime MessageDate { get; set; }
        public int? Rate { get; set; }

    }
}
