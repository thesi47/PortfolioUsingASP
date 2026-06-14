using Microsoft.AspNetCore.Mvc;
using PortfolioUsingASP.Data;
using PortfolioUsingASP.Entities;

namespace PortfolioUsingASP.Controllers
{
    public class ExperienceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExperienceController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Admin") != "true")
            {
                return RedirectToAction("Login", "Admin");
            }
            var experiences = _context.Experiences.ToList();
            return View(experiences);
        }

        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("Admin") != "true")
            {
                return RedirectToAction("Login", "Admin");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Experience exp)
        {
            if (HttpContext.Session.GetString("Admin") != "true")
            {
                return RedirectToAction("Login", "Admin");
            }
            _context.Experiences.Add(exp);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            if (HttpContext.Session.GetString("Admin") != "true")
            {
                return RedirectToAction("Login", "Admin");
            }
            var exp = _context.Experiences.FirstOrDefault(e => e.JobId == id);

            if (exp == null)
                return NotFound();

            return View(exp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Experience exp)
        {
            if (HttpContext.Session.GetString("Admin") != "true")
            {
                return RedirectToAction("Login", "Admin");
            }
            var existing = _context.Experiences.FirstOrDefault(e => e.JobId == exp.JobId);

            if (existing != null)
            {
                existing.JobTitle = exp.JobTitle;
                existing.JobDescription = exp.JobDescription;
                existing.CompanyName = exp.CompanyName;
                existing.StartDate = exp.StartDate;
                existing.EndDate = exp.EndDate;
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("Admin") != "true")
            {
                return RedirectToAction("Login", "Admin");
            }
            var exp = _context.Experiences.FirstOrDefault(e => e.JobId == id);

            if (exp == null)
                return NotFound();

            return View(exp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Experience exp)
        {
            if (HttpContext.Session.GetString("Admin") != "true")
            {
                return RedirectToAction("Login", "Admin");
            }
            var existing = _context.Experiences.FirstOrDefault(e => e.JobId == exp.JobId);

            if (existing != null)
            {
                _context.Experiences.Remove(existing);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}