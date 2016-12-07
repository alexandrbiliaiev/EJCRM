using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EuroJobsCrm.Dto;
using EuroJobsCrm.Models;
using EuroJobsCrm.Models.AccountViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace EuroJobsCrm.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("api/Users/GetInternalUsers")]
        public async Task<IList<UserDto>> GetUsers()
        {
            var users = await _userManager.GetUsersInRoleAsync("Normal user");

            return users.Select(u => new UserDto
            {
                Id = u.Id,
                Email = u.Email
            }).ToList();
        }

        [HttpPost]
        [Route("api/Users/AddNormalUser")]
        public async Task<UserDto> AddNormalUser([FromBody] UserDto user)
        {
            ApplicationUser normalUser = new ApplicationUser
            {
                Email = user.Email,
                UserName = user.Email
            };

            var result = await _userManager.CreateAsync(normalUser, user.Password);
            await _userManager.AddToRoleAsync(normalUser, "Normal user");
            return new UserDto
            {
                Id = normalUser.Id,
                Email = normalUser.Email,
                Password = string.Empty
            };
        }
        

        [HttpGet]
        [Route("api/Users/SetupRoles")]
        public async Task<bool> SetupRoles()
        {
            var superAdminRole = await _roleManager.FindByNameAsync("Super Admin");
            if (superAdminRole == null)
            {
                superAdminRole = new IdentityRole("Super Admin");
                await _roleManager.CreateAsync(superAdminRole);
            }

            var adminRole = await _roleManager.FindByNameAsync("Admin");
            if (adminRole == null)
            {
                adminRole = new IdentityRole("Admin");
                await _roleManager.CreateAsync(adminRole);
            }

            var normalRole = await _roleManager.FindByNameAsync("Normal user");
            if (normalRole == null)
            {
                normalRole = new IdentityRole("Normal user");
                await _roleManager.CreateAsync(normalRole);
            }

            var advancedRole = await _roleManager.FindByNameAsync("Advanced user");
            if (advancedRole == null)
            {
                advancedRole = new IdentityRole("Advanced user");
                await _roleManager.CreateAsync(advancedRole);
            }

            var guestRole = await _roleManager.FindByNameAsync("Guest");
            if (guestRole == null)
            {
                guestRole = new IdentityRole("Guest");
                await _roleManager.CreateAsync(guestRole);
            }

            var user = await _userManager.FindByIdAsync(User.GetUserId());

            if (!await _userManager.IsInRoleAsync(user, superAdminRole.Name))
            {
                await _userManager.AddToRoleAsync(user, superAdminRole.Name);
            }

            return true;
        }
    }
}