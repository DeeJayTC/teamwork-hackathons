using System;
using System.Collections.Generic;

namespace Teamwork_Hackathon.Models
{
    public partial class HackathonVotingTeams
    {
        public int Id { get; set; }
        public int? RunningOrder { get; set; }
        public int? Teamid { get; set; }
        public int? VotingId { get; set; }

        public HackathonTeam Team { get; set; }
        public HackathonVoting Voting { get; set; }
    }
}
