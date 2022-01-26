using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tebnabawe.Application.Authentication;
using Tebnabawe.Application.Authentication.Dto;
using Tebnabawe.Data;
using Tebnabawe.Data.Models;

namespace Tebnabawe_API.Controllers
{
    [Authorize(Roles = UserRoleModel.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly TebnabaweContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(TebnabaweContext context, UserManager<ApplicationUser> userManager)
        {
            this._context = context;
            this._userManager = userManager;
        }
        [HttpGet("GetAllvistores")]
        public async Task<ActionResult<IEnumerable<UserDetails>>> GetAllVistores()
        {
            List<UserDetails> users = new List<UserDetails>();
            var roleUserId = _context.Roles.FirstOrDefault(r => r.Name == "مستخدم").Id;
            foreach (var item in _context.UserRoles.ToList())
            {
                if (item.RoleId == roleUserId)
                {
                    UserDetails user = new UserDetails();
                    var applicationUser = await _context.Users.FindAsync(item.UserId);
                    user.FirstName = applicationUser.FirstName;
                    user.LastName = applicationUser.LastName;
                    user.UserName = applicationUser.UserName;
                    user.Email = applicationUser.Email;
                    user.ConfirmedEmail = applicationUser.EmailConfirmed;
                    user.Role = "مستخدم";
                    users.Add(user);
                }
            }
            return users;
        }
        [HttpGet("GetAllVistoresPagination/{pageSize},{pageNumber}")]
        public async Task<IActionResult> GetAllVistoresPaginationAsync(int pageSize, int pageNumber)
        {
            pageSize = (pageSize <= 0) ? 10 : pageSize;
            pageNumber = (pageNumber < 1) ? 0 : pageNumber - 1;
            List<UserDetails> users = new List<UserDetails>();
            var roleUserId = _context.Roles.FirstOrDefault(r => r.Name == "مستخدم").Id;
            foreach (var item in _context.UserRoles.ToList())
            {
                if (item.RoleId == roleUserId)
                {
                    UserDetails user = new UserDetails();
                    var applicationUser = await _context.Users.FindAsync(item.UserId);
                    user.FirstName = applicationUser.FirstName;
                    user.LastName = applicationUser.LastName;
                    user.UserName = applicationUser.UserName;
                    user.Email = applicationUser.Email;
                    user.ConfirmedEmail = applicationUser.EmailConfirmed;
                    user.Role = "مستخدم";
                    users.Add(user);
                }
            }
            var result = users.Skip(pageNumber * pageSize).Take(pageSize).ToList();
            return Ok(result);
        }
        [HttpGet("VisitoresCount")]
        public async Task<IActionResult> GetVisitoresCountAsync()
        {
            List<ApplicationUser> users = new List<ApplicationUser>();
            var roleUserId = _context.Roles.FirstOrDefault(r => r.Name == "مستخدم").Id;
            foreach (var item in _context.UserRoles.ToList())
            {
                if (item.RoleId == roleUserId)
                {
                    users.Add(await _context.Users.FindAsync(item.UserId));
                }
            }
            return Ok(users.Count);
        }
        [HttpGet("GetAllUsersPagination/{pageSize},{pageNumber}")]
        public async Task<IActionResult> GetAllUsersPaginationAsync(int pageSize, int pageNumber)
        {
            pageSize = (pageSize <= 0) ? 10 : pageSize;
            pageNumber = (pageNumber < 1) ? 0 : pageNumber - 1;
            List<UserDetails> users = new List<UserDetails>();
            var rolesAdminId = _context.Roles.FirstOrDefault(r => r.Name == "أدمن").Id;
            var roleId = _context.Roles.FirstOrDefault(r => r.Name == "مشرف").Id;
            foreach (var item in _context.UserRoles.ToList())
            {
                if (item.RoleId == roleId || item.RoleId == rolesAdminId)
                {
                    UserDetails user = new UserDetails();
                    var applicationUser = await _context.Users.FindAsync(item.UserId);
                    user.FirstName = applicationUser.FirstName;
                    user.LastName = applicationUser.LastName;
                    user.UserName = applicationUser.UserName;
                    user.Email = applicationUser.Email;
                    user.ConfirmedEmail = applicationUser.EmailConfirmed;
                    if (item.RoleId == roleId)
                        user.Role = "مشرف";
                    else
                        user.Role = "أدمن";
                    users.Add(user);
                }

            }
            var result = users.Skip(pageNumber * pageSize).Take(pageSize).ToList();
            return Ok(result);
        }
        [HttpGet("UsersCount")]
        public async Task<IActionResult> GetUsersCountAsync()
        {
            List<ApplicationUser> users = new List<ApplicationUser>();
            var rolesAdminId = _context.Roles.FirstOrDefault(r => r.Name == "أدمن").Id;
            var roleId = _context.Roles.FirstOrDefault(r => r.Name == "مشرف").Id;
            foreach (var item in _context.UserRoles.ToList())
            {
                if (item.RoleId == roleId || item.RoleId == rolesAdminId)
                {
                    users.Add(await _context.Users.FindAsync(item.UserId));
                }
            }
            return Ok(users.Count);
        }
        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<IEnumerable<UserDetails>>> GetAllUsers()
        {
            List<UserDetails> users = new List<UserDetails>();
            var rolesAdminId = _context.Roles.FirstOrDefault(r => r.Name == "أدمن").Id;
            var roleId = _context.Roles.FirstOrDefault(r => r.Name == "مشرف").Id;
            foreach (var item in _context.UserRoles.ToList())
            {
                if (item.RoleId == roleId || item.RoleId == rolesAdminId)
                {
                    UserDetails user = new UserDetails();
                    var applicationUser = await _context.Users.FindAsync(item.UserId);
                    user.FirstName = applicationUser.FirstName;
                    user.LastName = applicationUser.LastName;
                    user.UserName = applicationUser.UserName;
                    user.Email = applicationUser.Email;
                    user.ConfirmedEmail = applicationUser.EmailConfirmed;
                    if (item.RoleId == roleId)
                        user.Role = "مشرف";
                    else
                        user.Role = "أدمن";
                    users.Add(user);
                }

            }
            return users;
        }
      
        [HttpPut("UpdateRole/{Role}")]
        public async Task<ActionResult<UserDetails>> UpdateRole(UserDetails user, string Role)
        {
            var currentUser = await _userManager.FindByNameAsync(user.UserName);
            await _userManager.RemoveFromRoleAsync(currentUser, user.Role);
            await _userManager.AddToRoleAsync(currentUser, Role);
            user.Role = Role;

            return Ok(user);
        }
        [HttpPut("AddRole/{Role}")]
        public async Task<ActionResult<RegisterModel>> AddRole(RegisterModel user, string Role)
        {
            var currentUser = await _userManager.FindByNameAsync(user.UserName);
            await _userManager.AddToRoleAsync(currentUser, Role);

            return Ok(user);
        }

        [HttpPut("UpdateUser/{Role}")]
        public async Task<ActionResult<UserDetails>> UpdateUser(UserDetails user, string Role)
        {
            var currentUser = await _userManager.FindByNameAsync(user.UserName);
            await _userManager.RemoveFromRoleAsync(currentUser, user.Role);
            await _userManager.AddToRoleAsync(currentUser, Role);
            user.Role = Role;
            ApplicationUser newUser = new ApplicationUser();
            newUser.FirstName = user.FirstName;
            newUser.LastName = user.LastName;
            newUser.UserName = user.UserName;
            newUser.Email = user.Email;
            newUser.EmailConfirmed = user.ConfirmedEmail;
            await _userManager.UpdateAsync(newUser);
            return Ok(user);
        }
        [HttpDelete("DeleteUser/{userName}")]
        public async Task<ActionResult<UserDetails>> DeleteUser(string userName)
        {
            var user =await _userManager.FindByNameAsync(userName);
            await _userManager.DeleteAsync(user);
            return Ok(user);
        }
    }   
}
