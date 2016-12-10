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
        private const string NORMAL_USER_ROLE_NAME = "Normal user";
        private const string ADVANSED_USER_ROLE_NAME = "Advanced user";
        private const string ACCOUNTING_ROLE_NAME = "Accounting";
        private const string SUPER_ADMIN_ROLE_NAME = "Super Admin";
        private const string ADMIN_ROLE_NAME = "Admin";
        private const string GUEST_ROLE_NAME = "Guest";

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
            var users = await _userManager.GetUsersInRoleAsync(NORMAL_USER_ROLE_NAME);

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
            ApplicationUser applicationUser = new ApplicationUser
            {
                Email = user.Email,
                UserName = user.Email
            };

            IdentityResult result = await _userManager.CreateAsync(applicationUser, user.Password);
            if (!result.Succeeded)
            {
                return new UserDto
                {
                    Success = false,
                    ErrorMessage = string.Join("; ", result.Errors.Select(e => e.Description).ToArray())
                };
            }
            await _userManager.AddToRoleAsync(applicationUser, NORMAL_USER_ROLE_NAME);
            return new UserDto
            {
                Id = applicationUser.Id,
                Email = applicationUser.Email,
                Password = string.Empty
            };
        }

        [HttpPost]
        [Route("api/Users/AddAdvancedUser")]
        public async Task<UserDto> AddAdvancedUser([FromBody] UserDto user)
        {
            ApplicationUser applicationUser = new ApplicationUser
            {
                Email = user.Email,
                UserName = user.Email
            };

            IdentityResult result = await _userManager.CreateAsync(applicationUser, user.Password);
            if (!result.Succeeded)
            {
                return new UserDto
                {
                    Success = false,
                    ErrorMessage = string.Join("; ", result.Errors.Select(e => e.Description).ToArray())
                };
            }
            await _userManager.AddToRoleAsync(applicationUser, ADVANSED_USER_ROLE_NAME);
            return new UserDto
            {
                Id = applicationUser.Id,
                Email = applicationUser.Email,
                Password = string.Empty
            };
        }

        [HttpPost]
        [Route("api/Users/AddAccountingUser")]
        public async Task<UserDto> AddAccountingUser([FromBody] UserDto user)
        {
            ApplicationUser applicationUser = new ApplicationUser
            {
                Email = user.Email,
                UserName = user.Email
            };

            IdentityResult result = await _userManager.CreateAsync(applicationUser, user.Password);
            if (!result.Succeeded)
            {
                return new UserDto
                {
                    Success = false,
                    ErrorMessage = string.Join("; ", result.Errors.Select(e => e.Description).ToArray())
                };
            }
            await _userManager.AddToRoleAsync(applicationUser, ACCOUNTING_ROLE_NAME);
            return new UserDto
            {
                Id = applicationUser.Id,
                Email = applicationUser.Email,
                Password = string.Empty
            };
        }

        [HttpPost]
        [Route("api/Users/AddAdmin")]
        public async Task<UserDto> AddAdmin([FromBody] UserDto user)
        {
            ApplicationUser applicationUser = new ApplicationUser
            {
                Email = user.Email,
                UserName = user.Email
            };

            IdentityResult result = await _userManager.CreateAsync(applicationUser, user.Password);
            if (!result.Succeeded)
            {
                return new UserDto
                {
                    Success = false,
                    ErrorMessage = string.Join("; ", result.Errors.Select(e => e.Description).ToArray())
                };
            }

            await _userManager.AddToRoleAsync(applicationUser, ADMIN_ROLE_NAME);
            return new UserDto
            {
                Id = applicationUser.Id,
                Email = applicationUser.Email,
                Password = string.Empty
            };
        }

        [HttpGet]
        [Route("api/Users/SetupRoles")]
        public async Task<bool> SetupRoles()
        {
            var superAdminRole = await _roleManager.FindByNameAsync(SUPER_ADMIN_ROLE_NAME);
            if (superAdminRole == null)
            {
                superAdminRole = new IdentityRole(SUPER_ADMIN_ROLE_NAME);
                await _roleManager.CreateAsync(superAdminRole);
            }

            var adminRole = await _roleManager.FindByNameAsync(ADMIN_ROLE_NAME);
            if (adminRole == null)
            {
                adminRole = new IdentityRole(ADMIN_ROLE_NAME);
                await _roleManager.CreateAsync(adminRole);
            }

            var normalRole = await _roleManager.FindByNameAsync(NORMAL_USER_ROLE_NAME);
            if (normalRole == null)
            {
                normalRole = new IdentityRole(NORMAL_USER_ROLE_NAME);
                await _roleManager.CreateAsync(normalRole);
            }

            var advancedRole = await _roleManager.FindByNameAsync(ADVANSED_USER_ROLE_NAME);
            if (advancedRole == null)
            {
                advancedRole = new IdentityRole(ADVANSED_USER_ROLE_NAME);
                await _roleManager.CreateAsync(advancedRole);
            }

            var guestRole = await _roleManager.FindByNameAsync(GUEST_ROLE_NAME);
            if (guestRole == null)
            {
                guestRole = new IdentityRole(GUEST_ROLE_NAME);
                await _roleManager.CreateAsync(guestRole);
            }

            var accountingRole = await _roleManager.FindByNameAsync(ACCOUNTING_ROLE_NAME);
            if (accountingRole == null)
            {
                accountingRole = new IdentityRole(ACCOUNTING_ROLE_NAME);
                await _roleManager.CreateAsync(accountingRole);
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