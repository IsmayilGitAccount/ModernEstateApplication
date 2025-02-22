using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Application.ViewModels.Contacts;
using ModernEstate.Domain.Entities;
using ModernEstate.Domain.Enums;
using ModernEstate.Persistence.Data;
using ModernEstate.Domain.Entities.Account;
using ModernEstate.Application.Utilities.Exceptions;

namespace ModernEstate.MVC.Controllers
{
    public class UserMessageController(AppDbContext _context, UserManager<AppUser> _userManager) : Controller
    {
        public async Task<IActionResult> Index(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new BadRequestException("Invalid user Id!");
            }

            var messages = await _context.Contacts
                 .Where(c => c.Role == UserRole.Admin.ToString() && c.UserId == userId) 
                 .OrderByDescending(x => x.CreatedAt)
                 .Select(c => new GetUserMessageVM
                 {
                     MessageId = c.Id,
                     Name = c.Name,
                     UserId = userId,
                     Email = c.Email,
                     Message = c.Message
                 }).ToListAsync();

            return View(messages);
        }


        public async Task<IActionResult> SendMessage(string userId)
        {
            var contact = await _context.Contacts
                                        .FirstOrDefaultAsync(c => c.UserId == userId);
            if (contact == null)
            {
                return NotFound();
            }

            SendMessageVM vmMessage = new SendMessageVM()
            {
                Messages = await _context.Contacts
                                        .Where(c => c.UserId == userId && c.IsDeleted == false)
                                        .ToListAsync(),
            };

            return View(vmMessage);
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(string userId, SendMessageVM vm)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            Contact contact = new Contact()
            {
                UserId = userId,
                CreatedAt = DateTime.Now,
                Email = user.Email,
                IsDeleted = false,
                Message = vm.Message,
                Name = user.Name,
                PhoneNumber = vm.PhoneNumber,
                Role = "User"
            };

            await _context.Contacts.AddAsync(contact);
            await _context.SaveChangesAsync();

            return RedirectToAction("SendMessage", new { userId = userId });
        }
    }
}
