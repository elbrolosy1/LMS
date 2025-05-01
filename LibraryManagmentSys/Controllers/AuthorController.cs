using BLL__Buisness_Logic_Layer_.Dtos.AuthorDto;
using BLL__Buisness_Logic_Layer_.Services.AuthorService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagmentSys.Controllers
{
    [Authorize]
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        // GET: Author
        public async Task<IActionResult> Index()
        {
            var authors = await _authorService.GetAllAsync();
            return View(authors);
        }

        // GET: Author/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var author = await _authorService.GetByIdAsync(id);
            if (author == null)
                return NotFound();

            return View(author);
        }

        // GET: Author/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Author/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAuthorDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            try
            {
                await _authorService.CreateAsync(dto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error while creating author: {ex.Message}");
                return View(dto);
            }
        }

        // GET: Author/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var author = await _authorService.GetByIdAsync(id);
            if (author == null)
                return NotFound();

            var dto = new UpdateAuthorDto
            {
                Id = author.Id,
                Name = author.Name,
                Description = author.Description,
                BornYear = author.BornYear,
                DeathYear = author.DeathYear,
                Gender = author.Gender
            };

            return View(dto);
        }

        // POST: Author/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateAuthorDto dto)
        {
            if (id != dto.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(dto);

            try
            {
                var result = await _authorService.UpdateAsync(dto);
                if (!result)
                    return NotFound();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error while updating author: {ex.Message}");
                return View(dto);
            }
        }

        // GET: Author/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var author = await _authorService.GetByIdAsync(id);
            if (author == null)
                return NotFound();

            return View(author);
        }

        // POST: Author/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var result = await _authorService.DeleteAsync(id);
                if (!result)
                    return NotFound();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error while deleting author: {ex.Message}");
                var author = await _authorService.GetByIdAsync(id);
                if (author == null)
                    return NotFound();

                return View(author);
            }
        }

        // AJAX Delete Endpoint
        [HttpDelete]
        [Route("Author/Delete/{id}")]
        public async Task<IActionResult> DeleteAjax(int id)
        {
            try
            {
                await _authorService.DeleteAsync(id);
                return Json(new { success = true, message = "The author has been deleted." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"An error occurred while deleting the author: {ex.Message}" });
            }
         }
    }
}
