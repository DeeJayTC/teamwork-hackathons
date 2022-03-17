using System;
using System.Collections.Generic;

namespace Teamwork_Hackathon.Models
{
    public partial class HackathonMembers
    {
        public int Id { get; set; }
        public int? TeamId { get; set; }
        public string UserId { get; set; }
        public string DisplayName { get; set; }
        public string ProfileImage { get; set; }

        public HackathonTeam Team { get; set; }
        public AspNetUsers User { get; set; }
    }
}
