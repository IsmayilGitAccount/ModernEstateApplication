using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Application.ViewModels.Contacts;
using ModernEstate.Domain.Entities;
using ModernEstate.Domain.Entities.Account;
using ModernEstate.Persistence.Data;

namespace ModernEstate.MVC.Controllers
{
    public class ContactController(AppDbContext _context, UserManager<AppUser> _userManager) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(CreateContactVM contactVM)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if(!ModelState.IsValid)
            {
                return View("Index");
            }

            Contact contact = new Contact()
            {
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                CreatedAt = DateTime.Now,
                Email = contactVM.Email,
                IsDeleted = false,
                Message = contactVM.Message,
                PhoneNumber = contactVM.Phone,
                Name = contactVM.Name,
                Role = "User"
            };

            await _context.Contacts.AddAsync(contact);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Contact");
        }
    }
}
