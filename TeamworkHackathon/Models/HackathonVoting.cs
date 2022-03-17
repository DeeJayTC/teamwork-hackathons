using System;
using System.Collections.Generic;

namespace Teamwork_Hackathon.Models
{
    public partial class HackathonVoting
    {
        public HackathonVoting()
        {
            HackathonVotingTeams = new HashSet<HackathonVotingTeams>();
            HackathonVotingVote = new HashSet<HackathonVotingVote>();
        }

        public int Id { get; set; }
        public int? HackathonId { get; set; }
        public DateTime? VoteStartDate { get; set; }
        public DateTime? VoteEnabledFrom { get; set; }
        public string CreatedBy { get; set; }
        public bool? IsCompleted { get; set; }

        public Hackathon Hackathon { get; set; }
        public ICollection<HackathonVotingTeams> HackathonVotingTeams { get; set; }
        public ICollection<HackathonVotingVote> HackathonVotingVote { get; set; }
    }
}
