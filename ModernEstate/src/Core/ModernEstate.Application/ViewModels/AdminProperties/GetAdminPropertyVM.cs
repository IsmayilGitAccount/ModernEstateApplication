namespace ModernEstate.MVC.Areas.Admin.ViewModels.Property
{
    public class GetAdminPropertyVM
    {
        public int Id { get; set; }
        public string Photo { get; set; }
        public string AgencyName { get; set; }
        public string AgentName { get; set; }
        public string Location { get; set; }
        public string CategoryName { get; set; }
        public decimal Price { get; set; }
    }
}
