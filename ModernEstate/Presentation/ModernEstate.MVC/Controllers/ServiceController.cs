using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Application.Utilities.Exceptions;
using ModernEstate.Application.ViewModels.Services;
using ModernEstate.Domain.Entities;
using ModernEstate.Persistence.Data;

namespace ModernEstate.MVC.Controllers
{
    public class ServiceController(AppDbContext _context) : Controller
    {
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null || id <= 0) throw new BadRequestException($"{id} is wrong!");

            Agency agency = await _context.Agencies.FirstOrDefaultAsync(s => s.Id == id);

            if (agency == null) throw new NotFoundException($"Not found!");

            GetServiceVM serviceVMs = new GetServiceVM()
            {
                Service = await _context.Services.Include(s => s.Agency).Where(a => a.AgencyId == id).ToListAsync(),
                Agency = agency,
            };

            return View(serviceVMs);
        }
    }
}
