using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Domain.Entities;
using ModernEstate.MVC.Areas.Admin.ViewModels.Agencies;
using ModernEstate.Persistence.Data;

namespace ModernEstate.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AgencyController(AppDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var agencyVMs = await _context.Agencies.Select(a => new GetAdminAgencyVM
            {
                Id = a.Id,
                AgencyName = a.AgencyName,
            }).ToListAsync();

            return View(agencyVMs);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAdminAgencyVM agencyVM)
        {
            if (!ModelState.IsValid)
            {
                return View(agencyVM);
            }

            bool result = await _context.Agencies.AnyAsync(a => a.AgencyName.Trim() == agencyVM.AgencyName.Trim());

            if (result)
            {
                ModelState.AddModelError(nameof(CreateAdminAgencyVM.AgencyName), $"{agencyVM.AgencyName} is already taken, please try again!");
                return View(agencyVM);
            }

            Agency agency = new Agency()
            {
                AgencyName = agencyVM.AgencyName,
                Description = agencyVM.Description,
                CreatedAt = DateTime.Now,
                IsDeleted = false
            };

            if(agencyVM.Description.Length > 1000)
            {
                ModelState.AddModelError(nameof(agencyVM.Description), "Description must be less than 1000 characters!");
                return View(agencyVM);
            }

            await _context.Agencies.AddAsync(agency);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id is null || id <= 0) return BadRequest();
            Agency agency = await _context.Agencies.FirstOrDefaultAsync(a => a.Id == id);
            if (agency == null) return NotFound();

            UpdateAdminAgencyVM agencyVM = new UpdateAdminAgencyVM()
            {
                AgencyName = agency.AgencyName,
                Description = agency.Description,
            };

            return View(agencyVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateAdminAgencyVM agencyVM)
        {
            if (id is null || id <= 0) return BadRequest();
            Agency agency = await _context.Agencies.FirstOrDefaultAsync(a => a.Id == id);
            if (agency == null) return NotFound();

            if (!ModelState.IsValid)
            {
                return View(agencyVM);
            }

            bool result = await _context.Agencies.AnyAsync(a => a.AgencyName.Trim() == agencyVM.AgencyName.Trim() && a.Id != id);

            if (result)
            {
                ModelState.AddModelError(nameof(UpdateAdminAgencyVM.AgencyName), $"{agencyVM.AgencyName} is already taken, please try again!");
                return View(agencyVM);
            }

            agency.AgencyName = agencyVM.AgencyName;

            if (agencyVM.Description.Length > 1000)
            {
                ModelState.AddModelError(nameof(agencyVM.Description), "Description must be less than 1000 characters!");
                return View(agencyVM);
            }
            agency.Description = agencyVM.Description;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null || id <= 0) return BadRequest();
            Agency agency = await _context.Agencies.FirstOrDefaultAsync(a => a.Id == id);
            if (agency == null) return NotFound();

            _context.Agencies.Remove(agency);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id is null || id <= 0) return BadRequest();
            Agency agency = await _context.Agencies.FirstOrDefaultAsync(a => a.Id == id);
            if (agency == null) return NotFound();

            return View(agency);
        }
    }
}
