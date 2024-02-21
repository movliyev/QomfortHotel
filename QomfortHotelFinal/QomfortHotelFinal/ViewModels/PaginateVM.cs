using QomfortHotelFinal.Models;

namespace QomfortHotelFinal.ViewModels
{
    public class PaginateVM<T> where T : class, new()
    {
        public double TotalPage { get; set; }
        public int CurrentPage { get; set; }

        public List<T> Items { get; set; }

        public List<Category> Categories { get; set; }
        public List<Servicee> Services { get; set; }
        public List<Room> Rooms { get; set; }
        public int? Orrder { get; set; }
        public int? CategoryId { get; set; }
        public int? ServiceId { get; set; }
        public string? Search { get; set; }
        public List<Blog> Blogs { get; set; }
    }
}
