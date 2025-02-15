using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Application.ViewModels.AdminPaginations;
using ModernEstate.Areas.Admin.ViewModels.Types;
using ModernEstate.Areas.Admin.ViewModels.Views;
using ModernEstate.Domain.Entities;
using ModernEstate.Persistence.Data;

namespace ModernEstate.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PropertyViewController(AppDbContext _context) : Controller
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

            int count = await _context.Views.CountAsync();

            double total = Math.Ceiling((double)count / 3);

            if (page > total) return BadRequest();

            var viewVMs = await _context.Views.Select(v => new GetAdminViewVM
            {
                Id = v.Id,
                ViewType = v.ViewType,
            }).Skip((page-1)*3).Take(3).ToListAsync();

            PaginationVM<GetAdminViewVM> paginationVM = new PaginationVM<GetAdminViewVM>()
            {
                TotalPage = total,
                CurrentPage = page,
                Items = viewVMs
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
        public async Task<IActionResult> Create(CreateAdminViewVM viewVM)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("Admin"))
            {
                return RedirectToAction("Login", "Account");
            }
            if (!ModelState.IsValid)
            {
                return View(viewVM);
            }

            bool result = await _context.Views.AnyAsync(v => v.ViewType.Trim() == viewVM.ViewType.Trim());

            if (result)
            {
                ModelState.AddModelError(nameof(CreateAdminViewVM.ViewType), $"{viewVM.ViewType} is already taken, please try again!");
                return View(viewVM);
            }

            View view = new View()
            {
                ViewType = viewVM.ViewType,
                CreatedAt = DateTime.Now,
                IsDeleted = false
            };

            await _context.Views.AddAsync(view);

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
            View view = await _context.Views.FirstOrDefaultAsync(v => v.Id == id);
            if (view == null) return NotFound();

            UpdateAdminViewVM viewVM = new UpdateAdminViewVM()
            {
                ViewType = view.ViewType,
            };

            return View(viewVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateAdminViewVM viewVM)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("Admin"))
            {
                return RedirectToAction("Login", "Account");
            }
            if (id is null || id <= 0) return BadRequest();
            View view = await _context.Views.FirstOrDefaultAsync(v => v.Id == id);
            if (view == null) return NotFound();

            if (!ModelState.IsValid)
            {
                return View(viewVM);
            }

            bool result = await _context.Views.AnyAsync(v => v.ViewType.Trim() == viewVM.ViewType.Trim() && v.Id != id);

            if (result)
            {
                ModelState.AddModelError(nameof(UpdateAdminViewVM.ViewType), $"{viewVM.ViewType} is already taken, please try again!");
                return View(viewVM);
            }

            view.ViewType = viewVM.ViewType;

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
            View view = await _context.Views.FirstOrDefaultAsync(v => v.Id == id);
            if (view == null) return NotFound();

            _context.Views.Remove(view);

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
            View view = await _context.Views.FirstOrDefaultAsync(v => v.Id == id);
            if (view == null) return NotFound();

            return View(view);
        }

    }
}
