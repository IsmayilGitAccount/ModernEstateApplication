using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Application.Utilities.Exceptions;
using ModernEstate.Application.ViewModels.Properties;
using ModernEstate.Persistence.Data;

namespace ModernEstate.MVC.Controllers
{
    public class HomeController(AppDbContext _context) : Controller
    {
        public async Task<IActionResult> Index(
    string keyword = null,
    string location = null,
    string status = null,
    string type = null,
    int? minArea = null,
    int? maxArea = null,
    int? minPrice = null,
    int? maxPrice = null,
    int? minBeds = null,
    int? minBaths = null,
    int page = 1)
        {
            if (page < 1) throw new NotFoundException($"{page}th page is not found!");

            var query = _context.Properties
               .Include(p => p.Agency)
               .Include(p => p.Agent)
               .Include(p => p.PropertyFeatures)
               .Include(p => p.PropertyPhotos.Where(p => p.IsPrimary == true))
               .AsQueryable();

            // Apply Filters
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(p => p.Description.Contains(keyword) ||
                                         p.Agency.AgencyName.Contains(keyword) ||
                                         p.Agent.FullName.Contains(keyword));
            }

            if (!string.IsNullOrEmpty(location))
            {
                query = query.Where(p => p.Location == location);
            }

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(p => p.Status.StatusName == status);
            }

            if (!string.IsNullOrEmpty(type))
            {
                query = query.Where(p => p.Type.TypeName == type);
            }

            if (minArea.HasValue)
            {
                query = query.Where(p => p.Area >= minArea.Value);
            }

            if (maxArea.HasValue)
            {
                query = query.Where(p => p.Area <= maxArea.Value);
            }

            if (minPrice.HasValue)
            {
                query = query.Where(p => p.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= maxPrice.Value);
            }

            if (minBeds.HasValue)
            {
                query = query.Where(p => p.BedroomCount >= minBeds.Value);
            }

            if (minBaths.HasValue)
            {
                query = query.Where(p => p.BathroomCount >= minBaths.Value);
            }

            int count = await query.CountAsync();
            double total = Math.Ceiling((double)count / 3);

            if (total < page) throw new BadRequestException("Not found!");

            var propertyVMs = new PropertyVM()
            {
                Property = await query
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
                    .Where(c => c.Properties.Count() != 0)
                    .Take(12)
                    .ToListAsync(),
                Status = await _context.Status.Include(s => s.Properties).ToListAsync(),
                Types = await _context.Types.Include(t => t.Properties).ToListAsync(),
                Slides = await _context.Slides.Take(3).OrderBy(s => s.Order).ToListAsync(),
                Agents = await _context.Agents.Where(a => a.Properties.Count() != 0).Take(9).Include(a => a.Agency).ToListAsync(),
                TotalPage = total,
                CurrentPage = page,
            };

            return View(propertyVMs);
        }

        public IActionResult Error(string errorMessage)
        {
            return View(model: errorMessage);
        }
    }
}


//    public async Task<IActionResult> Index(
//        string? sort,
//        string? location,
//        string? status,
//        string? type,
//        int? minBeds,
//        int? minBaths,
//        int? minArea,
//        int? maxArea,
//        int? minPrice,
//        int? maxPrice)
//    {
//        IQueryable<Property> query = _context.Properties
//            .Include(p => p.Status)
//            .Include(p => p.Type)
//            .Include(p => p.Category)
//            .Include(p => p.Agent)
//            .Include(p => p.Agency)
//            .Include(p => p.Exterior)
//            .Include(p => p.PropertyPhotos);

//        if (!string.IsNullOrEmpty(location))
//        {
//            query = query.Where(p => p.Location != null && p.Location.ToLower() == location.ToLower());
//        }

//        if (!string.IsNullOrEmpty(status))
//        {
//            query = query.Where(p => p.Status.StatusName == status);
//        }

//        if (!string.IsNullOrEmpty(type))
//        {
//            query = query.Where(p => p.Type.TypeName == type);
//        }

//        if (minBeds.HasValue)
//        {
//            query = query.Where(p => p.BedroomCount >= minBeds);
//        }

//        if (minBaths.HasValue)
//        {
//            query = query.Where(p => p.BathroomCount >= minBaths);
//        }

//        if (minArea.HasValue)
//        {
//            query = query.Where(p => p.Area >= minArea);
//        }

//        if (maxArea.HasValue)
//        {
//            query = query.Where(p => p.Area <= maxArea);
//        }

//        if (minPrice.HasValue)
//        {
//            query = query.Where(p => p.Price >= minPrice);
//        }

//        if (maxPrice.HasValue)
//        {
//            query = query.Where(p => p.Price <= maxPrice);
//        }

//        switch (sort)
//        {
//            case "price-asc":
//                query = query.OrderBy(p => p.Price);
//                break;
//            case "price-desc":
//                query = query.OrderByDescending(p => p.Price);
//                break;
//            case "recent":
//                query = query.OrderByDescending(p => p.CreatedAt);
//                break;
//            default:
//                query = query.OrderByDescending(p => p.CreatedAt);
//                break;
//        }

//        var model = new SearchPropertyVM
//        {
//            PropertyData = new PropertyVM
//            {
//                Property = await query.Select(p => new GetPropertyVM
//                {
//                    Id = p.Id,
//                    AgencyName = p.Agency.AgencyName,
//                    AgentName = p.Agent.FullName,
//                    Area = p.Area,
//                    BathroomCount = p.BathroomCount,
//                    BedroomCount = p.BedroomCount,
//                    BuiltYear = p.BuiltYear,
//                    CategoryName = p.Category.CategoryName,
//                    Description = p.Description,
//                    ExteriorType = p.Exterior.ExteriorType,
//                    GarageCount = p.GarageCount,
//                    SchoolDistrict = p.SchoolDistrict,
//                    LotSize = p.LotSize,
//                    Location = p.Location,
//                    ParkingType = p.Parking.ParkingType,
//                    Price = p.Price,
//                    RoofType = p.Roof.RoofType,
//                    RoomCount = p.RoomCount,
//                    ViewType = p.View.ViewType,
//                    Photo = p.PropertyPhotos.FirstOrDefault(p => p.IsDeleted == false).Photo,
//                    CreatedAt = p.CreatedAt,
//                })
//                .Take(6)
//                .ToListAsync() ?? new List<GetPropertyVM>(),

//                Category = await _context.Categories.Take(4).OrderByDescending(c=>c.Id).Include(c => c.Properties).ToListAsync(),
//                Status = await _context.Status.Include(c => c.Properties).ToListAsync(),
//                Types = await _context.Types.Include(c => c.Properties).ToListAsync(),
//                Slides = await _context.Slides.OrderByDescending(s => s.Order).Take(3).ToListAsync()
//            },

//            Properties = await query.ToListAsync()
//        };

//        return View(model);
//    }
//}





