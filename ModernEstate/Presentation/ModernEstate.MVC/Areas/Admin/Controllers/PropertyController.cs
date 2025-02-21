using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Application.Utilities.Extensions;
using ModernEstate.Application.ViewModels.AdminPaginations;
using ModernEstate.Domain.Entities;
using ModernEstate.Domain.Enums;
using ModernEstate.MVC.Areas.Admin.ViewModels.Properties;
using ModernEstate.MVC.Areas.Admin.ViewModels.Property;
using ModernEstate.Persistence.Data;

namespace ModernEstate.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PropertyController(AppDbContext _context, IWebHostEnvironment _env) : Controller
    {
        public async Task<IActionResult> Index(int page = 1)
        {
            

            if (page < 1) return BadRequest();

            int count = await _context.Properties.CountAsync();

            double total = Math.Ceiling((double)count / 3);

            if (page > total) return BadRequest();

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
                }).Skip((page - 1) * 3).Take(3).ToListAsync();

            PaginationVM<GetAdminPropertyVM> paginationVM = new PaginationVM<GetAdminPropertyVM>()
            {
                TotalPage = total,
                CurrentPage = page,
                Items = propertyVMs
            };

            return View(paginationVM);
        }

        public async Task<IActionResult> Create()
        {
            
            CreateAdminPropertyVM propertyVM = new CreateAdminPropertyVM()
            {
                Categories = await _context.Categories.ToListAsync(),
                Exteriors = await _context.Exteriors.ToListAsync(),
                Agencies = await _context.Agencies.ToListAsync(),
                Agents = await _context.Agents.ToListAsync(),
                Parkings = await _context.Parkings.ToListAsync(),
                Features = await _context.Features.ToListAsync(),
                Views = await _context.Views.ToListAsync(),
                Roofs = await _context.Roofs.ToListAsync(),
                Status = await _context.Status.ToListAsync(),
                Types = await _context.Types.ToListAsync(),
            };

            return View(propertyVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAdminPropertyVM propertyVM)
        {
           
            propertyVM.Agencies = await _context.Agencies.ToListAsync();
            propertyVM.Agents = await _context.Agents.ToListAsync();
            propertyVM.Roofs = await _context.Roofs.ToListAsync();
            propertyVM.Views = await _context.Views.ToListAsync();
            propertyVM.Exteriors = await _context.Exteriors.ToListAsync();
            propertyVM.Parkings = await _context.Parkings.ToListAsync();
            propertyVM.Status = await _context.Status.ToListAsync();
            propertyVM.Types = await _context.Types.ToListAsync();
            propertyVM.Features = await _context.Features.ToListAsync();
            propertyVM.Categories = await _context.Categories.ToListAsync();

            if (!ModelState.IsValid)
            {
                return View(propertyVM);
            }

            bool agency = await _context.Agencies.AnyAsync(a => a.Id == propertyVM.AgencyId);

            if (!agency)
            {
                ModelState.AddModelError(nameof(CreateAdminPropertyVM.AgencyId), "Agency does not exist");
                return View(propertyVM);
            }

            bool agent = await _context.Agents.AnyAsync(a => a.Id == propertyVM.AgentId);

            if (!agent)
            {
                ModelState.AddModelError(nameof(CreateAdminPropertyVM.AgentId), "Agent does not exist");
                return View(propertyVM);
            }

            bool roof = await _context.Roofs.AnyAsync(a => a.Id == propertyVM.RoofId);

            if (!roof)
            {
                ModelState.AddModelError(nameof(CreateAdminPropertyVM.RoofId), "Roof does not exist");
                return View(propertyVM);
            }

            bool view = await _context.Views.AnyAsync(a => a.Id == propertyVM.ViewId);

            if (!view)
            {
                ModelState.AddModelError(nameof(CreateAdminPropertyVM.ViewId), "View does not exist");
                return View(propertyVM);
            }

            bool exterior = await _context.Exteriors.AnyAsync(a => a.Id == propertyVM.ExteriorId);

            if (!exterior)
            {
                ModelState.AddModelError(nameof(CreateAdminPropertyVM.ExteriorId), "Exterior does not exist");
                return View(propertyVM);
            }

            bool parking = await _context.Parkings.AnyAsync(a => a.Id == propertyVM.ParkingId);

            if (!parking)
            {
                ModelState.AddModelError(nameof(CreateAdminPropertyVM.ParkingId), "Parking does not exist");
                return View(propertyVM);
            }

            bool status = await _context.Status.AnyAsync(a => a.Id == propertyVM.StatusId);

            if (!status)
            {
                ModelState.AddModelError(nameof(CreateAdminPropertyVM.StatusId), "Status does not exist");
                return View(propertyVM);
            }

            bool type = await _context.Types.AnyAsync(a => a.Id == propertyVM.TypeId);

            if (!type)
            {
                ModelState.AddModelError(nameof(CreateAdminPropertyVM.TypeId), "Type does not exist");
                return View(propertyVM);
            }

            bool category = await _context.Categories.AnyAsync(a => a.Id == propertyVM.CategoryId);

            if (!category)
            {
                ModelState.AddModelError(nameof(CreateAdminPropertyVM.CategoryId), "Category does not exist");
                return View(propertyVM);
            }

            if (propertyVM.FeatureIds is null)
            {
                propertyVM.FeatureIds = new List<int>();
            }

            bool feature = propertyVM.FeatureIds.Any(fId => !propertyVM.Features.Exists(f => f.Id == fId));

            if (feature)
            {
                ModelState.AddModelError(nameof(CreateAdminPropertyVM.FeatureIds), "Features are wrong!");
                return View(propertyVM);
            }

            if (propertyVM.MainPhoto != null)
            {
                if (!propertyVM.MainPhoto.ValidateType("image/"))
                {
                    ModelState.AddModelError(nameof(propertyVM.MainPhoto), "File type is incorrect");
                    return View(propertyVM);
                }
                if (!propertyVM.MainPhoto.ValidateSize(FileSize.MB, 2))
                {
                    ModelState.AddModelError(nameof(propertyVM.MainPhoto), "File size is incorrect");
                    return View(propertyVM);
                }
            }

            PropertyPhoto main = new()
            {
                Photo = await propertyVM.MainPhoto.CreatFileAsync(_env.WebRootPath, "assets", "images"),
                IsPrimary = true,
                CreatedAt = DateTime.Now,
                IsDeleted = false,
            };

            Property property = new Property()
            {
                AgencyId = propertyVM.AgencyId,
                AgentId = propertyVM.AgentId,
                CategoryId = propertyVM.CategoryId,
                StatusId = propertyVM.StatusId,
                TypeId = propertyVM.TypeId,
                RoofId = propertyVM.RoofId,
                ViewId = propertyVM.ViewId,
                ExteriorId = propertyVM.ExteriorId,
                ParkingId = propertyVM.ParkingId,
                Location = propertyVM.Location,
                LotSize = propertyVM.LotSize.Value,
                SchoolDistrict = propertyVM.SchoolDistrict,
                Area = propertyVM.Area.Value,
                Price = propertyVM.Price.Value,
                BathroomCount = propertyVM.BathroomCount.Value,
                BedroomCount = propertyVM.BedroomCount.Value,
                GarageCount = propertyVM.GarageCount.Value,
                RoomCount = propertyVM.RoomCount.Value,
                BuiltYear = propertyVM.BuiltYear.Value,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false,
                Description = propertyVM.Description,
                PropertyPhotos = new List<PropertyPhoto> { main },
                PropertyFeatures = propertyVM.FeatureIds.Select(fId =>
                    new PropertyFeature
                    {
                        FeatureId = fId
                    }
                ).ToList()
            };

            string text = string.Empty;


            if (propertyVM.AdditionalPhoto != null)
            {
                foreach (var file in propertyVM.AdditionalPhoto)
                {
                    if (!file.ValidateType("image/"))
                    {
                        ModelState.AddModelError(nameof(propertyVM.AdditionalPhoto), "File type is incorrect");
                        return View(propertyVM);
                    }
                    if (!file.ValidateSize(FileSize.MB, 2))
                    {
                        ModelState.AddModelError(nameof(propertyVM.AdditionalPhoto), "File size is incorrect");
                        return View(propertyVM);
                    }

                    property.PropertyPhotos.Add(new PropertyPhoto
                    {
                        Photo = await file.CreatFileAsync(_env.WebRootPath, "assets", "images"),
                        CreatedAt = DateTime.Now,
                        IsDeleted = false,
                        IsPrimary = false
                    });
                }
            }



            TempData["FileWarning"] = text;

            await _context.Properties.AddAsync(property);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
           
            if (id is null || id <= 0) return BadRequest();

            Property property = await _context.Properties
                .Include(p => p.PropertyPhotos)
                .Include(p => p.PropertyFeatures).FirstOrDefaultAsync(p => p.Id == id);

            if (property == null) return NotFound();

            UpdateAdminPropertyVM propertyVM = new UpdateAdminPropertyVM()
            {
                Categories = await _context.Categories.ToListAsync(),
                Exteriors = await _context.Exteriors.ToListAsync(),
                Agencies = await _context.Agencies.ToListAsync(),
                Agents = await _context.Agents.ToListAsync(),
                Parkings = await _context.Parkings.ToListAsync(),
                Features = await _context.Features.ToListAsync(),
                Views = await _context.Views.ToListAsync(),
                Roofs = await _context.Roofs.ToListAsync(),
                Status = await _context.Status.ToListAsync(),
                Types = await _context.Types.ToListAsync(),
                CategoryId = property.CategoryId,
                AgencyId = property.AgencyId,
                AgentId = property.AgentId,
                ViewId = property.ViewId,
                ExteriorId = property.ExteriorId,
                RoofId = property.RoofId,
                TypeId = property.TypeId,
                StatusId = property.StatusId,
                ParkingId = property.ParkingId,
                Area = property.Area,
                Price = property.Price,
                BedroomCount = property.BedroomCount,
                BathroomCount = property.BedroomCount,
                GarageCount = property.GarageCount,
                BuiltYear = property.BuiltYear,
                LotSize = property.LotSize,
                Location = property.Location,
                Description = property.Description,
                RoomCount = property.RoomCount,
                SchoolDistrict = property.SchoolDistrict,
                FeatureIds = property.PropertyFeatures.Select(f => f.FeatureId).ToList(),
                PropertiesPhoto = property.PropertyPhotos
            };

            return View(propertyVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateAdminPropertyVM propertyVM)
        {
           
            if (id is null || id <= 0) return BadRequest();

            Property property = await _context.Properties.Include(p => p.PropertyPhotos).Include(p => p.PropertyFeatures).FirstOrDefaultAsync(p => p.Id == id);

            if (property == null) return NotFound();

            propertyVM.Agencies = await _context.Agencies.ToListAsync();
            propertyVM.Agents = await _context.Agents.ToListAsync();
            propertyVM.Roofs = await _context.Roofs.ToListAsync();
            propertyVM.Views = await _context.Views.ToListAsync();
            propertyVM.Exteriors = await _context.Exteriors.ToListAsync();
            propertyVM.Parkings = await _context.Parkings.ToListAsync();
            propertyVM.Status = await _context.Status.ToListAsync();
            propertyVM.Types = await _context.Types.ToListAsync();
            propertyVM.Features = await _context.Features.ToListAsync();
            propertyVM.Categories = await _context.Categories.ToListAsync();
            //propertyVM.PropertiesPhoto = await _context.PropertiesPhotos.ToListAsync();
            propertyVM.PropertiesPhoto = property.PropertyPhotos;


            if (!ModelState.IsValid)
            {
                return View(propertyVM);
            }


            bool category = propertyVM.Categories.Any(c => c.Id == propertyVM.CategoryId);

            if (!category)
            {
                ModelState.AddModelError(nameof(UpdateAdminPropertyVM.CategoryId), "Categories are wrong");
                return View(propertyVM);
            }


            if (property.AgencyId != propertyVM.AgencyId)
            {
                bool result = propertyVM.Agencies.Any(c => c.Id == propertyVM.AgencyId);

                if (!result)
                {
                    return View(propertyVM);
                }
            }

            if (property.AgentId != propertyVM.AgentId)
            {
                bool result = propertyVM.Agents.Any(c => c.Id == propertyVM.AgentId);

                if (!result)
                {
                    return View(propertyVM);
                }
            }

            if (property.ViewId != propertyVM.ViewId)
            {
                bool result = propertyVM.Views.Any(c => c.Id == propertyVM.ViewId);

                if (!result)
                {
                    return View(propertyVM);
                }
            }

            if (property.RoofId != propertyVM.RoofId)
            {
                bool result = propertyVM.Roofs.Any(c => c.Id == propertyVM.RoofId);

                if (!result)
                {
                    return View(propertyVM);
                }
            }

            if (property.ExteriorId != propertyVM.ExteriorId)
            {
                bool result = propertyVM.Exteriors.Any(c => c.Id == propertyVM.ExteriorId);

                if (!result)
                {
                    return View(propertyVM);
                }
            }

            if (property.ParkingId != propertyVM.ParkingId)
            {
                bool result = propertyVM.Parkings.Any(c => c.Id == propertyVM.ParkingId);

                if (!result)
                {
                    return View(propertyVM);
                }
            }

            if (propertyVM.FeatureIds is null)
            {
                propertyVM.FeatureIds = new();
            }

            if (propertyVM.Features is not null)
            {
                bool featureResult = propertyVM.FeatureIds.Any(tId => !propertyVM.Features.Exists(t => t.Id == tId));
            }


            _context.PropertyFeatures.RemoveRange((property.PropertyFeatures ?? new List<PropertyFeature>())
            .Where(pFeature => !(propertyVM.FeatureIds ?? new List<int>()).Exists(tId => tId == pFeature.FeatureId))
            .ToList());

            _context.PropertyFeatures.AddRange(
                (propertyVM.FeatureIds ?? new List<int>())
                .Where(fId => !(property.PropertyFeatures ?? new List<PropertyFeature>()).Exists(pFeature => pFeature.FeatureId == fId))
                .ToList()
                .Select(tId => new PropertyFeature { FeatureId = tId, PropertyId = property.Id }));


            if (propertyVM.MainPhoto != null)
            {
                if (!propertyVM.MainPhoto.ValidateType("image/"))
                {
                    ModelState.AddModelError(nameof(propertyVM.MainPhoto), "File type is incorrect");
                    return View(propertyVM);
                }
                if (!propertyVM.MainPhoto.ValidateSize(FileSize.MB, 2))
                {
                    ModelState.AddModelError(nameof(propertyVM.MainPhoto), "File size is incorrect");
                    return View(propertyVM);
                }
            }

            if (propertyVM.MainPhoto is not null)
            {
                string file = await propertyVM.MainPhoto.CreatFileAsync(_env.WebRootPath, "assets", "images");
                PropertyPhoto main = property.PropertyPhotos.FirstOrDefault(p => p.IsPrimary == true);

                if (main is not null)
                {
                    main.Photo.DeleteFile(_env.WebRootPath, "assets", "images");
                    property.PropertyPhotos.Remove(main);
                }

                property.PropertyPhotos.Add(new PropertyPhoto
                {
                    CreatedAt = DateTime.Now,
                    IsDeleted = false,
                    IsPrimary = true,
                    Photo = file,
                });

            }

            if (propertyVM.PhotoIds is null)
            {
                propertyVM.PhotoIds = new List<int>();
            }

            var deletedImages = await _context.PropertiesPhotos
                .Where(pp => pp.PropertyId == property.Id && !propertyVM.PhotoIds.Contains(pp.Id) && pp.IsPrimary == false)
                .ToListAsync();

            deletedImages.ForEach(di => di.Photo.DeleteFile(_env.WebRootPath, "assets", "images"));

            _context.PropertiesPhotos.RemoveRange(deletedImages);
            //await _context.SaveChangesAsync();

            if (propertyVM.AdditionalPhoto != null)
            {
                foreach (var file in propertyVM.AdditionalPhoto)
                {
                    if (!file.ValidateType("image/"))
                    {
                        ModelState.AddModelError(nameof(propertyVM.AdditionalPhoto), "File type is incorrect");
                        return View(propertyVM);
                    }
                    if (!file.ValidateSize(FileSize.MB, 2))
                    {
                        ModelState.AddModelError(nameof(propertyVM.AdditionalPhoto), "File size is incorrect");
                        return View(propertyVM);
                    }

                    property.PropertyPhotos.Add(new PropertyPhoto
                    {
                        Photo = await file.CreatFileAsync(_env.WebRootPath, "assets", "images"),
                        CreatedAt = DateTime.Now,
                        IsDeleted = false,
                        IsPrimary = false
                    });
                }
            }

            property.AgencyId = propertyVM.AgencyId.Value;
            property.AgentId = propertyVM.AgentId.Value;
            property.CategoryId = propertyVM.CategoryId.Value;
            property.ViewId = propertyVM.ViewId.Value;
            property.RoofId = propertyVM.RoofId.Value;
            property.ParkingId = propertyVM.ParkingId.Value;
            property.ExteriorId = propertyVM.ExteriorId.Value;
            property.Area = propertyVM.Area.Value;
            property.BathroomCount = propertyVM.BathroomCount.Value;
            property.BedroomCount = propertyVM.BedroomCount.Value;
            property.RoomCount = propertyVM.RoomCount.Value;
            property.GarageCount = propertyVM.GarageCount.Value;
            property.BuiltYear = propertyVM.BuiltYear;
            property.Description = propertyVM.Description;
            property.Location = propertyVM.Location;
            property.LotSize = propertyVM.LotSize.Value;
            property.Price = propertyVM.Price.Value;
            property.StatusId = propertyVM.StatusId;
            property.TypeId = propertyVM.TypeId;
            property.SchoolDistrict = propertyVM.SchoolDistrict;


            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int? id)
        {
           
            if (id is null || id <= 0) return BadRequest();
            Property property = await _context.Properties.FirstOrDefaultAsync(p => p.Id == id);
            if (property == null) return NotFound();

            _context.Properties.Remove(property);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
           
            if (id == null || id <= 0)
            {
                return BadRequest();
            }

            var property = await _context.Properties
                .Include(p => p.Agency)
                .Include(p => p.Agent)
                .Include(p => p.Category)
                .Include(p => p.View)
                .Include(p => p.Roof)
                .Include(p => p.Exterior)
                .Include(p => p.Parking)
                .Include(p => p.PropertyFeatures)
                .Include(p => p.PropertyPhotos)
                .Include(p => p.Status)
                .Include(p => p.Type)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (property == null)
            {
                return NotFound();
            }

            return View(property);
        }

    }
}
