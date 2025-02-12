namespace ModernEstate.Domain.Entities
{
    public class Wishlist
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int PropertyId { get; set; }
        public Property Property { get; set; }
    }
}
