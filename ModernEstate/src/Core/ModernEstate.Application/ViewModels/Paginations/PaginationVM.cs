using ModernEstate.Application.ViewModels.Properties;

namespace ModernEstate.Application.ViewModels.Paginations
{
    public class PaginationVM<T>
    {
        public double TotalPage { get; set; }
        public int CurrentPage { get; set; }
        public List<T> Items { get; set; }
    }
}
