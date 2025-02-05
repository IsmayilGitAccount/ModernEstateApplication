using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernEstateProject.Areas.Admin.ViewModels.Categories;
using ModernEstateProject.Data;
using ModernEstateProject.Models;
using ModernEstateProject.ViewModels.Paginations;
using ModernEstateProject.ViewModels.Properties;

namespace ModernEstateProject.Controllers
{
    public class CategoryController(AppDbContext _context) : Controller
    {
        public async Task<IActionResult> Details(int? id, int page = 1)
        {
            if (page < 1) return BadRequest();

            int count = await _context.Properties.CountAsync();

            double total = Math.Ceiling((double)count / 2);

            if (total < page) return BadRequest();

            if (id is null || id <= 0) return BadRequest();

            Category categrory = await _context.Categories.Include(a => a.Properties).FirstOrDefaultAsync(a => a.Id == id);

            if (categrory == null) return BadRequest();

            var propertyVMs = new PropertyVM()
            {
                Property = await _context.Properties
                .Skip((page-1)*2)
                .Take(2)
            .Include(p => p.Agency)
            .Include(p => p.Agent)
            .Include(p => p.PropertyFeatures)
            .Include(p => p.PropertyPhotos.Where(p => p.IsPrimary == true))
            .Where(p => p.CategoryId == categrory.Id)
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
                Category = await _context.Categories.Include(c => c.Properties).ToListAsync(),
                Status = await _context.Status.Include(s => s.Properties).ToListAsync(),
                Types = await _context.Types.Include(s => s.Properties).ToListAsync()
            };

            PaginationVM paginationVM = new()
            {
                TotalPage = total,
                CurrentPage = page,
                Items = propertyVMs
            };
            return View(paginationVM);
        }
    }
}
