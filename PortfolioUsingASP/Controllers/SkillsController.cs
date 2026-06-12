using Microsoft.AspNetCore.Mvc;
using PortfolioUsingASP.Models;


namespace PortfolioUsingASP.Controllers
{
    public class SkillsController : Controller
    {
        private static List<Skills> skills = new List<Skills>();

        public IActionResult Index()
        {
            return View(skills);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Skills skill)
        {
            skills.Add(skill);
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var skill = skills.FirstOrDefault(s => s.SkillId == id);

            if (skill == null)
                return NotFound();

            return View(skill);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Skills skill)
        {
            var existing = skills.FirstOrDefault(s => s.SkillId == skill.SkillId);

            if (existing != null)
            {
                existing.SkillName = skill.SkillName;
                existing.SkillLevel = skill.SkillLevel;
            }

            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var skill = skills.FirstOrDefault(s => s.SkillId == id);

            if (skill == null)
                return NotFound();

            return View(skill);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Skills skill)
        {
            var existing = skills.FirstOrDefault(s => s.SkillId == skill.SkillId);

            if (existing != null)
            {
                skills.Remove(existing);
            }

            return RedirectToAction("Index");
        }

    }
}
