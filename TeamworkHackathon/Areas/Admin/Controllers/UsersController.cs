using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Teamwork_Hackathon.Areas.Admin.Models;
using Teamwork_Hackathon.Models;

namespace Teamwork_Hackathon.Areas.Admin.Controllers
{

    [Authorize(Roles = "admin")]
    public class UsersController : Controller
    {
        private readonly teamwork_hackathonContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public UsersController(teamwork_hackathonContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Admin/Users
        public async Task<IActionResult> Index()
        {
            var currentuser = await _userManager.GetUserAsync(User);
            var model = new UserListDTO(currentuser);
            model.Users = await _context.AspNetUsers.ToListAsync();

            return View(model);
        }


        private bool AspNetUsersExists(string id)
        {
            return _context.AspNetUsers.Any(e => e.Id == id);
        }
    }
}
