using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Teamwork_Hackathon.Models
{
    public partial class HackathonTeamOfferings
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        [MaxLength(500)]
        public string Text { get; set; }

        public HackathonTeam Team { get; set; }
    }
}
