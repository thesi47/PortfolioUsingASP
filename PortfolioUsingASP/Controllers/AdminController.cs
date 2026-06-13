using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace PortfolioUsingASP.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (username == "admin" && password == "1234")
            {
                HttpContext.Session.SetString("Admin", "true");

                return RedirectToAction("Dashboard");
            }

            ViewBag.Error = "Invalid Username or Password";

            return View();
        }

        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("Admin") != "true")
            {
                return RedirectToAction("Login");
            }

            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Login");
        }
    }
}