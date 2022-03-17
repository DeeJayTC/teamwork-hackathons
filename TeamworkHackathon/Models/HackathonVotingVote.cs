using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Teamwork_Hackathon.Models
{
    public partial class HackathonVotingVote
    {
       
        public int Id { get; set; }
        public string UserId { get; set; }
        public int? TeamId { get; set; }
        public int? CategoryId { get; set; }
        public int? Points { get; set; }
        public DateTime? VoteDate { get; set; }
        public int? VotingId { get; set; }

        public HackathonVotingCategory Category { get; set; }
        public HackathonTeam Team { get; set; }
        public AspNetUsers User { get; set; }
        public HackathonVoting Voting { get; set; }

        [NotMapped]
        public VotingTeamOverview VotingOverview { get; set; }
    }
}
