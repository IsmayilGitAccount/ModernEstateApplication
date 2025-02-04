namespace ModernEstateApplication.Models
{
    public class Agency : BaseEntity
    {
        public string AgencyName { get; set; }
        public string Description { get; set; }

        //Relational
        public ICollection<Agent> Agents { get; set; }
        public ICollection<Property> Properties { get; set; }
    }
}
