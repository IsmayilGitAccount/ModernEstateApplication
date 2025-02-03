namespace ModernEstateDemo.Models
{
    public class Agent:BaseEntity
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public string FacebookLink { get; set; }
        public string XLink { get; set; }
        public string LinkedinLink { get; set; }
        public string InstagramLink { get; set; }
        public int AgencyId { get; set; }
        public Agency Agency { get; set; }
        public ICollection<Property> Properties { get; set; }
    }
}
