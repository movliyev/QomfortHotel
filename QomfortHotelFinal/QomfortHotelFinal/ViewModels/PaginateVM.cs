namespace QomfortHotelFinal.ViewModels
{
    public class PaginateVM<T> where T : class, new()
    {
        public double TotalPage { get; set; }
        public int CurrentPage { get; set; }

        public List<T> Items { get; set; }
    }
}
