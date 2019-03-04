using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using KingPim.DataAccess;
using KingPim.Infrastructure;
using KingPim.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KingPim.Web.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserController(UserManager<IdentityUser> userManager, ApplicationDbContext context, RoleManager<IdentityRole> roleManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _context = context;
            _roleManager = roleManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> ListUsers()
        {
            var users = new List<ListUserViewModel>();

            foreach (var user in _userManager.Users.ToList())
            {
                var listUserViewModel = new ListUserViewModel()
                {
                    UserName = user.UserName,
                    Roles = await _userManager.GetRolesAsync(user),
                    Email = user.Email
                };
                users.Add(listUserViewModel);
            }

            return View(users);
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            var u = new UserViewModel()
            {
                Roles = _roleManager.Roles.ToList().OrderBy(x => x.Name).ToSelectList()
            };

            return View(u);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserViewModel u)
        {
            if (!_context.Users.Any(x => x.UserName == u.UserName) || !_context.Users.Any(x => x.Email == u.Email))
            {
                var user = new IdentityUser
                {
                    UserName = u.UserName,
                    Email = u.Email,
                    EmailConfirmed = true
                };

                await _userManager.CreateAsync(user, u.Password);
                await _userManager.AddToRoleAsync(user, u.Role);
                TempData["Success"] = $"Succé! Användare \"{u.UserName}\" har lagts till";
                return RedirectToAction("ListUsers", "User");
            }
            else
            {
                TempData["Info"] = "Du har angett ett användarnamn eller e-postadress som redan existerar";
                return RedirectToAction("ListUsers", "User");
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            await _userManager.DeleteAsync(user);
            TempData["Success"] = $"Succé! Användare \"{userName}\" har tagits bort";
            return RedirectToAction("ListUsers", "User");
        }

        [HttpGet]
        public IActionResult ChangeUserPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangeUserPassword(ChangeUserPasswordViewModel c)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            if (await _userManager.CheckPasswordAsync(user, c.OldPassword))
            {
                await _userManager.ChangePasswordAsync(user, c.OldPassword, c.NewPassword);
                TempData["Success"] = $"Succé! Du har ändrat ditt lösenord";
                return RedirectToAction("ListProducts", "Product");
            }
            else
            {
                TempData["Info"] = "Du har angett ett felaktigt gammalt lösenord";
                return View();
            }
        }
    }
}