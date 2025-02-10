using ModernEstate.Domain.Entities;

namespace ModernEstate.Application.ViewModels.Posts
{
    public class GetPostVM
    {
        public List<Post> Posts { get; set; }
        public List<Post> RecentlyPosts { get; set; }
        public Post Post { get; set; }
    }
}
