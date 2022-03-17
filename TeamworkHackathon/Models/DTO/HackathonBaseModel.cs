using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Teamwork_Hackathon.Models.DTO
{
    public class HackathonBaseModel
    {
        public ApplicationUser CurrentUser { get; set; }
        private HackathonTeam userTeam;
        public HackathonTeam UserTeam
        {
            get
            {
                using (var ctx = new teamwork_hackathonContext()) {
                    var team = ctx.HackathonTeam.Where(p=>p.HackathonId == CurrentUser.ActiveHackathon).Include(p=>p.HackathonTeamOfferings).FirstOrDefault(p => p.HackathonMembers.Any(x => x.UserId == CurrentUser.Id));
                    if (team?.HackathonMembers != null)
                        foreach (var member in team.HackathonMembers)
                        {
                            var user = ctx.AspNetUsers.FirstOrDefault(p => p.Id == member.UserId);
                            if (user.Id == CurrentUser.Id) team.IsUserTeam = true;
                            member.DisplayName = user.FirstName + " " + user.LastName;
                            member.ProfileImage = user.ProfileImage;
                        }
                    return team;
                }
            }
        }

        public HackathonSearchMembers searchEntry;

        public HackathonSearchMembers SearchEntry
        {
            get
            {
                using (var ctx = new teamwork_hackathonContext())
                {
                   return ctx.HackathonSearchMembers.FirstOrDefault(p => p.UserId == CurrentUser.Id);
                }
            }
        }



        private Hackathon hackathon { get; set; }

        public Hackathon Hackathon
        {
            get
            {
                using (var ctx = new teamwork_hackathonContext())
                {
                    return hackathon ?? ctx.Hackathon.FirstOrDefault(p => p.Id == CurrentUser.ActiveHackathon);
                }
            }
        }

        public bool HasTeam => UserTeam != null;
        public bool IsTeamCreator => UserTeam != null && UserTeam.CreatedByID == CurrentUser.Id;

        public bool IsLookingForTeam => SearchEntry != null;

        public HackathonBaseModel(ApplicationUser pUser)
        {
            CurrentUser = pUser;
        }

    }
}
