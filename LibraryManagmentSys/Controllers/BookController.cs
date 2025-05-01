using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BLL__Buisness_Logic_Layer_.Services.BookServices;
using BLL__Buisness_Logic_Layer_.Dtos.BookDto;
using System.Threading.Tasks;
using DAL__Data_Access_Layer_.Repo.GenericRepo; // Assuming repos for Author and Category
using DAL__Data_Access_Layer_.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace YourNamespace.Controllers
{
    [Authorize]
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IGenericRepo<Author> _authorRepo;
        private readonly IGenericRepo<Category> _categoryRepo;

        public BookController(
            IBookService bookService,
            IGenericRepo<Author> authorRepo,
            IGenericRepo<Category> categoryRepo)
        {
            _bookService = bookService;
            _authorRepo = authorRepo;
            _categoryRepo = categoryRepo;
        }

        private async Task PopulateDropdowns(BookCreateDto dto)
        {
            var categories = await _categoryRepo.GetAllAsync();
            dto.Categories = await categories
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                })
                .ToListAsync();

            // If you need Authors dropdown as well
            var authors = await _authorRepo.GetAllAsync();
            ViewBag.Authors = new SelectList(authors, "Id", "Name"); // ✅ كده تمام

        }

        private async Task PopulateDropdowns(BookUpdateDto dto)
        {
            var categories = await _categoryRepo.GetAllAsync();
            dto.Categories = await categories
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                })
                .ToListAsync();

            // If you need Authors dropdown as well
            var authors = await _authorRepo.GetAllAsync();
            ViewBag.Authors = new SelectList(authors, "Id", "Name"); // ✅ كده تمام

        }

        public async Task<IActionResult> Index()
        {
            var books = await _bookService.GetAllBooksAsync();
            return View(books);
        }

        public async Task<IActionResult> Details(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null) return NotFound();
            return View(book);
        }

        public async Task<IActionResult> Create()
        {
            var dto = new BookCreateDto();
            await PopulateDropdowns(dto);
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                await PopulateDropdowns(dto);
                return View(dto);
            }

            try
            {
                await _bookService.CreateBookAsync(dto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while creating the book: {ex.Message}");
                await PopulateDropdowns(dto);
                return View(dto);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null) return NotFound();

            var model = new BookUpdateDto
            {
                Id = id, 
                Title = book.Title,
                ISBN = book.ISBN,
                ExistingCoverPath = book.Cover, 
                AuthorId = book.AuthorId , 
                CategoryId = book.CategoryId  ,
                Description = book.Description
            };

            await PopulateDropdowns(model);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BookUpdateDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("The book ID in the URL does not match the DTO ID.");
            }

            if (!ModelState.IsValid)
            {
                await PopulateDropdowns(dto);
                return View(dto);
            }

            try
            {
                var updatedBook = await _bookService.UpdateBookAsync(id, dto);
                if (updatedBook == null) return NotFound();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while updating the book: {ex.Message}");
                await PopulateDropdowns(dto);
                return View(dto);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null) return NotFound();

            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _bookService.DeleteBookAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while deleting the book: {ex.Message}");
                var book = await _bookService.GetBookByIdAsync(id);
                if (book == null) return NotFound();
                return View(book);
            }
        }

        [HttpDelete]
        [Route("Book/Delete/{id}")]
        public async Task<IActionResult> DeleteAjax(int id)
        {
            try
            {
                await _bookService.DeleteBookAsync(id);
                return Json(new { success = true, message = "The book has been deleted." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"An error occurred while deleting the book: {ex.Message}" });
            }
        }
    }
}