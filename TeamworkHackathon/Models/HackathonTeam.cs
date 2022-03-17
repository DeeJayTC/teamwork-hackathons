using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Teamwork_Hackathon.Models
{
    public partial class HackathonTeam
    {
        public HackathonTeam()
        {
            HackathonMembers = new HashSet<HackathonMembers>();
            HackathonTeamOfferings = new HashSet<HackathonTeamOfferings>();
            HackathonVotingVote = new HashSet<HackathonVotingVote>();
            HackathonVotingTeams = new HashSet<HackathonVotingTeams>();
        }

        public bool IsTechnical { get; set; }

        public int Id { get; set; }
        public int? HackathonId { get; set; }
        [Required]
        [StringLength(100)]
        public string TeamName { get; set; }
        [Required]
        [StringLength(1000)]
        public string TeamDescription { get; set; }
        [Url]
        public string Url { get; set; }
        [Url]
        public string Logo { get; set; }

        public string CreatedByID { get; set; }

        [NotMapped]
        public bool IsUserTeam { get; set; }

        public Hackathon Hackathon { get; set; }
        public ICollection<HackathonMembers> HackathonMembers { get; set; }
        public ICollection<HackathonTeamOfferings> HackathonTeamOfferings { get; set; }
        public ICollection<HackathonVotingVote> HackathonVotingVote { get; set; }
        public ICollection<HackathonVotingTeams> HackathonVotingTeams { get; set; }
    }
}
