namespace QomfortHotelFinal.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public DateTime CommentDate { get; set; }
        public string CommentContent { get; set; }
        public bool CommentStatus { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}
