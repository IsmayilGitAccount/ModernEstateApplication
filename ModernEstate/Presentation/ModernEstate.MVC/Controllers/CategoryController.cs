using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Application.ViewModels.Paginations;
using ModernEstate.Application.ViewModels.Properties;
using ModernEstate.Domain.Entities;
using ModernEstate.Persistence.Data;

namespace ModernEstate.MVC.Controllers
{
    public class CategoryController(AppDbContext _context) : Controller
    {
        public async Task<IActionResult> Details(int? id, int page = 1)
        {
            if (page < 1) return BadRequest();

            int count = await _context.Categories.CountAsync();

            double total = Math.Ceiling((double)count / 3);

            if (total < page) return BadRequest();

            if (id is null || id <= 0) return BadRequest();

            Category category = await _context.Categories.Include(a => a.Properties).FirstOrDefaultAsync(a => a.Id == id);

            if (category == null) return BadRequest();

            //var propertyVMs = await _context.Properties
            //    .Skip((page-1)*3)
            //    .Take(3)
            //    .Include(p=>p.PropertyPhotos)
            //    .Where(p=>p.CategoryId==id)
            //    .Select(p=>new GetPropertyVM
            //    {
            //        Id = p.Id,
            //        AgencyName = p.Agency.AgencyName,
            //        AgentName = p.Agent.FullName,
            //        Area = p.Area,
            //        BathroomCount = p.BathroomCount,
            //        BedroomCount = p.BedroomCount,
            //        BuiltYear = p.BuiltYear,
            //        CategoryName = p.Category.CategoryName,
            //        Description = p.Description,
            //        ExteriorType = p.Exterior.ExteriorType,
            //        GarageCount = p.GarageCount,
            //        SchoolDistrict = p.SchoolDistrict,
            //        LotSize = p.LotSize,
            //        Location = p.Location,
            //        ParkingType = p.Parking.ParkingType,
            //        Price = p.Price,
            //        RoofType = p.Roof.RoofType,
            //        RoomCount = p.RoomCount,
            //        ViewType = p.View.ViewType,
            //        Photo = p.PropertyPhotos.FirstOrDefault(p => p.IsDeleted == false).Photo,
            //    }).ToListAsync();

            var propertyVMs = new PropertyVM()
            {
                Property = await _context.Properties
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
               .Skip((page - 1) * 3)
               .Take(3)
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
                TotalPage = total,
                CurrentPage = page
            };


            return View(propertyVMs);
        }
    }
}
