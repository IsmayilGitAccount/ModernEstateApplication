using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Application.ViewModels.Services;
using ModernEstate.Domain.Entities;
using ModernEstate.Persistence.Data;

namespace ModernEstate.MVC.Controllers
{
    public class ServiceController(AppDbContext _context) : Controller
    {
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null || id <= 0) return BadRequest();

            Agency agency = await _context.Agencies.FirstOrDefaultAsync(s => s.Id == id);

            if (agency == null) return NotFound();

            GetServiceVM serviceVMs = new GetServiceVM()
            {
                Service = await _context.Services.Include(s => s.Agency).Where(a=>a.Id == id).ToListAsync(),
                Agency = agency,
            };

            return View(serviceVMs);
        }
    }
}
