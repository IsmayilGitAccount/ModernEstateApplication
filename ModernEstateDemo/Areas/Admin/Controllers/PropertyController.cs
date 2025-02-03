using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernEstateDemo.Areas.ViewModels.Properties;
using ModernEstateDemo.Data;
using ModernEstateDemo.Models;
using ModernEstateDemo.Utilities.Extensions;

namespace ModernEstateDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PropertyController(AppDbContext _context, IWebHostEnvironment _env) : Controller
    {
        string Root = Path.Combine("assets", "images");
        public async Task<IActionResult> Index()
        {
            var properties = await _context.Properties
                .Include(p=>p.Agency)
                .Include(p=>p.Agent)
                .Include(p=>p.Category)
                .Select(p=>new GetAdminPropertyVM
                {
                    Id = p.Id,
                    AgencyName = p.Agency.AgencyName,
                    AgentName = p.Agent.FullName,
                    Location = p.Location,
                    Price = p.Price,
                    CategoryName = p.Category.CategoryName,
                    Photo = p.PropertyPhotos.FirstOrDefault(pp=>p.IsDeleted==false).Photo,
                }).ToListAsync();
            return View(properties);
        }

        public async Task<IActionResult> Create()
        {
            CreateAdminPropertyVM propertyVM = new CreateAdminPropertyVM()
            {
                Categories = await _context.Categories.ToListAsync(),
                Agencies = await _context.Agencies.ToListAsync(),
                Agents = await _context.Agents.ToListAsync(),
                Views = await _context.Views.ToListAsync(),
                Exteriors = await _context.Exteriors.ToListAsync(),
                Parkings = await _context.Parkings.ToListAsync(),
                Features = await _context.Features.ToListAsync(),
                Roofs = await _context.Roofs.ToListAsync()
            };
            return View(propertyVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAdminPropertyVM propertyVM)
        {
            propertyVM.Features = await _context.Features.ToListAsync() ?? new List<Feature>();
            propertyVM.Categories = await _context.Categories.ToListAsync() ?? new List<Category>();
            propertyVM.Exteriors = await _context.Exteriors.ToListAsync() ?? new List<Exterior>();
            propertyVM.Agents = await _context.Agents.ToListAsync() ?? new List<Agent>();
            propertyVM.Agencies = await _context.Agencies.ToListAsync() ?? new List<Agency>();
            propertyVM.Parkings = await _context.Parkings.ToListAsync() ?? new List<Parking>();
            propertyVM.Views = await _context.Views.ToListAsync() ?? new List<View>();
            propertyVM.Roofs = await _context.Roofs.ToListAsync() ?? new List<Roof>();

            if (!ModelState.IsValid)
            {
                return View(propertyVM);
            }

            if (propertyVM.MainPhoto != null)
            {
                if (!propertyVM.MainPhoto.ValidateType("image/"))
                {
                    ModelState.AddModelError(nameof(propertyVM.MainPhoto), "File type is incorrect");
                    return View(propertyVM);
                }
                if (!propertyVM.MainPhoto.ValidateSize(Utilities.Enums.FileSize.MB, 2))
                {
                    ModelState.AddModelError(nameof(propertyVM.MainPhoto), "File size is incorrect");
                    return View(propertyVM);
                }

                string photoPath = await propertyVM.MainPhoto.CreatFileAsync(_env.WebRootPath, Root);
                PropertiesPhoto main = new()
                {
                    Photo = photoPath,
                    IsDeleted = false,
                    CreatedAt = DateTime.Now
                };

                Property property = new()
                {
                    Location = propertyVM.Location,
                    SchoolDistrict = propertyVM.SchoolDistrict,
                    LotSize = propertyVM.LotSize ?? 0,
                    Area = propertyVM.Area ?? 0,
                    CreatedAt = DateTime.Now,
                    CategoryId = propertyVM.CategoryId,
                    ViewId = propertyVM.ViewId,
                    RoofId = propertyVM.RoofId,
                    AgentId = propertyVM.AgentId,
                    AgencyId = propertyVM.AgencyId,
                    ExteriorId = propertyVM.ExteriorId,
                    ParkingId = propertyVM.ParkingId,
                    PropertyPhotos = new List<PropertiesPhoto> { main },
                    Price = propertyVM.Price ?? 0,
                    IsDeleted = false,
                    BathroomCount = propertyVM.BathroomCount ?? 0,
                    BedroomCount = propertyVM.BedroomCount ?? 0,
                    RoomCount = propertyVM.RoomCount ?? 0,
                    GarageCount = propertyVM.GarageCount ?? 0,
                    Description = propertyVM.Description,
                    BuiltYear = propertyVM.BuiltYear,
                    PropertyStatus = propertyVM.PropertyStatus,
                    PropertyType = propertyVM.PropertyType,
                };

                if (propertyVM.Features != null && propertyVM.FeatureIds != null)
                {
                    bool featureResult = propertyVM.FeatureIds.Any(fId => !propertyVM.Features.Exists(t => t.Id == fId));

                    if (featureResult)
                    {
                        ModelState.AddModelError(nameof(CreateAdminPropertyVM.FeatureIds), "Features are wrong");
                        return View(propertyVM);
                    }

                    property.PropertyFeatures = propertyVM.FeatureIds.Select(fId => new PropertyFeature { FeatureId = fId }).ToList();
                }

                if (propertyVM.AdditionalPhoto != null)
                {
                    foreach (IFormFile file in propertyVM.AdditionalPhoto)
                    {
                        if (!file.ValidateType("image/"))
                        {
                            ModelState.AddModelError(nameof(propertyVM.MainPhoto.FileName), "File type is incorrect");
                            return View(propertyVM);
                        }
                        if (!file.ValidateSize(Utilities.Enums.FileSize.MB, 2))
                        {
                            ModelState.AddModelError(nameof(propertyVM.MainPhoto.FileName), "File size is incorrect");
                            return View(propertyVM);
                        }

                        property.PropertyPhotos.Add(new PropertiesPhoto
                        {
                            Photo = await file.CreatFileAsync(_env.WebRootPath, Root),
                            CreatedAt = DateTime.Now,
                            IsDeleted = false
                        });
                    }
                }

                await _context.Properties.AddAsync(property);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError(nameof(propertyVM.MainPhoto), "Main photo is required.");
                return View(propertyVM);
            }
        }

        //public async Task<IActionResult> Create(CreateAdminPropertyVM propertyVM)
        //{
        //    propertyVM.Features = await _context.Features.ToListAsync();
        //    propertyVM.Categories = await _context.Categories.ToListAsync();
        //    propertyVM.Exteriors = await _context.Exteriors.ToListAsync();
        //    propertyVM.Agents = await _context.Agents.ToListAsync();
        //    propertyVM.Agencies = await _context.Agencies.ToListAsync();
        //    propertyVM.Parkings = await _context.Parkings.ToListAsync();
        //    propertyVM.Views = await _context.Views.ToListAsync();
        //    propertyVM.Roofs = await _context.Roofs.ToListAsync();

        //    if (!ModelState.IsValid)
        //    {
        //        return View(propertyVM);
        //    }

        //    if (propertyVM.MainPhoto != null)
        //    {
        //        if (!propertyVM.MainPhoto.ValidateType("image/"))
        //        {
        //            ModelState.AddModelError(nameof(propertyVM.MainPhoto), "File type is incorrect");
        //            return View(propertyVM);
        //        }
        //        if (!propertyVM.MainPhoto.ValidateSize(Utilities.Enums.FileSize.MB, 2))
        //        {
        //            ModelState.AddModelError(nameof(propertyVM.MainPhoto), "File size is incorrect");
        //            return View(propertyVM);
        //        }
        //    }

        //    bool category = propertyVM.Categories.Any(p => p.Id == propertyVM.CategoryId);

        //    if (!category)
        //    {
        //        ModelState.AddModelError(nameof(CreateAdminPropertyVM), "Category does not exist");
        //        return View(propertyVM);
        //    }

        //    bool view = propertyVM.Views.Any(p => p.Id == propertyVM.ViewId);

        //    if (!view)
        //    {
        //        ModelState.AddModelError(nameof(CreateAdminPropertyVM), "View does not exist");
        //        return View(propertyVM);
        //    }

        //    bool exterior = propertyVM.Exteriors.Any(p => p.Id == propertyVM.ExteriorId);

        //    if (!exterior)
        //    {
        //        ModelState.AddModelError(nameof(CreateAdminPropertyVM), "Exterior does not exist");
        //        return View(propertyVM);
        //    }

        //    bool roof = propertyVM.Roofs.Any(p => p.Id == propertyVM.RoofId);

        //    if (!roof)
        //    {
        //        ModelState.AddModelError(nameof(CreateAdminPropertyVM), "Roof does not exist");
        //        return View(propertyVM);
        //    }

        //    bool parking = propertyVM.Parkings.Any(p => p.Id == propertyVM.ParkingId);

        //    if (!parking)
        //    {
        //        ModelState.AddModelError(nameof(CreateAdminPropertyVM), "Parking does not exist");
        //        return View(propertyVM);
        //    }

        //    bool agent = propertyVM.Agents.Any(p => p.Id == propertyVM.AgentId);

        //    if (!agent)
        //    {
        //        ModelState.AddModelError(nameof(CreateAdminPropertyVM), "Agent does not exist");
        //        return View(propertyVM);
        //    }

        //    bool agency = propertyVM.Agencies.Any(p => p.Id == propertyVM.AgencyId);

        //    if (!agency)
        //    {
        //        ModelState.AddModelError(nameof(CreateAdminPropertyVM), "Agency does not exist");
        //        return View(propertyVM);
        //    }

        //    if (propertyVM.Features is not null)
        //    {
        //        bool featureResult = propertyVM.FeatureIds != null &&
        //             propertyVM.Features != null &&
        //             propertyVM.FeatureIds.Any(fId => !propertyVM.Features.Exists(t => t.Id == fId));


        //        if (featureResult)
        //        {
        //            ModelState.AddModelError(nameof(CreateAdminPropertyVM.FeatureIds), "Features are wrong");
        //            return View(propertyVM);
        //        }
        //    }


        //        PropertiesPhoto main = new()
        //        {
        //            Photo = await propertyVM.MainPhoto.CreatFileAsync(_env.WebRootPath, Root),
        //            IsDeleted = false,
        //            CreatedAt = DateTime.Now
        //        };


        //    Property property = new()
        //    {
        //        Location = propertyVM.Location,
        //        SchoolDistrict = propertyVM.SchoolDistrict,
        //        LotSize = propertyVM.LotSize.Value,
        //        Area = propertyVM.Area.Value,
        //        CreatedAt = DateTime.Now,
        //        CategoryId = propertyVM.CategoryId,
        //        ViewId = propertyVM.ViewId,
        //        RoofId = propertyVM.RoofId,
        //        AgentId = propertyVM.AgentId,
        //        AgencyId = propertyVM.AgencyId,
        //        ExteriorId = propertyVM.ExteriorId,
        //        ParkingId = propertyVM.ParkingId,
        //        PropertyPhotos = new List<PropertiesPhoto> { main },
        //        Price = propertyVM.Price.Value,
        //        IsDeleted = false,
        //        BathroomCount = propertyVM.BathroomCount.Value,
        //        BedroomCount = propertyVM.BedroomCount.Value,
        //        RoomCount = propertyVM.RoomCount.Value,
        //        GarageCount = propertyVM.GarageCount.Value,
        //        Description = propertyVM.Description,
        //        BuiltYear = propertyVM.BuiltYear
        //    };

        //    if (propertyVM.FeatureIds is not null)
        //    {
        //        property.PropertyFeatures = propertyVM.FeatureIds.Select(fId => new PropertyFeature { FeatureId = fId }).ToList();
        //    }

        //    string text = string.Empty;
        //    if (propertyVM.AdditionalPhoto is not null)
        //    {

        //        foreach (IFormFile file in propertyVM.AdditionalPhoto)
        //        {
        //            if (!file.ValidateType("image/"))
        //            {
        //                ModelState.AddModelError(nameof(propertyVM.MainPhoto.FileName), "File type is incorrect");
        //                return View(propertyVM);
        //            }
        //            if (!file.ValidateSize(Utilities.Enums.FileSize.MB, 2))
        //            {
        //                ModelState.AddModelError(nameof(propertyVM.MainPhoto.FileName), "File size is incorrect");
        //                return View(propertyVM);
        //            }

        //            property.PropertyPhotos.Add(new PropertiesPhoto
        //            {
        //                Photo = await file.CreatFileAsync(_env.WebRootPath, Root),
        //                CreatedAt = DateTime.Now,
        //                IsDeleted = false
        //            });
        //        }
        //    }

        //    await _context.Properties.AddAsync(property);

        //    await _context.SaveChangesAsync();

        //    return RedirectToAction(nameof(Index));
        //}

        public async Task<IActionResult> Delete(int? id)
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

            if(property.Photo != null)
            {
                property.Photo.DeleteFile(_env.WebRootPath, Root);
            }

            _context.Properties.Remove(property);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
