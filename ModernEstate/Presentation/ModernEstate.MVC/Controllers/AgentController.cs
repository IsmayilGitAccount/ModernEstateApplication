using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Application.ViewModels.Agents;
using ModernEstate.Domain.Entities;
using ModernEstate.Persistence.Data;
namespace ModernEstate.MVC.Controllers
{
        public class AgentController(AppDbContext _context) : Controller
        {
            public async Task<IActionResult> Details(int? id)
            {
                if (id is null || id <= 0) return BadRequest();

                Agent agent = await _context.Agents.Include(a => a.Properties).FirstOrDefaultAsync(a => a.Id == id);

                if (agent == null) return BadRequest();

                AgentVM agentVM = new AgentVM()
                {
                    Agent = agent,
                    Properties = await _context.Properties.Include(p => p.PropertyPhotos).Where(p => p.AgentId == agent.Id).ToListAsync()
                };

                return View(agentVM);
            }
        }
}
