using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Teamwork_Hackathon.Models;
using Teamwork_Hackathon.Models.DTO;

namespace Teamwork_Hackathon.Controllers
{
    [Authorize]
    public class VotingOverviewController : Controller
    {
        private readonly teamwork_hackathonContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public VotingOverviewController(teamwork_hackathonContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [Authorize(Roles ="admin")]
        public async Task<IActionResult> LockTeams()
        {
            var hack = await _context.Hackathon.FirstOrDefaultAsync(p => p.Id == 1);
            hack.VotingIsActive = true;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }
        [Authorize(Roles = "admin")]
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
                orderNumber = randomizer.Next(0, maxvalue);
            }
            return orderNumber;

        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> InitVoting()
        {
            var currentuser = await _userManager.GetUserAsync(User);

            var voteRunning = _context.HackathonVoting.Any(p => p.IsCompleted == false && p.HackathonId == 4);
            if (!voteRunning)
            {

                var hack = await _context.Hackathon.FirstOrDefaultAsync(p => p.Id == 4);
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
                    _context.HackathonTeam.Where(p => p.HackathonId == currentuser.ActiveHackathon).Include(p => p.HackathonMembers)
                        .Where(p => p.HackathonMembers.Count > 0)
                        .ToListAsync();

                var orderRandomizer = new Random(teams.Count);
                List<int> usedOrderNumbers = new List<int>();
                foreach (var team in teams.Where(p => p.HackathonId == 4))
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




        // GET: VotingOverview
        public async Task<IActionResult> Index()
        {
            var currentuser = await _userManager.GetUserAsync(User);
            var teamList = await _context.VotingTeams.ToListAsync();


            List<HackathonVotingVote> votes = new List<HackathonVotingVote>();
            if(_context.HackathonVotingVote.Any(p => p.UserId == currentuser.Id && p.Team.HackathonId == currentuser.ActiveHackathon)) { 
                votes = await _context.HackathonVotingVote.Where(p => p.UserId == currentuser.Id).ToListAsync();
            }


            foreach (var team in teamList)
            {
                if (votes.Any(p => p.TeamId == team.ID && p.UserId == currentuser.Id))
                {
                    team.HasVoted = true;
                }
            }

            var modeldata = new VotingDTO(currentuser)
            {
                Teams = teamList
            };
            return View(modeldata);
        }


        public async Task<IActionResult> Results()
        {        
            var currentuser = await _userManager.GetUserAsync(User);
            var hackaton = await _context.Hackathon.FirstOrDefaultAsync(p => p.Id == 4);
            if (!User.IsInRole("admin")) return NotFound();
            var modeldata = new VotingResultsDTO(currentuser);
            return View(modeldata);
        }


        public async Task<IActionResult> ResultsByCategory()
        {
            var currentuser = await _userManager.GetUserAsync(User);
            var hackaton = await _context.Hackathon.FirstOrDefaultAsync(p => p.Id == 4);
            if (!User.IsInRole("admin")) return NotFound();


            
            var results = await _context.VotingResultsByCategory.ToListAsync();
            
            var modeldata = new VotingResultsDTO(currentuser);
            modeldata.CategoryResults = results;

            return PartialView("ResultsCategory",modeldata);
        }

        public async Task<IActionResult> ResultsTotal()
        {
            var currentuser = await _userManager.GetUserAsync(User);
            var hackaton = await _context.Hackathon.FirstOrDefaultAsync(p => p.Id == 4);
            if (!User.IsInRole("admin")) return NotFound();

            var results = await _context.VotingResultsTotal.ToListAsync();

            var modeldata = new VotingResultsDTO(currentuser);
            modeldata.TotalResults = results;
            return PartialView("ResultsTotal", modeldata);
        }


        public async Task<IActionResult> ResultsByCategoryNonTech()
        {
            var currentuser = await _userManager.GetUserAsync(User);
            var hackaton = await _context.Hackathon.FirstOrDefaultAsync(p => p.Id == 4);
            if (!User.IsInRole("admin")) return NotFound();



            var results = await _context.VotingResultsByCategoryNonTech.ToListAsync();

            var modeldata = new VotingResultsDTO(currentuser);
            modeldata.CategoryResultsNonTech = results;

            return PartialView("ResultsCategoryNonTech", modeldata);
        }

        public async Task<IActionResult> ResultsTotalNonTech()
        {
            var currentuser = await _userManager.GetUserAsync(User);
            var hackaton = await _context.Hackathon.FirstOrDefaultAsync(p => p.Id == 4);
            if (!User.IsInRole("admin")) return NotFound();

            var results = await _context.VotingResultsTotalNonTech.ToListAsync();

            var modeldata = new VotingResultsDTO(currentuser);
            modeldata.TotalResultsNonTech = results;
            return PartialView("ResultsTotalNonTech", modeldata);
        }


        public async Task<IActionResult> VotesPerTeam(int id)
        {
            var currentuser = await _userManager.GetUserAsync(User);
            var team = await _context.HackathonTeam.FirstAsync(p => p.Id == id);
            var votes =
                await _context.HackathonVotingVote.Where(p => p.UserId == currentuser.Id && p.TeamId == id)
                    .ToListAsync();
            var categories = await _context.HackathonVotingCategory.Where(p=>p.HackathonId == 4).ToListAsync();

            var model = new VotingVoteDTO();
            model.Votes = votes;
            model.Cats = categories;
            model.Team = team;

            return PartialView("VotesPerTeam", model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddVote([Bind("val_5,val_6,val_7,val_8, TeamID, VotingID")] VotingInput vote)
        {

            try
            {
                var currentuser = await _userManager.GetUserAsync(User);
                var IsUserTeam = _context.HackathonTeam
                    .Include(p => p.HackathonMembers)
                    .Include(p => p.HackathonTeamOfferings)
                    .Any(p => p.HackathonMembers.Any(x => x.UserId == currentuser.Id) && p.Id == vote.TeamID);
                if (IsUserTeam) return NotFound();

                for (int i = 5; i < 9; i++)
                {
                    if (
                        _context.HackathonVotingVote.Any(p => p.TeamId == vote.TeamID && p.CategoryId.Value == i && p.UserId == currentuser.Id))
                    {
                        var telemetry = new Microsoft.ApplicationInsights.TelemetryClient();
                        telemetry.TrackTrace("CHEATER - " + currentuser.Email + ", tried to cheat during voting!");
                    }
                    int points = int.Parse(vote.GetType().GetProperty("val_" + i).GetValue(vote).ToString());
                    if(points > 10 || points < 0)
                    {
                        var telemetry = new Microsoft.ApplicationInsights.TelemetryClient();
                        telemetry.TrackTrace("CHEATER - " + currentuser.Email + ", tried to cheat during voting!");
                    }
                    else { 
                    var uservote = new HackathonVotingVote
                    {
                        TeamId = vote.TeamID,
                        CategoryId = i,
                        UserId =  currentuser.Id,
                        Points = points,
                        VoteDate = DateTime.Now
                    };

                    await _context.HackathonVotingVote.AddAsync(uservote);
                    await _context.SaveChangesAsync();
                    }
                }


                return RedirectToAction("Index", "VotingOverview", new {area = ""});
            }
            catch (Exception ex)
            {

            }

            //Verify theres no current vote so this can only be called once
            //Get all teams
            //Create vote table entries
            return RedirectToAction("Index", "VotingOverview", new { area = "" });
        }


    }


    public class VotingInput
    {
        public int TeamID { get; set; }
        public int val_1 { get; set; }
        public int val_2 { get; set; }
        public int val_3 { get; set; }
        public int val_4 { get; set; }
        public int val_5 { get; set; }
        public int val_6 { get; set; }
        public int val_7 { get; set; }
        public int val_8 { get; set; }
        public int val_9 { get; set; }
        public int val_10 { get; set; }
    }
}
