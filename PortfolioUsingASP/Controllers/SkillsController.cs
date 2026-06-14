using Microsoft.AspNetCore.Mvc;
using PortfolioUsingASP.Data;
using PortfolioUsingASP.Entities;

namespace PortfolioUsingASP.Controllers
{
    public class SkillsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SkillsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Admin") != "true")
            {
                return RedirectToAction("Login", "Admin");
            }
            var skills = _context.Skills.ToList();
            return View(skills);
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
        public IActionResult Create(Skills skill)
        {
            if (HttpContext.Session.GetString("Admin") != "true")
            {
                return RedirectToAction("Login", "Admin");
            }
            _context.Skills.Add(skill);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            if (HttpContext.Session.GetString("Admin") != "true")
            {
                return RedirectToAction("Login", "Admin");
            }
            var skill = _context.Skills.FirstOrDefault(s => s.SkillId == id);

            if (skill == null)
                return NotFound();

            return View(skill);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Skills skill)
        {
            if (HttpContext.Session.GetString("Admin") != "true")
            {
                return RedirectToAction("Login", "Admin");
            }
            var existing = _context.Skills.FirstOrDefault(s => s.SkillId == skill.SkillId);

            if (existing != null)
            {
                existing.SkillName = skill.SkillName;
                existing.SkillLevel = skill.SkillLevel;
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
            var skill = _context.Skills.FirstOrDefault(s => s.SkillId == id);

            if (skill == null)
                return NotFound();

            return View(skill);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Skills skill)
        {
            if (HttpContext.Session.GetString("Admin") != "true")
            {
                return RedirectToAction("Login", "Admin");
            }
            var existing = _context.Skills.FirstOrDefault(s => s.SkillId == skill.SkillId);

            if (existing != null)
            {
                _context.Skills.Remove(existing);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
