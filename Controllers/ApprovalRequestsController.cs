using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OutOfOfficeApp.Data;
using OutOfOfficeApp.Models;

public class ApprovalRequestsController : Controller
{
    private readonly OutOfOfficeAppDbContext _context;

    public ApprovalRequestsController(OutOfOfficeAppDbContext context)
    {
        _context = context;
    }

    // GET: ApprovalRequests
    public async Task<IActionResult> Index()
    {
        return View(await _context.ApprovalRequests.ToListAsync());
    }

    // GET: ApprovalRequests/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var approvalRequest = await _context.ApprovalRequests
            .FirstOrDefaultAsync(m => m.ID == id);
        if (approvalRequest == null)
        {
            return NotFound();
        }

        return View(approvalRequest);
    }

    // GET: ApprovalRequests/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: ApprovalRequests/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ID,ApproverId,LeaveRequestId,Status,Comment")] ApprovalRequest approvalRequest)
    {
        if (ModelState.IsValid)
        {
            _context.Add(approvalRequest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(approvalRequest);
    }

    // GET: ApprovalRequests/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var approvalRequest = await _context.ApprovalRequests.FindAsync(id);
        if (approvalRequest == null)
        {
            return NotFound();
        }
        return View(approvalRequest);
    }

    // POST: ApprovalRequests/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("ID,ApproverId,LeaveRequestId,Status,Comment")] ApprovalRequest approvalRequest)
    {
        if (id != approvalRequest.ID)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(approvalRequest);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApprovalRequestExists(approvalRequest.ID))
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
        return View(approvalRequest);
    }

    // GET: ApprovalRequests/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var approvalRequest = await _context.ApprovalRequests
            .FirstOrDefaultAsync(m => m.ID == id);
        if (approvalRequest == null)
        {
            return NotFound();
        }

        return View(approvalRequest);
    }

    // POST: ApprovalRequests/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var approvalRequest = await _context.ApprovalRequests.FindAsync(id);
        _context.ApprovalRequests.Remove(approvalRequest);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ApprovalRequestExists(int id)
    {
        return _context.ApprovalRequests.Any(e => e.ID == id);
    }
}
