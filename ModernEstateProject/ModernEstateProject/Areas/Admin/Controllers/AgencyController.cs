using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernEstateProject.Areas.Admin.ViewModels.Agencies;
using ModernEstateProject.Data;
using ModernEstateProject.Models;

namespace ModernEstateProject.Areas.Admin.Controllers
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

            await _context.Agencies.AddAsync(agency);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id is null || id <= 0) return BadRequest();
            Agency agency = await _context.Agencies.FirstOrDefaultAsync(a => a.Id == id);
            if (agency == null) return BadRequest();

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
            if (agency == null) return BadRequest();

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
            agency.Description = agencyVM.Description;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null || id <= 0) return BadRequest();
            Agency agency = await _context.Agencies.FirstOrDefaultAsync(a => a.Id == id);
            if (agency == null) return BadRequest();

            _context.Agencies.Remove(agency);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id is null || id <= 0) return BadRequest();
            Agency agency = await _context.Agencies.FirstOrDefaultAsync(a => a.Id == id);
            if (agency == null) return BadRequest();

            return View(agency);
        }
    }
}
