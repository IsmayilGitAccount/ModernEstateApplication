using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Domain.Entities;
using ModernEstate.MVC.Areas.Admin.ViewModels.Parkings;
using ModernEstate.Persistence.Data;

namespace ModernEstate.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ParkingController(AppDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var parkingVMs = await _context.Parkings.Select(p => new GetAdminParkingVM
            {
                Id = p.Id,
                ParkingType = p.ParkingType,
            }).ToListAsync();

            return View(parkingVMs);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAdminParkingVM parkingVM)
        {
            if (!ModelState.IsValid)
            {
                return View(parkingVM);
            }

            bool result = await _context.Parkings.AnyAsync(p => p.ParkingType.Trim() == parkingVM.ParkingType.Trim());

            if (result)
            {
                ModelState.AddModelError(nameof(CreateAdminParkingVM.ParkingType), $"{parkingVM.ParkingType} is already taken, please try again!");
                return View(parkingVM);
            }

            Parking parking = new Parking()
            {
                ParkingType = parkingVM.ParkingType,
                CreatedAt = DateTime.Now,
                IsDeleted = false
            };

            await _context.Parkings.AddAsync(parking);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id is null || id <= 0) return BadRequest();
            Parking parking = await _context.Parkings.FirstOrDefaultAsync(p => p.Id == id);
            if (parking == null) return NotFound();

            UpdateAdminParkingVM parkingVM = new UpdateAdminParkingVM()
            {
                ParkingType = parking.ParkingType,
            };

            return View(parkingVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateAdminParkingVM parkingVM)
        {
            if (id is null || id <= 0) return BadRequest();
            Parking parking = await _context.Parkings.FirstOrDefaultAsync(p => p.Id == id);
            if (parking == null) return NotFound();

            if (!ModelState.IsValid)
            {
                return View(parkingVM);
            }

            bool result = await _context.Parkings.AnyAsync(p => p.ParkingType.Trim() == parkingVM.ParkingType.Trim() && p.Id != id);

            if (result)
            {
                ModelState.AddModelError(nameof(UpdateAdminParkingVM.ParkingType), $"{parkingVM.ParkingType} is already taken, please try again!");
                return View(parkingVM);
            }

            parking.ParkingType = parkingVM.ParkingType;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null || id <= 0) return BadRequest();
            Parking parking = await _context.Parkings.FirstOrDefaultAsync(p => p.Id == id);
            if (parking == null) return NotFound();

            _context.Parkings.Remove(parking);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id is null || id <= 0) return BadRequest();
            Parking parking = await _context.Parkings.FirstOrDefaultAsync(p => p.Id == id);
            if (parking == null) return NotFound();

            return View(parking);
        }

    }
}
