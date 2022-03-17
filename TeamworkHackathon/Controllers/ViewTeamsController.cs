using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Teamwork_Hackathon.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Teamwork_Hackathon.Extensions;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Teamwork_Hackathon.Models.DTO;

namespace Teamwork_Hackathon.Controllers
{

    public static class StringExt
    {
        public static bool ContainsAny(this string haystack, params string[] needles)
        {
            foreach (string needle in needles)
            {
                if (haystack.Contains(needle))
                    return true;
            }

            return false;
        }
    }


    [Authorize]
    public class ViewTeamsController : Controller
    {
        private readonly teamwork_hackathonContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public ViewTeamsController(teamwork_hackathonContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: ViewTeams
        public async Task<IActionResult> Index()
        {
            var currentuser = await _userManager.GetUserAsync(User);
            var teamwork_hackathonContext = _context.HackathonTeam.Where(p=>p.HackathonId == currentuser.ActiveHackathon).Include(h => h.Hackathon).Include(h => h.HackathonMembers).Include(h => h.HackathonTeamOfferings);
            foreach (var team in teamwork_hackathonContext)
            {
                foreach(var member in team.HackathonMembers)
                {
                    var user = _context.AspNetUsers.FirstOrDefault(p => p.Id == member.UserId);
                    if (user.Id == currentuser.Id) team.IsUserTeam = true;
                    member.DisplayName = user.FirstName + " " + user.LastName;
                    member.ProfileImage = user.ProfileImage;
                }
            }

            var modeldata = new HackathonTeamListDto(currentuser)
            {
                Teams = await teamwork_hackathonContext.ToListAsync()
            };

            return View(modeldata);
        }

        // GET: ViewTeams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var currentuser = await _userManager.GetUserAsync(User);
            var hackathonTeam = await _context.HackathonTeam.Where(p => p.HackathonId == currentuser.ActiveHackathon)
                .Include(h => h.Hackathon).Include(h => h.HackathonMembers).Include(h => h.HackathonTeamOfferings)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (hackathonTeam == null)
            {
                return NotFound();
            }

           
            foreach (var member in hackathonTeam.HackathonMembers)
            {
                var user = _context.AspNetUsers.FirstOrDefault(p => p.Id == member.UserId);
                if (user.Id == currentuser.Id) hackathonTeam.IsUserTeam = true;
                member.DisplayName = user.FirstName + " " + user.LastName;
                member.ProfileImage = user.ProfileImage;
            }

            var modeldata = new HackathonTeamDto(currentuser)
            {
                Team = hackathonTeam
            };
            return View(modeldata);
        }

        // GET: ViewTeams/Create
        public IActionResult Create()
        {
            ViewData["HackathonId"] = new SelectList(_context.Hackathon, "Id", "Id");
            return View();
        }

        // POST: ViewTeams/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HackathonId,TeamName,TeamDescription,Url,Logo,IsTechnical")] HackathonTeam hackathonTeam)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hackathonTeam);
                var user = await _userManager.GetUserAsync(User);
                hackathonTeam.CreatedByID = user.Id;
                hackathonTeam.HackathonId = user.ActiveHackathon;

                //Make sure the user is not looking for a team any longer
                var entry = await _context.HackathonSearchMembers.SingleOrDefaultAsync(p => p.UserId == user.Id);
                if(entry != null)
                {
                    _context.HackathonSearchMembers.Remove(entry);
                    await _context.SaveChangesAsync();
                }



                if (string.IsNullOrEmpty(hackathonTeam.Logo) || !Uri.IsWellFormedUriString(hackathonTeam.Logo,UriKind.RelativeOrAbsolute))
                {
                    hackathonTeam.Logo = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/84/Picture_font_awesome.svg/600px-Picture_font_awesome.svg.png";
                }
                await _context.SaveChangesAsync();

                

                //Add creator as team member
                var member = new HackathonMembers()
                {
                    TeamId = hackathonTeam.Id,
                    UserId = user.Id
                };
                hackathonTeam.HackathonMembers.Add(member);
                await _context.SaveChangesAsync();

