using Microsoft.AspNetCore.Mvc;
using PortfolioUsingASP.Models;
using System.Collections.Generic;
using System.Linq;

namespace PortfolioUsingASP.Controllers
{
    public class ProjectController : Controller
    {
        private static List<Project> projectList = new List<Project>();
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Admin") != "true")
            {
                return RedirectToAction("Login", "Admin");
            }
            return View(projectList);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Project project)
        {
            if (HttpContext.Session.GetString("Admin") != "true")
            {
                return RedirectToAction("Login", "Admin");
            }
            project.ProjectId = projectList.Count + 1;
            projectList.Add(project);

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            if (HttpContext.Session.GetString("Admin") != "true")
            {
                return RedirectToAction("Login", "Admin");
            }
            var project = projectList.FirstOrDefault(p => p.ProjectId == id);

            if (project == null)
                return NotFound();

            return View(project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Project project)
        {
            var existing = projectList.FirstOrDefault(p => p.ProjectId == project.ProjectId);

            if (existing != null)
            {
                existing.ProjectName = project.ProjectName;
                existing.ProjectDescription = project.ProjectDescription;
                existing.ProjectSkills = project.ProjectSkills;
            }

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("Admin") != "true")
            {
                return RedirectToAction("Login", "Admin");
            }
            var project = projectList.FirstOrDefault(p => p.ProjectId == id);

            if (project == null)
                return NotFound();

            return View(project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Project project)
        {
            var existing = projectList.FirstOrDefault(p => p.ProjectId == project.ProjectId);

            if (existing != null)
            {
                projectList.Remove(existing);
            }

            return RedirectToAction("Index");
        }
    }
}