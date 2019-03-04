using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KingPim.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using KingPim.DataAccess;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Web;
using KingPim.Infrastructure;
using Microsoft.Extensions.Options;

namespace KingPim.Web.Controllers
{
    [Authorize]
    public class LoginController : Controller
    {
        private readonly CustomAppSettings _customAppSettings;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public LoginController(IOptions<CustomAppSettings> customAppSettings, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ApplicationDbContext context)
        {
            _customAppSettings = customAppSettings.Value;
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("ListProducts", "Product");
            }
            else
            {
                return View();
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(vm.Email);
                if (user != null)
                {
                    await _signInManager.SignOutAsync();
                    if ((await _signInManager.PasswordSignInAsync(user, vm.Password, false, false)).Succeeded)
                    {
                        return RedirectToAction("ListProducts", "Product");
                    }
                }
            }
            TempData["Info"] = "Du har angett felaktiga inloggningsuppgifter";
            return View("Index", vm);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            TempData["Info"] = "Du har loggat ut";
            return RedirectToAction("Index", "Login");
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult ForgotUserPassword()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotUserPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
                return View();

            var user = await _userManager.FindByEmailAsync(email);

            /* Jag gör så att man får samma meddelande om man uppger tomt e-postfält samt ogiltiga e-postadresser så
               att inte hackare ska kunna gräva fram giltiga e-postadresser */

            if (user == null || !await _userManager.IsEmailConfirmedAsync(user))
            {
                TempData["Info"] = "Vänligen kolla efter nya meddelanden i den e-post du uppgav för att återställa ditt lösenord";
                return RedirectToAction("Index", "Login");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = HttpUtility.UrlEncode(token);

            var callbackurl = Url.Action(
                controller: "Login",
                action: "ResetUserPassword",
                values: new { code = encodedToken },
                protocol: Request.Scheme);

            var client = new SendGridClient(_customAppSettings.SendGridApiKey);

            var msg = new SendGridMessage
            {
                From = new EmailAddress("bead17dd@student.ju.se", "Adam Bengtsson")
            };

            msg.AddTo(user.Email);

            msg.TemplateId = "d5630ad4-db71-46cc-83b3-dd43e494b0c6";

            var substitutions = new Dictionary<string, string>
            {
                {"-name-", user.UserName },
                { "-resetUserPasswordLink-", callbackurl }
            };

            msg.AddSubstitutions(substitutions);

            var response = client.SendEmailAsync(msg).Result;

            TempData["Info"] = "Vänligen kolla efter nya meddelanden i den e-post du uppgav för att återställa ditt lösenord";
            return RedirectToAction("Index", "Login");
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult ResetUserPassword(string code)
        {
            if (code == null)
            {
                TempData["Info"] = "Token-kod saknas";
                return RedirectToAction("Index", "Login");
            }

            var model = new ResetUserPasswordViewModel { Code = code };
            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetUserPassword(ResetUserPasswordViewModel r)
        {
            if (!ModelState.IsValid)
                return View(r);

            var user = await _userManager.FindByEmailAsync(r.Email);
            if (user == null)
            {
                TempData["Info"] = "Du har angett en felaktig e-postadress";
                return RedirectToAction("Index", "Login");
            }

            var decodedToken = HttpUtility.UrlDecode(r.Code);

            var result = await _userManager.ResetPasswordAsync(user, decodedToken, r.Password);

            if (!result.Succeeded)
            {
                TempData["Info"] = "Något har gått fel, kanske har token-koden utgått, klicka på \"Glömt lösenord\" igen";
                return RedirectToAction("Index", "Login");
            }

            if (result.Succeeded)
            {
                TempData["Success"] = "Succé! Ditt lösenord har ändrats";
                return RedirectToAction("Index", "Login");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return View(r);
        }
    }
}