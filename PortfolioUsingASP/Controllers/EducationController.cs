using Microsoft.AspNetCore.Mvc;
using PortfolioUsingASP.Data;
using PortfolioUsingASP.Entities;

namespace PortfolioUsingASP.Controllers
{
    public class EducationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EducationController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Admin") != "true")
            {
                return RedirectToAction("Login", "Admin");
            }
            var educations = _context.Educations.ToList();
            return View(educations);
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
        public IActionResult Create(Education edu)
        {
            if (HttpContext.Session.GetString("Admin") != "true")
            {
                return RedirectToAction("Login", "Admin");
            }
            _context.Educations.Add(edu);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            if (HttpContext.Session.GetString("Admin") != "true")
            {
                return RedirectToAction("Login", "Admin");
            }
            var edu = _context.Educations.FirstOrDefault(e => e.EduId == id);

            if (edu == null)
                return NotFound();

            return View(edu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Education edu)
        {
            if (HttpContext.Session.GetString("Admin") != "true")
            {
                return RedirectToAction("Login", "Admin");
            }
            var existing = _context.Educations.FirstOrDefault(e => e.EduId == edu.EduId);

            if (existing != null)
            {
                existing.EduLevel = edu.EduLevel;
                existing.SubjectOrGroup = edu.SubjectOrGroup;
                existing.Result = edu.Result;
                existing.PassingYear = edu.PassingYear;
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
            var edu = _context.Educations.FirstOrDefault(e => e.EduId == id);

            if (edu == null)
                return NotFound();

            return View(edu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Education edu)
        {
            if (HttpContext.Session.GetString("Admin") != "true")
            {
                return RedirectToAction("Login", "Admin");
            }
            var existing = _context.Educations.FirstOrDefault(e => e.EduId == edu.EduId);

            if (existing != null)
            {
                _context.Educations.Remove(existing);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}