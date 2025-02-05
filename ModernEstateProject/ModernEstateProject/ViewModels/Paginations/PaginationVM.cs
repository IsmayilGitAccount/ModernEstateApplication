using ModernEstateProject.ViewModels.Agents;
using ModernEstateProject.ViewModels.Properties;

namespace ModernEstateProject.ViewModels.Paginations
{
    public class PaginationVM
    {
        public double TotalPage { get; set; }
        public int CurrentPage { get; set; }
        public PropertyVM Items { get; set; }
    }
}
