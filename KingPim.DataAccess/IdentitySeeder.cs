using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingPim.DataAccess
{
    public class IdentitySeeder : IIdentitySeeder
    {
        private const string _username = "AdamBengtsson";
        private const string _password = "admin";

        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentitySeeder(ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<bool> CreateRolesAndAdminUserIfEmpty()
        {
            if (!_context.Roles.Any())
            {
                var roleAdministrator = new IdentityRole
                {
                    Name = "Administrator"
                };
                await _roleManager.CreateAsync(roleAdministrator);

                var rolePublisher = new IdentityRole
                {
                    Name = "Publisher"
                };
                await _roleManager.CreateAsync(rolePublisher);

                var roleEditor = new IdentityRole
                {
                    Name = "Editor"
                };
                await _roleManager.CreateAsync(roleEditor);
            }

            if (!_context.Users.Any(u => u.UserName == _username))
            {
                var user = new IdentityUser
                {
                    UserName = _username,
                    Email = "info@adambengtsson.se",
                    EmailConfirmed = true
                };

                await _userManager.CreateAsync(user, _password);
                await _userManager.AddToRoleAsync(user, "Administrator");
            }
            return true;
        }
    }
}
