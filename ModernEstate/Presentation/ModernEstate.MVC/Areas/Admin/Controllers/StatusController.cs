﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Application.Utilities.Exceptions;
using ModernEstate.Application.ViewModels.AdminPaginations;
using ModernEstate.Areas.Admin.ViewModels.Status;
using ModernEstate.Domain.Entities;
using ModernEstate.Persistence.Data;

namespace ModernEstate.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StatusController(AppDbContext _context) : Controller
    {
        public async Task<IActionResult> Index(int page = 1)
        {
            if (page < 1) throw new BadRequestException();

            int count = await _context.Status.CountAsync();

            double total = Math.Ceiling((double)count / 3);

            if (page > total) throw new BadRequestException();

            var statusVMs = await _context.Status.Select(s => new GetAdminStatusVM
            {
                Id = s.Id,
                StatusName = s.StatusName,
            }).Skip((page - 1) * 3).Take(3).ToListAsync();

            PaginationVM<GetAdminStatusVM> paginationVM = new PaginationVM<GetAdminStatusVM>()
            {
                TotalPage = total,
                CurrentPage = page,
                Items = statusVMs
            };

            return View(paginationVM);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAdminStatusVM statusVM)
        {
            if (!ModelState.IsValid)
            {
                return View(statusVM);
            }

            bool result = await _context.Status.AnyAsync(s => s.StatusName.Trim() == statusVM.StatusName.Trim());

            if (result)
            {
                ModelState.AddModelError(nameof(CreateAdminStatusVM.StatusName), $"{statusVM.StatusName} is already taken, please try again!");
                return View(statusVM);
            }

            Status status = new Status()
            {
                StatusName = statusVM.StatusName,
                CreatedAt = DateTime.Now,
                IsDeleted = false
            };

            await _context.Status.AddAsync(status);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id is null || id <= 0) throw new BadRequestException();
            Status status = await _context.Status.FirstOrDefaultAsync(s => s.Id == id);
            if (status == null) throw new NotFoundException();

            UpdateAdminStatusVM statusVM = new UpdateAdminStatusVM()
            {
                StatusName = status.StatusName,
            };

            return View(statusVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateAdminStatusVM statusVM)
        {
            if (id is null || id <= 0) throw new BadRequestException();
            Status status = await _context.Status.FirstOrDefaultAsync(s => s.Id == id);
            if (status == null) throw new NotFoundException();

            if (!ModelState.IsValid)
            {
                return View(statusVM);
            }

            bool result = await _context.Status.AnyAsync(s => s.StatusName.Trim() == statusVM.StatusName.Trim() && s.Id != id);

            if (result)
            {
                ModelState.AddModelError(nameof(UpdateAdminStatusVM.StatusName), $"{statusVM.StatusName} is already taken, please try again!");
                return View(statusVM);
            }

            status.StatusName = statusVM.StatusName;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null || id <= 0) throw new BadRequestException();
            Status status = await _context.Status.FirstOrDefaultAsync(s => s.Id == id);
            if (status == null) throw new NotFoundException();

            _context.Status.Remove(status);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id is null || id <= 0) throw new BadRequestException();
            Status status = await _context.Status.FirstOrDefaultAsync(s => s.Id == id);
            if (status == null) throw new NotFoundException();

            return View(status);
        }

    }
}
