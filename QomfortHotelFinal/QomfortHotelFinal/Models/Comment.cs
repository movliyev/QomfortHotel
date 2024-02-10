namespace QomfortHotelFinal.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public string CoomentUser { get; set; }
        public DateTime CommentDate { get; set; }
        public string CommentContent { get; set; }
        public bool CommentState { get; set; }
        public int? BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}
