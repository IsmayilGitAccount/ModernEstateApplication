using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Application.Utilities.Exceptions;
using ModernEstate.Application.ViewModels.Agencies;
using ModernEstate.Domain.Entities;
using ModernEstate.Persistence.Data;

namespace ModernEstate.MVC.Controllers
{
    public class AgencyController(AppDbContext _context) : Controller
    {
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null || id <= 0) throw new BadRequestException($"{id} is wrong!");

            Agency agency = await _context.Agencies.Include(a => a.Agent).FirstOrDefaultAsync(a => a.Id == id);

            if (agency == null) throw new NotFoundException("Agency not found!");

            AgencyVM agencyVM = new AgencyVM()
            {
                Agency = agency,
                Agent = await _context.Agents.Where(a => a.AgencyId == agency.Id).ToListAsync()
            };

            return View(agencyVM);
        }
    }
}
