namespace ModernEstate.Application.ViewModels.AdminPaginations
{
    public class PaginationVM<T>
    {
        public double TotalPage { get; set; }
        public int CurrentPage { get; set; }
        public List<T> Items { get; set; }
    }
}
