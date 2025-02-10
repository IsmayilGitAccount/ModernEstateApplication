using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Application.ViewModels.Agents;
using ModernEstate.Domain.Entities;
using ModernEstate.Domain.Enums;
using ModernEstate.MVC.Areas.Admin.ViewModels.Agents;
using ModernEstate.MVC.Areas.Admin.ViewModels.Services;
using ModernEstate.MVC.Utilities.Extensions;
using ModernEstate.Persistence.Data;

namespace ModernEstate.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServiceController(AppDbContext _context, IWebHostEnvironment _env) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var serviceVMs = await _context.Services.Include(s=>s.Agency).Select(s=>new GetAgentServiceVM
            {
                Id = s.Id,
                Photo = s.Photo,
                Title = s.Title,
            }).ToListAsync();

            return View(serviceVMs);
        }

        public async Task<IActionResult> Create()
        {
            CreateAdminServiceVM serviceVM = new CreateAdminServiceVM()
            {
                Agencies = await _context.Agencies.ToListAsync()
            };

            return View(serviceVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAdminServiceVM serviceVM)
        {
            serviceVM.Agencies = await _context.Agencies.ToListAsync();

            if (!ModelState.IsValid)
            {
                return View(serviceVM);
            }

            bool result = await _context.Agencies.AnyAsync(a => a.Id == serviceVM.AgencyId);

            if (!result)
            {
                ModelState.AddModelError(nameof(CreateAdminServiceVM.AgencyId), "Agency is not exists");
                return View(serviceVM);
            }

            Service service = new Service()
            {
                Title = serviceVM.Title,
                Description = serviceVM.Description,
                AgencyId = serviceVM.AgencyId.Value,
                CreatedAt = DateTime.Now,
                IsDeleted = false
            };

            if (serviceVM.Photo is not null)
            {
                if (!serviceVM.Photo.ValidateType("image/"))
                {
                    ModelState.AddModelError(nameof(CreateAdminAgentVM.Photo), "File type is incorrect, please try again!");
                    return View(serviceVM);
                }
                if (!serviceVM.Photo.ValidateSize(FileSize.MB, 2))
                {
                    ModelState.AddModelError(nameof(CreateAdminAgentVM.Photo), "File size is incorrect, please try again!");
                    return View(serviceVM);
                }

                service.Photo = await serviceVM.Photo.CreatFileAsync(_env.WebRootPath, "assets", "images");
            }

            await _context.Services.AddAsync(service);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id is null || id <= 0) return BadRequest();

            Service service = await _context.Services.FirstOrDefaultAsync(s => s.Id == id);

            if(service == null) return NotFound();

            UpdateAdminServiceVM serviceVM = new UpdateAdminServiceVM()
            {
                Agencies = await _context.Agencies.ToListAsync(),
                AgencyId = service.AgencyId,
                Title = service.Title,
                Description = service.Description,
            };

            return View(serviceVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateAdminServiceVM serviceVM)
        {
            serviceVM.Agencies = await _context.Agencies.ToListAsync();

            if (id is null || id <= 0) return BadRequest();

            Service service = await _context.Services.FirstOrDefaultAsync(s => s.Id == id);

            if (service == null) return NotFound();

            if (!ModelState.IsValid)
            {
                return View(serviceVM);
            }

            bool result = await _context.Agencies.AnyAsync(a => a.Id == serviceVM.AgencyId);

            if (!result)
            {
                ModelState.AddModelError(nameof(CreateAdminServiceVM.AgencyId), "Agency is not exists");
                return View(serviceVM);
            }

            service.AgencyId = serviceVM.AgencyId.Value;
            service.Title = serviceVM.Title;
            service.Description = serviceVM.Description;
            service.IsDeleted = false;

            if (serviceVM.Photo is not null)
            {
                if (!serviceVM.Photo.ValidateType("image/"))
                {
                    ModelState.AddModelError(nameof(CreateAdminAgentVM.Photo), "File type is incorrect, please try again!");
                    return View(serviceVM);
                }
                if (!serviceVM.Photo.ValidateSize(FileSize.MB, 2))
                {
                    ModelState.AddModelError(nameof(CreateAdminAgentVM.Photo), "File size is incorrect, please try again!");
                    return View(serviceVM);
                }
                service.Photo.DeleteFile(_env.WebRootPath, "assets", "images");
                service.Photo = await serviceVM.Photo.CreatFileAsync(_env.WebRootPath, "assets", "images");
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null || id <= 0) return BadRequest();

            Service service = await _context.Services.FirstOrDefaultAsync(s => s.Id == id);

            if (service == null) return NotFound();

            if (service.Photo is not null)
            {
                service.Photo.DeleteFile(_env.WebRootPath, "assets", "images");
            }

            _context.Services.Remove(service);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id is null || id <= 0) return BadRequest();

            Service service = await _context.Services.Include(s=>s.Agency).FirstOrDefaultAsync(s => s.Id == id);

            if (service == null) return NotFound();

            return View(service);
        }
    }
}
