using LapShop.Ef.Identity;
using LapShop.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LapShop.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public UserController(UserManager<ApplicationUser> userManager,
                              SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            if (_signInManager.IsSignedIn(User))
                return Redirect("~/");
            UserModelLogin modelLogin = new()
            {
                ReturnUrl = returnUrl
            };
            return View(modelLogin);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserModelLogin userModelLogin)
        {
            if (!ModelState.IsValid)
                return View("Login", userModelLogin);

            ApplicationUser applicationUser = new()
            {
                Email = userModelLogin.Email,
                UserName = userModelLogin.Email
            };

            var result = await _signInManager.PasswordSignInAsync(applicationUser.Email, userModelLogin.Password, true, true);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(applicationUser.Email);
                var userRoles = await _userManager.GetRolesAsync(user);
                if (userRoles.FirstOrDefault() == "admin")
                    return Redirect("/admin");
                else if (string.IsNullOrEmpty(userModelLogin.ReturnUrl))
                    return Redirect("~/");
                else
                    return Redirect(userModelLogin.ReturnUrl);
            }
            else
            {
                return View("Login", userModelLogin);
            }

        }

        [HttpGet]
        public IActionResult Register()
        {
            if (_signInManager.IsSignedIn(User))
                return Redirect("~/");
            return View(new UserModelRegister());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserModelRegister userModel)
        {
            if (!ModelState.IsValid)
                return View("Register", userModel);

            ApplicationUser applicationUser = new()
            {
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                Email = userModel.Email,
                UserName = userModel.Email
            };

            var result = await _userManager.CreateAsync(applicationUser, userModel.Password);

            if (result.Succeeded)
            {
                var myUser = await _userManager.FindByEmailAsync(applicationUser.Email);
                await _userManager.AddToRoleAsync(myUser, "customer");
                return View("Login");
            }
            else
            {
                return View("Register", userModel);
            }

        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("~/");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
