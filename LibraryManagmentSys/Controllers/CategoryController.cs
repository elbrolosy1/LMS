using BLL__Buisness_Logic_Layer_.Dtos.CategoryDto;
using BLL__Buisness_Logic_Layer_.Services.CategoryServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagmentSys.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: Category
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync();
            return View(categories);
        }

        // GET: Category/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
                return NotFound();

            return View(category);
        }

        // GET: Category/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreateDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            try
            {
                await _categoryService.CreateAsync(dto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error while creating category: {ex.Message}");
                return View(dto);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
                return NotFound();

            var dto = new CategoryUpdateDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };

            return View(dto);
        }

        // POST: Category/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoryUpdateDto dto)
        {
            if (id != dto.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(dto);

            try
            {
                var result = await _categoryService.UpdateAsync(dto);
                if (!result)
                    return NotFound();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error while updating category: {ex.Message}");
                return View(dto);
            }
        }

        // GET: Category/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
                return NotFound();

            return View(category);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var result = await _categoryService.DeleteAsync(id);
                if (!result)
                    return NotFound();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error while deleting category: {ex.Message}");
                var category = await _categoryService.GetByIdAsync(id);
                if (category == null)
                    return NotFound();

                return View(category);
            }
        }
        [HttpDelete]
        [Route("Category/Delete/{id}")]
        public async Task<IActionResult> DeleteAjax(int id)
        {
            try
            {
                await _categoryService.DeleteAsync(id);
                return Json(new { success = true, message = "The Category has been deleted." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"An error occurred while deleting the Category: {ex.Message}" });
            }
        }

    }
}
