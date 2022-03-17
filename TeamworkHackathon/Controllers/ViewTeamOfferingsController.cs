using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Teamwork_Hackathon.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Teamwork_Hackathon.Controllers
{
    [Authorize]
    public class ViewTeamOfferingsController : Controller
    {
        private readonly teamwork_hackathonContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public ViewTeamOfferingsController(teamwork_hackathonContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: ViewTeamOfferings
        public async Task<IActionResult> Index(int? id)
        {
            var currentuser = await _userManager.GetUserAsync(User);
            var teamwork_hackathonContext = _context.HackathonTeamOfferings.Include(h => h.Team).Where(p=>p.TeamId == id).Where(p=>p.Team.HackathonId ==  currentuser.ActiveHackathon);
            return PartialView(await teamwork_hackathonContext.ToListAsync());
        }

        // GET: ViewTeamOfferings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var currentuser = await _userManager.GetUserAsync(User);
            var hackathonTeamOfferings = await _context.HackathonTeamOfferings
                .Include(h => h.Team).Where(p=>p.Team.HackathonId == currentuser.ActiveHackathon)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (hackathonTeamOfferings == null)
            {
                return NotFound();
            }

            return View(hackathonTeamOfferings);
        }

        // GET: ViewTeamOfferings/Create
        public IActionResult Create(int? id)
        {
            ViewData["TeamId"] = id;
            return PartialView();
        }

        // POST: ViewTeamOfferings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TeamId,Title,Text")] HackathonTeamOfferings hackathonTeamOfferings)
        {
            if (ModelState.IsValid)
            {
                hackathonTeamOfferings.Id = 0;
                _context.Add(hackathonTeamOfferings);
                await _context.SaveChangesAsync();
                ViewData["TeamId"] = hackathonTeamOfferings.TeamId;
                return Json(new { success = true, url = "/viewteams/details/" + hackathonTeamOfferings.TeamId });
            }

            ViewData["TeamId"] = hackathonTeamOfferings.TeamId;
            return PartialView(hackathonTeamOfferings);
        }

        // GET: ViewTeamOfferings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hackathonTeamOfferings = await _context.HackathonTeamOfferings.SingleOrDefaultAsync(m => m.Id == id);
            if (hackathonTeamOfferings == null)
            {
                return NotFound();
            }
            ViewData["TeamId"] = hackathonTeamOfferings.TeamId;
            return PartialView(hackathonTeamOfferings);
        }

        // POST: ViewTeamOfferings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TeamId,Title,Text")] HackathonTeamOfferings hackathonTeamOfferings)
        {
            if (id != hackathonTeamOfferings.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hackathonTeamOfferings);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HackathonTeamOfferingsExists(hackathonTeamOfferings.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                ViewData["TeamId"] = hackathonTeamOfferings.TeamId;
                return Json(new { success = true, url = "/viewteams/details/" + hackathonTeamOfferings.TeamId });
            }
            ViewData["TeamId"] = hackathonTeamOfferings.TeamId;
            return Json(new { success = true, url = "/viewteams/details/" + hackathonTeamOfferings.TeamId });
        }

        // GET: ViewTeamOfferings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hackathonTeamOfferings = await _context.HackathonTeamOfferings
                .Include(h => h.Team)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (hackathonTeamOfferings == null)
            {
                return NotFound();
            }

            return PartialView(hackathonTeamOfferings);
        }

        // POST: ViewTeamOfferings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hackathonTeamOfferings = await _context.HackathonTeamOfferings.SingleOrDefaultAsync(m => m.Id == id);
            _context.HackathonTeamOfferings.Remove(hackathonTeamOfferings);
            await _context.SaveChangesAsync();
            ViewData["TeamId"] = hackathonTeamOfferings.TeamId;
            return Json(new { success = true, url = "/viewteams/details/" + hackathonTeamOfferings.TeamId });
        }

        private bool HackathonTeamOfferingsExists(int id)
        {
            return _context.HackathonTeamOfferings.Any(e => e.Id == id);
        }
    }
}
