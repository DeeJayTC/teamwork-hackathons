using System;
using System.Collections.Generic;

namespace Teamwork_Hackathon.Models
{
    public partial class AspNetUsers
    {
        public AspNetUsers()
        {
            HackathonMembers = new HashSet<HackathonMembers>();
            HackathonSearchMembers = new HashSet<HackathonSearchMembers>();
        }

        public string Id { get; set; }
        public int AccessFailedCount { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool LockoutEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public string NormalizedEmail { get; set; }
        public string NormalizedUserName { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public string SecurityStamp { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfileImage { get; set; }


        public ICollection<HackathonMembers> HackathonMembers { get; set; }
        public ICollection<HackathonSearchMembers> HackathonSearchMembers { get; set; }
    }
}
