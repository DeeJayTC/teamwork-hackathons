using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Teamwork_Hackathon.Models;
using Teamwork_Hackathon.Models.DTO;

namespace Teamwork_Hackathon.Areas.Admin.Controllers
{
    [Authorize(Roles="admin")]
    public class ConfigController : Controller
    {
        private readonly teamwork_hackathonContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public ConfigController(teamwork_hackathonContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            return RedirectToAction("Index", "Home", new {area = ""});
            var currentuser = await _userManager.GetUserAsync(User);
            var model = new HackathonBaseModel(currentuser);
            return View(model);
        }


        public async Task<IActionResult> InitVoting()
        {
            var currentuser = await _userManager.GetUserAsync(User);

            var voteRunning = _context.HackathonVoting.Any(p => p.IsCompleted == false);
            if (!voteRunning)
            {

                var hack = await _context.Hackathon.FirstOrDefaultAsync(p => p.Id == 3);
                hack.VotingIsActive = true;
                await _context.SaveChangesAsync();

                var vote = new HackathonVoting()
                {
                    CreatedBy = currentuser.Id,
                    HackathonId = 4,
                    IsCompleted = false,
                    VoteStartDate = DateTime.Now,
                    VoteEnabledFrom = new DateTime(2018, 04, 25, 10, 0, 0)
                };

                await _context.HackathonVoting.AddAsync(vote);
                await _context.SaveChangesAsync();


                var teams = await
                    _context.HackathonTeam.Where(p=>p.HackathonId == currentuser.ActiveHackathon).Include(p => p.HackathonMembers)
                        .Where(p => p.HackathonMembers.Count > 0)
                        .ToListAsync();

                var orderRandomizer = new Random(teams.Count);
                List<int> usedOrderNumbers = new List<int>();
                foreach (var team in teams.Where(p=>p.HackathonId == 4))
                {
                    var orderNo = GetRandomNumber(usedOrderNumbers, orderRandomizer, teams.Count);
                    usedOrderNumbers.Add(orderNo);
                    vote.HackathonVotingTeams.Add(new HackathonVotingTeams()
                    {
                        RunningOrder = orderNo,
                        Teamid = team.Id,
                        VotingId = vote.Id
                    });

                }
                await _context.SaveChangesAsync();


            }

            return RedirectToAction("Index", "VotingOverview");
        }



        public async Task<IActionResult> LockTeams()
        {
            var hack = await _context.Hackathon.FirstOrDefaultAsync(p => p.Id == 1);
            hack.VotingIsActive = true;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> UnlockTeams()
        {
            var hack = await _context.Hackathon.FirstOrDefaultAsync(p => p.Id == 1);
            hack.VotingIsActive = false;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }





        public int GetRandomNumber(List<int> usedNumbers, Random randomizer, int maxvalue)
        {
            int orderNumber = randomizer.Next(0, maxvalue);
            while (usedNumbers.Contains(orderNumber))
            {
                orderNumber =  randomizer.Next(0, maxvalue);
            }
            return orderNumber;

        }

    }
}
