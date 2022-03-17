using System;
using System.Collections.Generic;

namespace Teamwork_Hackathon.Models
{
    public partial class Hackathon
    {
        public Hackathon()
        {
            HackathonTeam = new HashSet<HackathonTeam>();
            HackathonVotingCategory = new HashSet<HackathonVotingCategory>();
        }

        public int Id { get; set; }
        public int? Year { get; set; }
        public DateTime? Date { get; set; }

        public bool VotingIsActive { get; set; }

        public ICollection<HackathonTeam> HackathonTeam { get; set; }
        public ICollection<HackathonVotingCategory> HackathonVotingCategory { get; set; }
    }
}
