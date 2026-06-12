using Microsoft.AspNetCore.Mvc;
using PortfolioUsingASP.Models;
using System.Collections.Generic;
using System.Linq;

namespace PortfolioUsingASP.Controllers
{
    public class ProjectController : Controller
    {
        // IN-MEMORY STORAGE
        private static List<Project> projectList = new List<Project>();

        // LIST
        public IActionResult Index()
        {
            return View(projectList);
        }

        // CREATE - GET
        public IActionResult Create()
        {
            return View();
        }

        // CREATE - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Project project)
        {
            project.ProjectId = projectList.Count + 1;
            projectList.Add(project);

            return RedirectToAction("Index");
        }

        // EDIT - GET
        public IActionResult Edit(int id)
        {
            var project = projectList.FirstOrDefault(p => p.ProjectId == id);

            if (project == null)
                return NotFound();

            return View(project);
        }

        // EDIT - POST
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

        // DELETE - GET
        public IActionResult Delete(int id)
        {
            var project = projectList.FirstOrDefault(p => p.ProjectId == id);

            if (project == null)
                return NotFound();

            return View(project);
        }

        // DELETE - POST
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