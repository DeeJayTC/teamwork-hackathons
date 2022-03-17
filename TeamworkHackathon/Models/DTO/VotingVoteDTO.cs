using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Teamwork_Hackathon.Models.DTO
{
    public class VotingVoteDTO
    {
        public HackathonTeam Team { get; set; }
        public List<HackathonVotingVote> Votes { get; set; }
        public List<HackathonVotingCategory> Cats { get; set; }
    }
}