                return Json(new { success = true, url = "/viewTeams/details/" + hackathonTeam.Id });
            }
            ViewData["HackathonId"] = new SelectList(_context.Hackathon, "Id", "Id", hackathonTeam.HackathonId);
            return PartialView(hackathonTeam);
        }

        // GET: ViewTeams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var currentuser = await _userManager.GetUserAsync(User);
            var hackathonTeam = await _context.HackathonTeam.Where(p=>p.HackathonId == currentuser.ActiveHackathon).Include(p=>p.HackathonMembers).SingleOrDefaultAsync(m => m.Id == id);
            if (hackathonTeam == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            var IsMember = hackathonTeam.HackathonMembers.Any(p => p.UserId == user.Id);
            if (!IsMember)
            {
                throw new Exception("Only Members can edit a Team");
            }

            ViewData["HackathonId"] = new SelectList(_context.Hackathon, "Id", "Id", hackathonTeam.HackathonId);
            return View(hackathonTeam);
        }


        public async Task<IActionResult> Leave(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var hack = await _context.Hackathon.FirstAsync(p => p.Id == 1);
            if (hack.VotingIsActive && !User.IsInRole("admin")) return NotFound();

            var hackathonTeam = await _context.HackathonTeam
                .Include(h => h.Hackathon).Include(h => h.HackathonMembers).Include(h => h.HackathonTeamOfferings)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hackathonTeam == null)
            {
                return NotFound();
            }

            var currentuser = await _userManager.GetUserAsync(User);
            var usrTeam = hackathonTeam.HackathonMembers.SingleOrDefault(x => x.UserId == currentuser.Id);
            if (usrTeam != null)
            {
                //Add creator as team member
                var member = new HackathonMembers()
                {
                    TeamId = hackathonTeam.Id,
                    UserId = currentuser.Id
                };
                hackathonTeam.HackathonMembers.Remove(usrTeam);
                await _context.SaveChangesAsync();
                return RedirectToAction("index", "home");
            }
            else
            {
                return RedirectToAction("details", "viewteams", new { area="",ID = hackathonTeam.Id });
            }
        }


        public async Task<IActionResult> Join(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hack = await _context.Hackathon.FirstAsync(p => p.Id == 1);
            if (hack.VotingIsActive && !User.IsInRole("admin")) return NotFound();

            var hackathonTeam = await _context.HackathonTeam
                .Include(h => h.Hackathon).Include(h => h.HackathonMembers).Include(h => h.HackathonTeamOfferings)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (hackathonTeam == null)
            {
                return NotFound();
            }

            var currentuser = await _userManager.GetUserAsync(User);
            var usrTeam = hackathonTeam.HackathonMembers.Any(x => x.UserId == currentuser.Id);
            if (!usrTeam)
            {
                //Add creator as team member
                var member = new HackathonMembers()
                {
                    TeamId = hackathonTeam.Id,
                    UserId = currentuser.Id
                };
                hackathonTeam.HackathonMembers.Add(member);


                //Make sure the user is not looking for a team any longer
                var entry = await _context.HackathonSearchMembers.SingleOrDefaultAsync(p => p.UserId == currentuser.Id);
                if (entry != null)
                {
                    _context.HackathonSearchMembers.Remove(entry);
                }


                await _context.SaveChangesAsync();
                return RedirectToAction("details", "viewteams", new { area="",ID = hackathonTeam.Id });
            }
            else
            {
                return View(hackathonTeam);
            }
        }


        public async Task<IActionResult> JoinPosition(int? id , int? posId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hack = await _context.Hackathon.FirstAsync(p => p.Id == 1);
            if (hack.VotingIsActive && !User.IsInRole("admin")) return NotFound();

            var hackathonTeam = await _context.HackathonTeam
                .Include(h => h.Hackathon).Include(h => h.HackathonMembers).Include(h => h.HackathonTeamOfferings)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (hackathonTeam == null)
            {
                return NotFound();
            }

            var currentuser = await _userManager.GetUserAsync(User);
            var usrTeam = hackathonTeam.HackathonMembers.Any(x => x.UserId == currentuser.Id);
            if (!usrTeam)
            {
                //Add creator as team member
                var member = new HackathonMembers()
                {
                    TeamId = hackathonTeam.Id,
                    UserId = currentuser.Id
                };
                hackathonTeam.HackathonMembers.Add(member);


                // Remove team offering by id
                var offering = await _context.HackathonTeamOfferings.SingleOrDefaultAsync(p => p.Id == posId);
                if(offering != null)
                {
                    _context.HackathonTeamOfferings.Remove(offering);
                }

                //Make sure the user is not looking for a team any longer
                var entry = await _context.HackathonSearchMembers.SingleOrDefaultAsync(p => p.UserId == currentuser.Id);
                if (entry != null)
                {
                    _context.HackathonSearchMembers.Remove(entry);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction("details", "viewteams", new { area = "", ID = hackathonTeam.Id });
            }
            else
            {
                return View(hackathonTeam);
            }
        }


        public async Task<IActionResult> Recruit(int? id, int? teamID)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hack = await _context.Hackathon.FirstAsync(p => p.Id == 1);
            if (hack.VotingIsActive && !User.IsInRole("admin")) return NotFound();

            var hackathonTeam = await _context.HackathonTeam.Include(h => h.Hackathon).Include(h => h.HackathonMembers).SingleOrDefaultAsync(m => m.Id == teamID);
            if (hackathonTeam == null)
            {
                return NotFound();
            }

            var userToAdd = await _context.HackathonSearchMembers.SingleOrDefaultAsync(p => p.Id == id);
            var user = _context.AspNetUsers.FirstOrDefault(p => p.Id == userToAdd.UserId);

            //Add creator as team member
            var member = new HackathonMembers()
            {
                TeamId = hackathonTeam.Id,
                UserId = user.Id
            };
            hackathonTeam.HackathonMembers.Add(member);
            _context.HackathonSearchMembers.Remove(userToAdd);
            await _context.SaveChangesAsync();
            return RedirectToAction("details", "viewteams", new { area = "", ID = hackathonTeam.Id });
        }


        // POST: ViewTeams/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HackathonId,TeamName,TeamDescription,Url,Logo,IsTechnical")] HackathonTeam hackathonTeam)
        {
            if (id != hackathonTeam.Id)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                try
                {
                    var currentuser = await _userManager.GetUserAsync(User);


                    if (hackathonTeam.TeamName.ContainsAny("<script","</script>","<style","</style","<link","<div", "<") ||
                        hackathonTeam.TeamDescription.ContainsAny("<script", "</script>", "<style", "</style", "<link", "<div", "<")
                        )
                    {
                        var telemetry = new Microsoft.ApplicationInsights.TelemetryClient();
                        telemetry.TrackTrace("CHEATER - " + currentuser.Email + ", tried to mess with the system!");
                    }
                    hackathonTeam.HackathonId = currentuser.ActiveHackathon;
                    _context.Update(hackathonTeam);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HackathonTeamExists(hackathonTeam.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return Json(new { success = true, url = "/viewTeams/details/" + hackathonTeam.Id });
            }
            ViewData["HackathonId"] = new SelectList(_context.Hackathon, "Id", "Id", hackathonTeam.HackathonId);
            return View(hackathonTeam);
        }




        // GET: ViewTeams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hack = await _context.Hackathon.FirstAsync(p => p.Id == 1);
            if (hack.VotingIsActive && !User.IsInRole("admin")) return NotFound();

            var hackathonTeam = await _context.HackathonTeam
                .Include(h => h.Hackathon).Include(p => p.HackathonMembers)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (hackathonTeam == null)
            {
                return NotFound();
            }
            var user = await _userManager.GetUserAsync(User);
            if(hackathonTeam.CreatedByID != user.Id)
            {
                throw new Exception("Only the creator can delete a team", null);
            }
            return View(hackathonTeam);
        }

        // POST: ViewTeams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hackathonTeam = await _context.HackathonTeam.Include(p=>p.HackathonMembers).SingleOrDefaultAsync(m => m.Id == id);
            var user = await _userManager.GetUserAsync(User);
            if (hackathonTeam.CreatedByID != user.Id)
            {
                throw new Exception("Only the creator can delete a team", null);
            }

            _context.HackathonTeam.Remove(hackathonTeam);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HackathonTeamExists(int id)
        {
            return _context.HackathonTeam.Any(e => e.Id == id);
        }
    }
}
