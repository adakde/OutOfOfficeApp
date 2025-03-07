﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OutOfOfficeApp.Models;
using OutOfOfficeApp.Data;

public class ProjectsController : Controller
{
    private readonly OutOfOfficeAppDbContext _context;

    public ProjectsController(OutOfOfficeAppDbContext context)
    {
        _context = context;
    }

    // GET: Projects
    public async Task<IActionResult> Index()
    {
        var projects = _context.Projects.Include(p => p.ProjectManager);
        return View(await projects.ToListAsync());
    }

    // GET: Projects/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var project = await _context.Projects
            .Include(p => p.ProjectManager)
            .FirstOrDefaultAsync(m => m.ID == id);
        if (project == null)
        {
            return NotFound();
        }

        return View(project);
    }

// GET: Projects/Create
public IActionResult Create()
{
    ViewData["ProjectManagerId"] = new SelectList(_context.Employees, "ID", "FullName");
    return View();
}

// POST: Projects/Create
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create([Bind("ID,ProjectType,StartDate,EndDate,ProjectManagerId,Comment,Status")] Project project)
{
    if (ModelState.IsValid)
    {
        _context.Add(project);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    ViewData["ProjectManagerId"] = new SelectList(_context.Employees, "ID", "FullName", project.ProjectManagerId);
    return View(project);
}

    // GET: Projects/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var project = await _context.Projects.FindAsync(id);
        if (project == null)
        {
            return NotFound();
        }
        ViewData["ProjectManagerId"] = new SelectList(_context.Employees, "ID", "FullName", project.ProjectManagerId);
        return View(project);
    }

    // POST: Projects/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("ID,ProjectType,StartDate,EndDate,ProjectManagerId,Comment,Status")] Project project)
    {
        if (id != project.ID)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(project);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(project.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        ViewData["ProjectManagerId"] = new SelectList(_context.Employees, "ID", "FullName", project.ProjectManagerId);
        return View(project);
    }

    // GET: Projects/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var project = await _context.Projects
            .Include(p => p.ProjectManager)
            .FirstOrDefaultAsync(m => m.ID == id);
        if (project == null)
        {
            return NotFound();
        }

        return View(project);
    }

    // POST: Projects/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var project = await _context.Projects.FindAsync(id);
        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ProjectExists(int id)
    {
        return _context.Projects.Any(e => e.ID == id);
    }
}
