using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Application.ViewModels.Agents;
using ModernEstate.Application.ViewModels.Properties;
using ModernEstate.Domain.Entities;
using ModernEstate.Persistence.Data;
namespace ModernEstate.MVC.Controllers
{
    public class AgentController(AppDbContext _context) : Controller
    {
        public async Task<IActionResult> Details(int? id, int page = 1)
        {
            if (id is null || id <= 0) return BadRequest();

            Agent agent = await _context.Agents.Include(a => a.Properties).FirstOrDefaultAsync(a => a.Id == id);

            if (agent == null) return BadRequest();

            if (page < 1) return BadRequest();

            int count = await _context.Properties.Where(p=>p.AgentId==id).CountAsync();

            double total = Math.Ceiling((double)count / 2);

            if (total < page) return BadRequest();

            AgentVM agentVM = new AgentVM()
            {
                Agent = agent,
                Properties = await _context.Properties
                .Where(a=>a.AgentId == id)
                .Skip((page-1)*2)
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
