using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Teamwork_Hackathon.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Teamwork_Hackathon.Models.DTO;

namespace Teamwork_Hackathon.Controllers
{
    [Authorize]
    public class ViewMemberSearchController : Controller
    {
        private readonly teamwork_hackathonContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public ViewMemberSearchController(teamwork_hackathonContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: ViewMemberSearch
        public async Task<IActionResult> Index()
        {
            var peopleList = _context.HackathonSearchMembers.Include(h => h.IdNavigation).Include(h => h.User);

            var currentuser = await _userManager.GetUserAsync(User);
            var modeldata = new HackathonPeopleSearchDto(currentuser)
            {
                People = await peopleList.ToListAsync()
            };

            return View(modeldata);
        }

        // GET: ViewMemberSearch/Create
        public IActionResult Create()
        {
            ViewData["Id"] = new SelectList(_context.HackathonSearchMembers, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id");
            return View();
        }

        // POST: ViewMemberSearch/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,Title,Text")] HackathonSearchMembers hackathonSearchMembers)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                // Find if the user has an existing
                var existing = await _context.HackathonSearchMembers.SingleOrDefaultAsync(p => p.UserId == user.Id);
                if(existing != null)
                {
                    existing.Text = hackathonSearchMembers.Text;
                    existing.Title = hackathonSearchMembers.Title;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    hackathonSearchMembers.UserId = user.Id;
                    _context.Add(hackathonSearchMembers);
                    await _context.SaveChangesAsync();
                }

                return Json(new { success = true, url = "/home" });
            }
            ViewData["Id"] = new SelectList(_context.HackathonSearchMembers, "Id", "Id", hackathonSearchMembers.Id);
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", hackathonSearchMembers.UserId);
            return View(hackathonSearchMembers);
        }

        // GET: ViewMemberSearch/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hackathonSearchMembers = await _context.HackathonSearchMembers.SingleOrDefaultAsync(m => m.Id == id);
            if (hackathonSearchMembers == null)
            {
                return NotFound();
            }
            var user = await _userManager.GetUserAsync(User);
            if (hackathonSearchMembers.UserId != user.Id)
            {
                return NotFound();
            }
            ViewData["Id"] = new SelectList(_context.HackathonSearchMembers, "Id", "Id", hackathonSearchMembers.Id);
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", hackathonSearchMembers.UserId);
            return View(hackathonSearchMembers);
        }

        // POST: ViewMemberSearch/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,Title,Text")] HackathonSearchMembers hackathonSearchMembers)
        {
            if (id != hackathonSearchMembers.Id)
            {
                return NotFound();
            }
            var user = await _userManager.GetUserAsync(User);
            if (hackathonSearchMembers.UserId != user.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {

                try
                {
                    _context.Update(hackathonSearchMembers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HackathonSearchMembersExists(hackathonSearchMembers.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Json(new { success = true, url = "/home" });
            }
            ViewData["Id"] = new SelectList(_context.HackathonSearchMembers, "Id", "Id", hackathonSearchMembers.Id);
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", hackathonSearchMembers.UserId);
            return View(hackathonSearchMembers);
        }

        // GET: ViewMemberSearch/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var hackathonSearchMembers = await _context.HackathonSearchMembers
                .Include(h => h.IdNavigation)
                .Include(h => h.User)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (hackathonSearchMembers == null)
            {
                return NotFound();
            }
            var user = await _userManager.GetUserAsync(User);
            if (hackathonSearchMembers.UserId != user.Id)
            {
                return NotFound();
            }
            return View(hackathonSearchMembers);
        }

        // POST: ViewMemberSearch/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hackathonSearchMembers = await _context.HackathonSearchMembers.SingleOrDefaultAsync(m => m.Id == id);
            var user = await _userManager.GetUserAsync(User);
            if (hackathonSearchMembers.UserId != user.Id)
            {
                return NotFound();
            }
            _context.HackathonSearchMembers.Remove(hackathonSearchMembers);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HackathonSearchMembersExists(int id)
        {
            return _context.HackathonSearchMembers.Any(e => e.Id == id);
        }
    }
}
