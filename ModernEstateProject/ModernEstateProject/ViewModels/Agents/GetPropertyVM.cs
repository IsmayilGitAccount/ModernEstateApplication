namespace ModernEstateProject.ViewModels.Agents
{
    public class GetPropertyVM
    {
        public int Id { get; set; }
        public int BedroomCount { get; set; }
        public int BathroomCount { get; set; }
        public int GarageCount { get; set; }
        public int Area { get; set; }
        public decimal Price { get; set; }
        public string Photo { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
    }
}
