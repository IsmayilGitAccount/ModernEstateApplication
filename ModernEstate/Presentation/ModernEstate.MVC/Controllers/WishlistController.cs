using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Application.Utilities.Exceptions;
using ModernEstate.Domain.Entities;
using ModernEstate.Persistence.Data;

namespace ModernEstate.MVC.Controllers
{
    public class WishlistController : Controller
    {
        private readonly AppDbContext _context;

        public WishlistController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string userId)
        {
            var wishlistItems = await _context.Wishlist
                .Include(w => w.Property.PropertyPhotos)
                .Where(w => w.UserId == userId || w.UserId == null)
                .Include(w => w.Property)
                .ToListAsync();

            return View(wishlistItems);
        }

        [HttpPost]
        public async Task<IActionResult> AddToWishlist(int propertyId, string userId = null)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            var property = await _context.Properties.FindAsync(propertyId);
            if (property == null)
            {
                throw new NotFoundException($"Property was not found!");
            }

            var wishlistItem = new Wishlist
            {
                PropertyId = property.Id,
                UserId = userId ?? User.Identity.Name,
                Property = property
            };

            _context.Wishlist.Add(wishlistItem);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Wishlist", new { userId = userId });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromWishlist(int propertyId, string userId = null)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            var wishlistItem = await _context.Wishlist
                .Where(w => w.PropertyId == propertyId && (w.UserId == userId || w.UserId == null))
                .FirstOrDefaultAsync();

            if (wishlistItem == null)
            {
                throw new NotFoundException("Wishlist item not found!");
            }

            _context.Wishlist.Remove(wishlistItem);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Wishlist", new { userId = userId });
        }


        [HttpPost]
        public async Task<IActionResult> ClearWishlist(string userId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            var wishlistItems = _context.Wishlist
                .Where(w => w.UserId == userId);

            _context.Wishlist.RemoveRange(wishlistItems);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Wishlist", new { userId = userId });
        }

    }

}
