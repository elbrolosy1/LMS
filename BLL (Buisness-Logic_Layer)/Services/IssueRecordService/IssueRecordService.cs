using BLL__Buisness_Logic_Layer_.Dtos.IssueRecordDto;
using BLL__Buisness_Logic_Layer_.Services.IssueRecordService;
using DAL__Data_Access_Layer_.Models;
using DAL__Data_Access_Layer_.Repo.GenericRepo;
using Microsoft.EntityFrameworkCore;

public class IssueRecordService : IIssueRecordService
{
    private readonly IGenericRepo<IssueRecord> _issueRecordRepo;

    public IssueRecordService(IGenericRepo<IssueRecord> issueRecordRepo)
    {
        _issueRecordRepo = issueRecordRepo;
    }

    public async Task<IEnumerable<ReadIssueRecord>> GetAllAsync()
    {
        var query = await _issueRecordRepo.GetAllAsync();
        var issueRecords = await query
            .Include(ir => ir.Book)
            .Include(ir => ir.User)
            .ToListAsync();

        var groupedRecords = issueRecords
            .GroupBy(ir => new { ir.UserId, ir.IssueDate, ir.User })
            .Select(g => new ReadIssueRecord
            {
                Id = g.First().Id,
                UserId = g.Key.UserId,
                UserFullName = $"{g.Key.User?.FirstName} {g.Key.User?.LastName}",
                IssueDate = g.Key.IssueDate,
                ReturnDate = g.First().ReturnDate,
                BookTitles = g.Select(ir => ir.Book?.Title ?? "Unknown").ToList(),
                BookIds = g.Select(ir => ir.BookId).ToList()
            });

        return groupedRecords;
    }

    public async Task<ReadIssueRecord?> GetByIdAsync(int id)
    {
        var mainRecord = await _issueRecordRepo.GetByIdAsync(id);
        if (mainRecord == null)
            return null;

        var query = await _issueRecordRepo.GetAllAsync();
        var relatedRecords = await query
            .Include(ir => ir.Book)
            .Include(ir => ir.User)
            .Where(ir => ir.UserId == mainRecord.UserId &&
                        ir.IssueDate == mainRecord.IssueDate)
            .ToListAsync();

        return new ReadIssueRecord
        {
            Id = mainRecord.Id,
            UserId = mainRecord.UserId,
            UserFullName = $"{mainRecord.User?.FirstName} {mainRecord.User?.LastName}",
            IssueDate = mainRecord.IssueDate,
            ReturnDate = mainRecord.ReturnDate,
            BookTitles = relatedRecords.Select(ir => ir.Book?.Title ?? "Unknown").ToList(),
            BookIds = relatedRecords.Select(ir => ir.BookId).ToList(),
        };
    }
    public async Task<bool> AddAsync(CreateIssueRecord dto)
    {
        foreach (var bookId in dto.BookId)
        {
            var newRecord = new IssueRecord
            {
                BookId = bookId,
                UserId = dto.UserId,
                IssueDate = dto.IssueDate,
                ReturnDate = dto.ReturnDate
            };
            await _issueRecordRepo.AddAsync(newRecord);
        }
        await _issueRecordRepo.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateAsync(int id, UpdateIssueRecord dto)
    {
        if (dto == null) throw new ArgumentNullException(nameof(dto));

        // 1. الحصول على السجلات الحالية
        var mainRecord = await _issueRecordRepo.GetByIdAsync(id);
        if (mainRecord == null) return false;

        var existingRecords = (await _issueRecordRepo.GetAllAsync())
            .Where(ir => ir.UserId == mainRecord.UserId &&
                        ir.IssueDate == mainRecord.IssueDate)
            .ToList();

        // 2. حفظ البيانات القديمة للاسترجاع في حالة الخطأ
        var oldBookIds = existingRecords.Select(ir => ir.BookId).ToList();
        var oldIssueDate = mainRecord.IssueDate;
        var oldReturnDate = mainRecord.ReturnDate;

        try
        {
            // 3. حذف السجلات القديمة
            foreach (var record in existingRecords)
            {
                await _issueRecordRepo.DeleteAsync(record.Id);
            }

            // 4. إضافة السجلات الجديدة
            foreach (var bookId in dto.BookIds.Distinct())
            {
                await _issueRecordRepo.AddAsync(new IssueRecord
                {
                    BookId = bookId,
                    UserId = dto.UserId,
                    IssueDate = dto.IssueDate,
                    ReturnDate = dto.ReturnDate
                });
            }

            await _issueRecordRepo.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            // 5. استرجاع البيانات القديمة في حالة الخطأ
            foreach (var record in existingRecords)
            {
                await _issueRecordRepo.AddAsync(new IssueRecord
                {
                    BookId = record.BookId,
                    UserId = record.UserId,
                    IssueDate = oldIssueDate,
                    ReturnDate = oldReturnDate
                });
            }

            await _issueRecordRepo.SaveChangesAsync();
            throw new Exception("Failed to update issue record.", ex);
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var record = await _issueRecordRepo.GetByIdAsync(id);
        if (record == null)
            return false;

        await _issueRecordRepo.DeleteAsync(id);
        await _issueRecordRepo.SaveChangesAsync();
        return true;
    }
}
