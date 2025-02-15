using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Application.ViewModels.AdminPaginations;
using ModernEstate.Application.ViewModels.AdminRoofs;
using ModernEstate.Areas.Admin.ViewModels.Views;
using ModernEstate.Domain.Entities;
using ModernEstate.Persistence.Data;

namespace ModernEstate.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoofController(AppDbContext _context) : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("Admin"))
            {
                context.Result = new RedirectToActionResult("Login", "Account", new { area = "" });
            }

            base.OnActionExecuting(context);
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("Admin"))
            {
                return RedirectToAction("Login", "Account");
            }
            if (page < 1) return BadRequest();

            int count = await _context.Roofs.CountAsync();

            double total = Math.Ceiling((double)count / 3);

            if (page > total) return BadRequest();

            var roofVMs = await _context.Roofs.Select(r => new GetAdminRoofVM
            {
                Id = r.Id,
                RoofType = r.RoofType,
            }).Skip((page-1)*3).Take(3).ToListAsync();

            PaginationVM<GetAdminRoofVM> paginationVM = new PaginationVM<GetAdminRoofVM>()
            {
                TotalPage = total,
                CurrentPage = page,
                Items = roofVMs
            };

            return View(paginationVM);
        }

        public IActionResult Create()
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("Admin"))
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAdminRoofVM roofVM)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("Admin"))
            {
                return RedirectToAction("Login", "Account");
            }
            if (!ModelState.IsValid)
            {
                return View(roofVM);
            }

            bool result = await _context.Roofs.AnyAsync(r => r.RoofType.Trim() == roofVM.RoofType.Trim());

            if (result)
            {
                ModelState.AddModelError(nameof(CreateAdminRoofVM.RoofType), $"{roofVM.RoofType} is already taken, please try again!");
                return View(roofVM);
            }

            Roof roof = new Roof()
            {
                RoofType = roofVM.RoofType,
                CreatedAt = DateTime.Now,
                IsDeleted = false
            };

            await _context.Roofs.AddAsync(roof);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("Admin"))
            {
                return RedirectToAction("Login", "Account");
            }
            if (id is null || id <= 0) return BadRequest();
            Roof roof = await _context.Roofs.FirstOrDefaultAsync(r => r.Id == id);
            if (roof == null) return NotFound();

            UpdateAdminRoofVM roofVM = new UpdateAdminRoofVM()
            {
                RoofType = roof.RoofType,
            };

            return View(roofVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateAdminRoofVM roofVM)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("Admin"))
            {
                return RedirectToAction("Login", "Account");
            }
            if (id is null || id <= 0) return BadRequest();
            Roof roof = await _context.Roofs.FirstOrDefaultAsync(r => r.Id == id);
            if (roof == null) return NotFound();

            if (!ModelState.IsValid)
            {
                return View(roofVM);
            }

            bool result = await _context.Roofs.AnyAsync(r => r.RoofType.Trim() == roofVM.RoofType.Trim() && r.Id != id);

            if (result)
            {
                ModelState.AddModelError(nameof(UpdateAdminRoofVM.RoofType), $"{roofVM.RoofType} is already taken, please try again!");
                return View(roofVM);
            }

            roof.RoofType = roofVM.RoofType;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("Admin"))
            {
                return RedirectToAction("Login", "Account");
            }
            if (id is null || id <= 0) return BadRequest();
            Roof roof = await _context.Roofs.FirstOrDefaultAsync(r => r.Id == id);
            if (roof == null) return NotFound();

            _context.Roofs.Remove(roof);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("Admin"))
            {
                return RedirectToAction("Login", "Account");
            }
            if (id is null || id <= 0) return BadRequest();
            Roof roof = await _context.Roofs.FirstOrDefaultAsync(r => r.Id == id);
            if (roof == null) return NotFound();

            return View(roof);
        }
    }
}
