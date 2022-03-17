using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Teamwork_Hackathon.Models
{
    public partial class HackathonSearchMembers
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        [MaxLength(500)]
        public string Text { get; set; }

        public HackathonSearchMembers IdNavigation { get; set; }
        public AspNetUsers User { get; set; }
        public HackathonSearchMembers InverseIdNavigation { get; set; }
    }
}
