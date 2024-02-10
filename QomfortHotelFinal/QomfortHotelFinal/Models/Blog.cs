namespace QomfortHotelFinal.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public DateTime BlogDate { get; set; }
        public string Desc1 { get; set; }
        public string Desc2 { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string HoverImage { get; set; }
        public string MainImage { get; set; }
        public List<Comment>? Comments { get; set; }
    }
}
