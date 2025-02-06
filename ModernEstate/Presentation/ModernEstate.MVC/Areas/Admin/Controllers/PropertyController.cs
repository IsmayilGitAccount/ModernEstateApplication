using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernEstate.MVC.Areas.Admin.ViewModels.Property;
using ModernEstate.Persistence.Data;

namespace ModernEstate.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PropertyController(AppDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var propertyVMs = await _context.Properties
                .Include(p => p.Agency)
                .Include(p => p.Agent)
                .Include(p => p.Category)
                .Include(p => p.PropertyPhotos.Where(pp => pp.IsPrimary == true))
                .Select(p => new GetAdminPropertyVM
                {
                    Id = p.Id,
                    AgencyName = p.Agency.AgencyName,
                    AgentName = p.Agent.FullName,
                    Location = p.Location,
                    Price = p.Price,
                    CategoryName = p.Category.CategoryName,
                    Photo = p.PropertyPhotos.FirstOrDefault(pp => pp.IsPrimary == true).Photo
                }).ToListAsync();

            return View(propertyVMs);
        }
    }
}
