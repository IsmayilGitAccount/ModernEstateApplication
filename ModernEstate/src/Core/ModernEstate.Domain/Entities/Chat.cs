using ModernEstate.Domain.Entities.Account;

namespace ModernEstate.Domain.Entities
{
    public class Chat
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public int AgentId { get; set; }
        public Agent Agent { get; set; }
        public DateTime SentAt { get; set; }
    }
}
