using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Teamwork_Hackathon.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
			public string FirstName { get; set; }
			public string LastName { get; set; }
		    public string ProfileImage { get; set; }


        public int ActiveHackathon { get; set; } = 5;
    }
}
