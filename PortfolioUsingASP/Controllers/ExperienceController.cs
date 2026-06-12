using Microsoft.AspNetCore.Mvc;
using PortfolioUsingASP.Models;
using System.Collections.Generic;
using System.Linq;

namespace PortfolioUsingASP.Controllers
{
    public class ExperienceController : Controller
    {
        // IN-MEMORY LIST
        private static List<Experience> experienceList = new List<Experience>();

        // LIST
        public IActionResult Index()
        {
            return View(experienceList);
        }

        // CREATE - GET
        public IActionResult Create()
        {
            return View();
        }

        // CREATE - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Experience exp)
        {
            exp.JobId = experienceList.Count + 1;

            experienceList.Add(exp);
            return RedirectToAction("Index");
        }

        // EDIT - GET
        public IActionResult Edit(int id)
        {
            var exp = experienceList.FirstOrDefault(e => e.JobId == id);

            if (exp == null)
                return NotFound();

            return View(exp);
        }

        // EDIT - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Experience exp)
        {
            var existing = experienceList.FirstOrDefault(e => e.JobId == exp.JobId);

            if (existing != null)
            {
                existing.JobTitle = exp.JobTitle;
                existing.JobDescription = exp.JobDescription;
                existing.CompanyName = exp.CompanyName;
                existing.StartDate = exp.StartDate;
                existing.EndDate = exp.EndDate;
            }

            return RedirectToAction("Index");
        }

        // DELETE - GET
        public IActionResult Delete(int id)
        {
            var exp = experienceList.FirstOrDefault(e => e.JobId == id);

            if (exp == null)
                return NotFound();

            return View(exp);
        }

        // DELETE - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Experience exp)
        {
            var existing = experienceList.FirstOrDefault(e => e.JobId == exp.JobId);

            if (existing != null)
            {
                experienceList.Remove(existing);
            }

            return RedirectToAction("Index");
        }
    }
}