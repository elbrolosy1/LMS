using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BLL__Buisness_Logic_Layer_.Services.IssueRecordService;
using BLL__Buisness_Logic_Layer_.Dtos.IssueRecordDto;
using DAL__Data_Access_Layer_.Repo.GenericRepo;
using DAL__Data_Access_Layer_.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class IssueRecordController : Controller
{
    private readonly IIssueRecordService _issueRecordService;
    private readonly IGenericRepo<Book> _bookRepo;
    private readonly UserManager<AppUser> _userManager;

    public IssueRecordController(
        IIssueRecordService issueRecordService,
        IGenericRepo<Book> bookRepo,
        UserManager<AppUser> userManager)
    {
        _issueRecordService = issueRecordService;
        _bookRepo = bookRepo;
        _userManager = userManager;
    }

    // GET: Index
    public async Task<IActionResult> Index()
    {
        var records = await _issueRecordService.GetAllAsync();
        return View(records);
    }

    // GET: Details
    public async Task<IActionResult> Details(int id)
    {
        var record = await _issueRecordService.GetByIdAsync(id);
        if (record == null)
            return NotFound();
        return View(record);
    }

    // GET: Create
    public async Task<IActionResult> Create()
    {
        await LoadDropDowns();
        return View();
    }

    // POST: Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateIssueRecord dto)
    {
        if (!ModelState.IsValid)
        {
            await LoadDropDowns();
            return View(dto);
        }

        await _issueRecordService.AddAsync(dto);
        return RedirectToAction(nameof(Index));
    }

    // GET: Edit
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var record = await _issueRecordService.GetByIdAsync(id);
        if (record == null) return NotFound();

        var dto = new UpdateIssueRecord
        {
            Id = record.Id,
            UserId = record.UserId,
            BookIds = record.BookIds, // تعبئة الكتب الحالية
            IssueDate = record.IssueDate,
            ReturnDate = record.ReturnDate
        };

        await LoadDropDowns(record.BookIds);
        return View(dto);
    }

    // POST: Edit
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, UpdateIssueRecord dto)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.RecordId = id;
            await LoadDropDowns();
            return View(dto);
        }

        // جلب UserId و IssueDate من السجل الأصلي
        var originalRecord = await _issueRecordService.GetByIdAsync(id);
        dto.UserId = originalRecord.UserId;
        dto.IssueDate = originalRecord.IssueDate;

        var success = await _issueRecordService.UpdateAsync(id, dto);
        if (!success)
            return NotFound();

        return RedirectToAction(nameof(Index));
    }

    // GET: Delete
    public async Task<IActionResult> Delete(int id)
    {
        var record = await _issueRecordService.GetByIdAsync(id);
        if (record == null)
            return NotFound();
        return View(record);
    }

    // POST: Delete Confirm
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var success = await _issueRecordService.DeleteAsync(id);
        if (!success)
            return NotFound();

        return RedirectToAction(nameof(Index));
    }

    // Helper to load dropdowns
    private async Task LoadDropDowns()
    {
        var books = await (await _bookRepo.GetAllAsync()).ToListAsync();
        var users = await _userManager.Users.ToListAsync();

        ViewBag.Books = new SelectList(await _bookRepo.GetAllAsync(), "Id", "Title");
        ViewBag.Users = new SelectList(
            users.Select(u => new {
                u.Id,
                FullName = $"{u.FirstName} {u.LastName}"
            }),
            "Id", "FullName"
        );
    }
    private async Task LoadDropDowns(List<int> selectedBookIds = null)
    {
        var books = await _bookRepo.GetAllAsync();
        var users = await _userManager.Users.ToListAsync();

        ViewBag.BookList = new MultiSelectList(books, "Id", "Title", selectedBookIds);
        ViewBag.UserList = new SelectList(
            users.Select(u => new {
                u.Id,
                FullName = $"{u.FirstName} {u.LastName}"
            }),
            "Id", "FullName"
        );
    }
}
