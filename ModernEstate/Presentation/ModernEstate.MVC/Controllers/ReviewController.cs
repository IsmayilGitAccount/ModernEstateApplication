using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ModernEstate.Application.ViewModels.Reviews;
using ModernEstate.Domain.Entities;
using ModernEstate.Domain.Entities.Account;
using ModernEstate.Persistence.Data;

namespace ModernEstate.MVC.Controllers
{
    public class ReviewController(AppDbContext _context, UserManager<AppUser> _userManager) : Controller
    {
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateReviewVM reviewVM)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (user == null) return View("Login", "Account");

            Review review = new Review()
            {
                Name = reviewVM.Name,
                Email = reviewVM.Email,
                Message = reviewVM.Message,
            };

            await _context.Reviews.AddAsync(review);

            await _context.SaveChangesAsync();

            return View(review);
        }
    }
}
