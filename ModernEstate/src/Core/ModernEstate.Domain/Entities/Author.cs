namespace ModernEstate.Domain.Entities
{
    public class Author 
    {
        public int Id { get; set; }
        public string AuthorName { get; set; }
        public string Photo { get; set; }
        public string Description { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
