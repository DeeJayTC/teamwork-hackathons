using System;
using System.Collections.Generic;

namespace Teamwork_Hackathon.Models
{
    public partial class HackathonVotingCategory
    {
        public HackathonVotingCategory()
        {
            HackathonVotingVote = new HashSet<HackathonVotingVote>();
        }

        public int Id { get; set; }
        public int? HackathonId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Hackathon Hackathon { get; set; }
        public ICollection<HackathonVotingVote> HackathonVotingVote { get; set; }
    }
}
