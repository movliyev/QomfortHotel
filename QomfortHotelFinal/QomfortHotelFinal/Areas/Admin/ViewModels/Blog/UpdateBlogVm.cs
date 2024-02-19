namespace QomfortHotelFinal.Areas.Admin.ViewModels.Blog
{
    public class UpdateBlogVm
    {
        public string Desc1 { get; set; }
        public string Desc2 { get; set; }
        public string Title { get; set; }
        public IFormFile HoverPhoto { get; set; }
        public IFormFile MainPhoto { get; set; }
        public string HoverImage { get; set; }
        public string MainImage { get; set; }
    }
}
