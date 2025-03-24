using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskManager_App.Models;

namespace TaskManager_App.Controllers
{
    public class AcountController : Controller
    {
        //private readonly UserManager<User> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        //UserManager<User> userManager,

        public AcountController( SignInManager<ApplicationUser> signInManager)
        {
            //_userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]

        // GET: /Account/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe,lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "TaskItems");
            }

            ModelState.AddModelError("", "Invalid login attempt.");
            return View(model);
        }

        //// GET: /Account/Register
        //public IActionResult Register()
        //{
        //    return View();
        //}

        //// POST: /Account/Register
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Register(RegisterViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //        return View(model);

        //    var user = new IdentityUser { UserName = model.Email, Email = model.Email };
        //    var result = await _userManager.CreateAsync(user, model.Password);

        //    if (result.Succeeded)
        //    {
        //        await _signInManager.SignInAsync(user, isPersistent: false);
        //        return RedirectToAction("Index", "Home");
        //    }

        //    foreach (var error in result.Errors)
        //    {
        //        ModelState.AddModelError("", error.Description);
        //    }

        //    return View(model);
        //}

        // POST: /Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Acount");
        }
    }
}
