using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernEstateProject.Data;
using ModernEstateProject.ViewModels.Properties;

namespace ModernEstateProject.Controllers
{
    public class HomeController(AppDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            PropertyVM propertyVMs = new PropertyVM()
            {
                Property = await _context.Properties
               .Include(p => p.Agency)
               .Include(p => p.Agent)
               .Include(p => p.PropertyFeatures)
               .Include(p=>p.PropertyPhotos.Where(p=>p.IsPrimary==true))
               .Select(p => new GetPropertyVM
               {
                   Id = p.Id,
                   AgencyName = p.Agency.AgencyName,
                   AgentName = p.Agent.FullName,
                   Area = p.Area,
                   BathroomCount = p.BathroomCount,
                   BedroomCount = p.BedroomCount,
                   BuiltYear = p.BuiltYear,
                   CategoryName = p.Category.CategoryName,
                   Description = p.Description,
                   ExteriorType = p.Exterior.ExteriorType,
                   GarageCount = p.GarageCount,
                   SchoolDistrict = p.SchoolDistrict,
                   LotSize = p.LotSize,
                   Location = p.Location,
                   ParkingType = p.Parking.ParkingType,
                   Price = p.Price,
                   RoofType = p.Roof.RoofType,
                   RoomCount = p.RoomCount,
                   ViewType = p.View.ViewType,
                   Photo = p.PropertyPhotos.FirstOrDefault(p => p.IsDeleted == false).Photo,
               }).ToListAsync(),
                Category = await _context.Categories
                .Include(c => c.Properties)
                .OrderByDescending(c=>c.CreatedAt)
                .Take(4)
                .ToListAsync(),
                Status = await _context.Status.Include(s=>s.Properties).ToListAsync(),
                Types = await _context.Types.Include(s => s.Properties).ToListAsync()
            };

            return View(propertyVMs);
        }
    }
}
