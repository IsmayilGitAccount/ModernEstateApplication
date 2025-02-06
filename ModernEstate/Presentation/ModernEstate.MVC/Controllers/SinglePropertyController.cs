using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Application.ViewModels.Properties;
using ModernEstate.Domain.Entities;
using ModernEstate.Persistence.Data;

namespace ModernEstate.MVC.Controllers
{
    public class SinglePropertyController(AppDbContext _context) : Controller
    {
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null || id <= 0) return BadRequest();
            Property property = await _context.Properties
                .Include(p => p.Agency)
                .Include(p => p.Agent)
                .Include(p => p.Category)
                .Include(p => p.Exterior)
                .Include(p => p.PropertyFeatures)
                .ThenInclude(pf => pf.Feature)
                .Include(p => p.PropertyPhotos)
                .OrderByDescending(p => p.Id)
                .Include(p => p.View)
                .Include(p => p.Roof)
                .Include(p => p.Parking)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (property == null) return NotFound();

            SinglePropertyVM productVM = new SinglePropertyVM()
            {
                Property = property,
                PropertyPhotos = await _context.PropertiesPhotos
                .ToListAsync(),
                RecentlyProperties = await _context.Properties
                .Where(p => p.CategoryId == property.Id || p.Id != id)
                .Include(p => p.PropertyPhotos)
                .Take(3)
                .ToListAsync()
            };

            return View(productVM);
        }
    }
}
