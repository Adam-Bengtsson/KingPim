using KingPim.Models;
using KingPim.Repositories;
using KingPim.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KingPim.Web.ViewComponents
{
    public class ShowLoggedInUserViewComponent : ViewComponent
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ShowLoggedInUserViewComponent (IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string Invoke()
        {
            var loggedInUserName = _httpContextAccessor.HttpContext.User.Identity.Name;
            var roles = ((ClaimsIdentity)User.Identity).Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value);
            var rolesCommaSeparatedString = string.Join(",", roles);
            if (loggedInUserName == null)
            { return ""; }
            return ($"{loggedInUserName} ({rolesCommaSeparatedString})");
        }
    }
}