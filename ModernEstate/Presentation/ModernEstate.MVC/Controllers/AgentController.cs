﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Application.Utilities.Exceptions;
using ModernEstate.Application.ViewModels.Agents;
using ModernEstate.Domain.Entities;
using ModernEstate.Domain.Entities.Account;
using ModernEstate.Persistence.Data;
namespace ModernEstate.MVC.Controllers
{
    public class AgentController(AppDbContext _context, UserManager<AppUser> _userManager) : Controller
    {
        public async Task<IActionResult> Details(int? id, int page = 1)
        {
            if (id is null || id <= 0) throw new BadRequestException($"{id} is wrong!");

            Agent agent = await _context.Agents.Include(a => a.Properties).Include(a => a.Agency).FirstOrDefaultAsync(a => a.Id == id);

            if (agent == null) throw new BadRequestException($"Agent not found!");

            if (page < 1) throw new BadRequestException($"{page}th page is not found!");

            int count = await _context.Properties.Where(p => p.AgentId == id).CountAsync();

            double total = Math.Ceiling((double)count / 2);

            if (total < page) throw new NotFoundException($"Not found!");

            AgentVM agentVM = new AgentVM()
            {
                Agent = agent,
                Properties = await _context.Properties
                .Where(a => a.AgentId == id)
                .Skip((page - 1) * 2)
                .Take(2)
                .Include(p => p.PropertyPhotos).Where(p => p.AgentId == agent.Id).ToListAsync(),
                TotalPage = total,
                CurrentPage = page
            };
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return View(agentVM);
            }
            return View(agentVM);
        }
    }
}
