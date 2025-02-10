using ModernEstate.Domain.Entities.Base;

namespace ModernEstate.Domain.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime PostedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int AgencyId { get; set; }
        public Agency Agency { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public string Photo { get; set; }
        public string Description { get; set; }
    }
}
