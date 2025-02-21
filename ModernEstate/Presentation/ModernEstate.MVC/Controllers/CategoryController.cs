using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Application.Utilities.Exceptions;
using ModernEstate.Application.ViewModels.Properties;
using ModernEstate.Domain.Entities;
using ModernEstate.Persistence.Data;

namespace ModernEstate.MVC.Controllers
{
    public class CategoryController(AppDbContext _context) : Controller
    {
        public async Task<IActionResult> Details(int? id, int page = 1)
        {
            if (page < 1) throw new NotFoundException($"{page}th is not found!");

            int count = await _context.Properties.Where(p => p.CategoryId == id).CountAsync();

            double total = Math.Ceiling((double)count / 2);

            if (total < page) throw new NotFoundException($"Not found!");

            if (id is null || id <= 0) throw new BadRequestException($"{id} is wrong!");

            Category category = await _context.Categories.Include(a => a.Properties).FirstOrDefaultAsync(a => a.Id == id);

            if (category == null) throw new NotFoundException("Category not found!");

            var propertyVMs = new PropertyVM()
            {
                Property = await _context.Properties
                .Where(p => p.CategoryId == id)
               .Include(p => p.Agency)
               .Include(p => p.Agent)
               .Include(p => p.PropertyFeatures)
               .Include(p => p.PropertyPhotos.Take(2).OrderByDescending(p => p.CreatedAt).Where(p => p.IsPrimary == true))
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
                   CreatedAt = p.CreatedAt,
               })
               .OrderByDescending(p => p.Id)
               .Skip((page - 1) * 2)
               .Take(2)
               .ToListAsync(),
                Category = await _context.Categories
                .Include(c => c.Properties)
                .OrderByDescending(c => c.CreatedAt)
                .Take(12)
                .ToListAsync(),
                Status = await _context.Status.Include(s => s.Properties).ToListAsync(),
                Types = await _context.Types.Include(s => s.Properties).ToListAsync(),
                Slides = await _context.Slides.Take(3).OrderByDescending(s => s.Order).ToListAsync(),
                Agents = await _context.Agents.Take(9).Include(a => a.Agency).ToListAsync(),
                Categories = category,
                TotalPage = total,
                CurrentPage = page
            };

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return View(propertyVMs);
            }
            return View(propertyVMs);
        }
    }
}
