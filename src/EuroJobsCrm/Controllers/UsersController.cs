using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Claims;
using System.Threading.Tasks;
using EuroJobsCrm.Dto;
using EuroJobsCrm.Models;
using EuroJobsCrm.Models.AccountViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using MimeKit.Text;

namespace EuroJobsCrm.Controllers
{
    public class UsersController : Controller
    {
        private const string NORMAL_USER_ROLE_NAME = "Normal user";
        private const string ADVANCED_USER_ROLE_NAME = "Advanced user";
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
        public Dictionary<string, List<UserDto>> GetInternalUsers()
        {
            using (var context = new DB_A12601_bielkaContext())
            {
                var users = context.AspNetUserRoles.Where(r=>r.Role.Name != SUPER_ADMIN_ROLE_NAME).Join(context.AspNetUsers,
                        r => r.UserId,
                        u => u.Id,
                        (role, user) => new
                        {
                            RoleName = role.Role.Name,
                            User = user,
                        })
                    .GroupBy(group => group.RoleName)
                    .Select(group => new  { RoleName = group.Key, Users = group })
                    .ToList()
                    .ToDictionary(k => k.RoleName, v => v.Users.Select(us => new UserDto(us.User))
                    .ToList());

                return users;
            }
        }

        [HttpPost]
        [Route("api/Users/AddNormalUser")]
        public async Task<UserDto> AddNormalUser([FromBody] UserDto user)
        {
            return await AddUserWithRole(user, NORMAL_USER_ROLE_NAME);
        }

        [HttpPost]
        [Route("api/Users/AddAdvancedUser")]
        public async Task<UserDto> AddAdvancedUser([FromBody] UserDto user)
        {
            return await AddUserWithRole(user, ADVANCED_USER_ROLE_NAME);
        }

        [HttpPost]
        [Route("api/Users/AddAccountingUser")]
        public async Task<UserDto> AddAccountingUser([FromBody] UserDto user)
        {
            return await AddUserWithRole(user, ACCOUNTING_ROLE_NAME);
        }

        [HttpPost]
        [Route("api/Users/AddAdmin")]
        public async Task<UserDto> AddAdmin([FromBody] UserDto user)
        {
            return await AddUserWithRole(user, ADMIN_ROLE_NAME);
        }

        [HttpPost]
        [Route("api/Users/ResetPassword")]
        public async Task<DataTransferObjectBase> ResetPassword([FromBody] string userId)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return new DataTransferObjectBase
                {
                    Success = false,
                    ErrorMessage = "User is not found"
                };
            }
            string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            string newPassword = GeneratePassword();
            await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
            return new DataTransferObjectBase();
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

            var advancedRole = await _roleManager.FindByNameAsync(ADVANCED_USER_ROLE_NAME);
            if (advancedRole == null)
            {
                advancedRole = new IdentityRole(ADVANCED_USER_ROLE_NAME);
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

        private async Task<UserDto> AddUserWithRole(UserDto user, string roleName)
        {
            try
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
                        ErrorMessage = string.Join("\r\n ", result.Errors.Select(e => e.Description).ToArray())
                    };
                }
                await _userManager.AddToRoleAsync(applicationUser, roleName);
                return new UserDto
                {
                    Id = applicationUser.Id,
                    Email = applicationUser.Email,
                    Password = string.Empty,
                    UserName = applicationUser.UserName
                };
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private string GeneratePassword()
        {
            string passwordBase = Guid.NewGuid().ToString().Substring(0, 10);
            var random = new Random();
            passwordBase += random.Next(100);
            passwordBase += "!@#*&"[random.Next(3)];

            return passwordBase;
        }

    }
}