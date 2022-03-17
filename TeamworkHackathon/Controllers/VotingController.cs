using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Teamwork_Hackathon.Models;

namespace Teamwork_Hackathon.Controllers
{
    [Authorize(Roles = "admin")]
    public class VotingController : Controller
    {
        private readonly teamwork_hackathonContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public VotingController(teamwork_hackathonContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

    }

}


