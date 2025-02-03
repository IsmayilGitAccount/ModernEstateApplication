using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernEstateDemo.Data;
using ModernEstateDemo.ViewModels;

namespace ModernEstateDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            PropertyVM propertyVM = new PropertyVM()
            {
                Property = await _context.Properties
               .Include(p => p.Agency)
               .Include(p => p.Agent)
               .Include(p => p.PropertyFeatures)
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

                Category = await _context.Categories.Include(c=>c.Properties).ToListAsync()
            };

            return View(propertyVM);
        }
    }
}
