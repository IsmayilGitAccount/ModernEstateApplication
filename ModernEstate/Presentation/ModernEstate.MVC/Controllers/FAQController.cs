using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Application.Utilities.Exceptions;
using ModernEstate.Application.ViewModels.FAQs;
using ModernEstate.Domain.Entities;
using ModernEstate.Persistence.Data;

namespace ModernEstate.MVC.Controllers
{
    public class FAQController(AppDbContext _context) : Controller
    {
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null || id <= 0) throw new BadRequestException($"{id} is wrong!");

            Agency agency = await _context.Agencies.FirstOrDefaultAsync(f => f.Id == id);

            if (agency == null) throw new NotFoundException("FAQ not found!");

            GetFAQVM faqVM = new GetFAQVM()
            {
                FAQs = await _context.FAQs.Include(f => f.Agency).Where(f => f.AgencyId == id).ToListAsync()
            };
            return View(faqVM);
        }
    }
}
