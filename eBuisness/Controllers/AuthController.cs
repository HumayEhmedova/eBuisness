using Core;
using eBuisness.Helper;
using eBuisness.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace eBuisness.Controllers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<AppUser> _signIn;
        private readonly UserManager<AppUser> _userManager;
        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signIn)
        {
            _userManager = userManager;
            _signIn = signIn;
        }
        //register get
        public IActionResult Register()
        {
            return View();
        }
        //regsiter post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) return View(registerVM);
            AppUser appUser = new AppUser
            {
                Fullname = registerVM.Fullname,
                UserName = registerVM.Username,
                Email = registerVM.Email,
            };
            var identityResult = await _userManager.CreateAsync(appUser, registerVM.Password);
            if (!identityResult.Succeeded)
            {
                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(registerVM);
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Login(LoginVM loginVM)
        {
            var user = await _userManager.FindByNameAsync(loginVM.Username);
            if(user == null)
            {
                ModelState.AddModelError("", "username or passsword is incorrect");
                return View(loginVM);
            }
            _signIn.PasswordSignInAsync(user,loginVM.Password,.)
            return RedirectToAction();
        }
    }
}
