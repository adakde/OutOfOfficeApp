using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OutOfOfficeApp.Data;
using OutOfOfficeApp.Models;

public class LeaveRequestsController : Controller
{
    private readonly OutOfOfficeAppDbContext _context;

    public LeaveRequestsController(OutOfOfficeAppDbContext context)
    {
        _context = context;
    }

    // GET: LeaveRequests
    public async Task<IActionResult> Index()
    {
        var leaveRequests = _context.LeaveRequests.Include(l => l.Employee);
        return View(await leaveRequests.ToListAsync());
    }

    // GET: LeaveRequests/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var leaveRequest = await _context.LeaveRequests
            .Include(l => l.Employee)
            .FirstOrDefaultAsync(m => m.ID == id);
        if (leaveRequest == null)
        {
            return NotFound();
        }

        return View(leaveRequest);
    }

    // GET: LeaveRequests/Create
    public IActionResult Create()
    {
        ViewData["EmployeeId"] = new SelectList(_context.Employees, "ID", "FullName");
        return View();
    }

    // POST: LeaveRequests/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ID,EmployeeId,StartDate,EndDate,AbsenceReason,Status")] LeaveRequest leaveRequest)
    {
        if (ModelState.IsValid)
        {
            try
            {
                _context.Add(leaveRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Dodaj logowanie błędów tutaj
                ModelState.AddModelError("", $"Error: {ex.Message}");
            }
        }
        ViewData["EmployeeId"] = new SelectList(_context.Employees, "ID", "FullName", leaveRequest.EmployeeId);
        return View(leaveRequest);
    }

    // GET: LeaveRequests/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var leaveRequest = await _context.LeaveRequests.FindAsync(id);
        if (leaveRequest == null)
        {
            return NotFound();
        }
        ViewData["EmployeeId"] = new SelectList(_context.Employees, "ID", "FullName", leaveRequest.EmployeeId);
        return View(leaveRequest);
    }

    // POST: LeaveRequests/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("ID,EmployeeId,StartDate,EndDate,Reason,Status")] LeaveRequest leaveRequest)
    {
        if (id != leaveRequest.ID)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(leaveRequest);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeaveRequestExists(leaveRequest.ID))
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
        ViewData["EmployeeId"] = new SelectList(_context.Employees, "ID", "FullName", leaveRequest.EmployeeId);
        return View(leaveRequest);
    }

    // GET: LeaveRequests/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var leaveRequest = await _context.LeaveRequests
            .Include(l => l.Employee)
            .FirstOrDefaultAsync(m => m.ID == id);
        if (leaveRequest == null)
        {
            return NotFound();
        }

        return View(leaveRequest);
    }

    // POST: LeaveRequests/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var leaveRequest = await _context.LeaveRequests.FindAsync(id);
        if (leaveRequest == null)
        {
            return NotFound();
        }

        _context.LeaveRequests.Remove(leaveRequest);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool LeaveRequestExists(int id)
    {
        return _context.LeaveRequests.Any(e => e.ID == id);
    }
}
