using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Application.ViewModels.Contacts;
using ModernEstate.Domain.Entities;
using ModernEstate.Domain.Entities.Account;
using ModernEstate.Domain.Enums;
using ModernEstate.Persistence.Data;

namespace ModernEstate.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MessageController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public MessageController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var contact = await _context.Contacts.ToListAsync();

            Contact text = contact.FirstOrDefault();

            var messages = await _context.Contacts
                 .Where(c => c.Role == UserRole.User.ToString())
        .Select(c => new GetContactVM
        {
            MessageId = c.Id,
            Email = c.Email,
            Name = c.Name,
            Message = c.Message,
            UserId = c.UserId
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
                UserId = userId
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
                Name = user.UserName,
                PhoneNumber = vm.PhoneNumber,
                Role = "Agent"
            };

            await _context.Contacts.AddAsync(contact);
            await _context.SaveChangesAsync();

            return RedirectToAction("SendMessage", new { userId = userId }); 
        }
    }
}
