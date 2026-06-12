using Microsoft.AspNetCore.Mvc;
using PortfolioUsingASP.Models;

namespace PortfolioUsingASP.Controllers
{
    public class EducationController : Controller
    {
        private static List<Education> educationList = new List<Education>();

        public IActionResult Index()
        {
            return View(educationList);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Education edu)
        {
            edu.EduId = educationList.Count + 1;
            educationList.Add(edu);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var edu = educationList.FirstOrDefault(e => e.EduId == id);

            if (edu == null)
                return NotFound();

            return View(edu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Education edu)
        {
            var existing = educationList.FirstOrDefault(e => e.EduId == edu.EduId);

            if (existing != null)
            {
                existing.EduLevel = edu.EduLevel;
                existing.SubjectOrGroup = edu.SubjectOrGroup;
                existing.Result = edu.Result;
                existing.PassingYear = edu.PassingYear;
            }

            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var edu = educationList.FirstOrDefault(e => e.EduId == id);

            if (edu == null)
                return NotFound();

            return View(edu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Education edu)
        {
            var existing = educationList.FirstOrDefault(e => e.EduId == edu.EduId);

            if (existing != null)
            {
                educationList.Remove(existing);
            }

            return RedirectToAction("Index");
        }
    }
}