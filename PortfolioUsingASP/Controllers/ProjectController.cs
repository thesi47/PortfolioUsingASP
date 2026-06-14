using Microsoft.AspNetCore.Mvc;
using PortfolioUsingASP.Data;
using PortfolioUsingASP.Entities;

namespace PortfolioUsingASP.Controllers
{
    public class ProjectController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Admin") != "true")
            {
                return RedirectToAction("Login", "Admin");
            }
            var projects = _context.Projects.ToList();
            return View(projects);
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
            _context.Projects.Add(project);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            if (HttpContext.Session.GetString("Admin") != "true")
            {
                return RedirectToAction("Login", "Admin");
            }
            var project = _context.Projects.FirstOrDefault(p => p.ProjectId == id);

            if (project == null)
                return NotFound();

            return View(project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Project project)
        {
            if (HttpContext.Session.GetString("Admin") != "true")
            {
                return RedirectToAction("Login", "Admin");
            }
            var existing = _context.Projects.FirstOrDefault(p => p.ProjectId == project.ProjectId);

            if (existing != null)
            {
                existing.ProjectName = project.ProjectName;
                existing.ProjectDescription = project.ProjectDescription;
                existing.ProjectSkills = project.ProjectSkills;
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
            var project = _context.Projects.FirstOrDefault(p => p.ProjectId == id);

            if (project == null)
                return NotFound();

            return View(project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Project project)
        {
            if (HttpContext.Session.GetString("Admin") != "true")
            {
                return RedirectToAction("Login", "Admin");
            }
            var existing = _context.Projects.FirstOrDefault(p => p.ProjectId == project.ProjectId);

            if (existing != null)
            {
                _context.Projects.Remove(existing);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}