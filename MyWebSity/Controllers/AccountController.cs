using Microsoft.AspNetCore.Mvc;
using MyWebSity.Models;
using MyWebSity.Services;

namespace MyWebSity.Controllers
{
    public class AccountController : Controller
    {
        private readonly ISqlUserService _sqlUserService;

        public AccountController(ISqlUserService sqlUserService)
        {
            _sqlUserService = sqlUserService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                if (_sqlUserService.ValidateUser(model.Email, model.Password))
                {
                    HttpContext.Session.SetString("CurrentUser", model.Email);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("CurrentUser");
            return RedirectToAction("Index", "Home");
        }
    }
}
