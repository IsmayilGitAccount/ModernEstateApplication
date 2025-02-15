using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Application.ViewModels.Posts;
using ModernEstate.Domain.Entities;
using ModernEstate.Persistence.Data;

namespace ModernEstate.MVC.Controllers
{
    public class PostController(AppDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            GetPostVM postVM = new GetPostVM()
            {
                Posts = await _context.Posts.Include(p => p.Agency).Include(p => p.Author).OrderByDescending(p=>p.Id).ToListAsync(),
                RecentlyPosts = await _context.Posts.Include(p => p.Agency).Include(p => p.Author).Take(2).ToListAsync(),
            };
            return View(postVM);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id is null || id <= 0) return BadRequest();

            Post post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);

            if (post == null) return NotFound();

            GetPostVM postVM = new GetPostVM()
            {
                Posts = await _context.Posts.Include(p => p.Agency).Include(p => p.Author).Where(p=>p.Id ==id).ToListAsync(),
                RecentlyPosts = await _context.Posts.Include(p => p.Agency).Include(p => p.Author).Where(p=>p.Id!=id).Take(2).ToListAsync(),
                Post = post
            };

            return View(postVM);
        }
    }
}
