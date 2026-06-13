using Microsoft.AspNetCore.Mvc;
using PortfolioUsingASP.Models;
using System.Collections.Generic;
using System.Linq;

namespace PortfolioUsingASP.Controllers
{
    public class ExperienceController : Controller
    {
        private static List<Experience> experienceList = new List<Experience>();

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Admin") != "true")
            {
                return RedirectToAction("Login", "Admin");
            }
            return View(experienceList);
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
            exp.JobId = experienceList.Count + 1;

            experienceList.Add(exp);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            if (HttpContext.Session.GetString("Admin") != "true")
            {
                return RedirectToAction("Login", "Admin");
            }
            var exp = experienceList.FirstOrDefault(e => e.JobId == id);

            if (exp == null)
                return NotFound();

            return View(exp);
        }

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

        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("Admin") != "true")
            {
                return RedirectToAction("Login", "Admin");
            }
            var exp = experienceList.FirstOrDefault(e => e.JobId == id);

            if (exp == null)
                return NotFound();

            return View(exp);
        }

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