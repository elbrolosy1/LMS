using BLL.Setting;
using BLL__Buisness_Logic_Layer_.Dtos.BookDto;
using DAL__Data_Access_Layer_.AppContext;
using DAL__Data_Access_Layer_.Models;
using DAL__Data_Access_Layer_.Repo.GenericRepo;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Net;

namespace BLL__Buisness_Logic_Layer_.Services.BookServices
{
    public class BookService : IBookService
    {
        private readonly IGenericRepo<Book> _repo;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imagesPath;

        public BookService(IGenericRepo<Book> repo, IWebHostEnvironment webHostEnvironment)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _webHostEnvironment = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));
            _imagesPath = Path.Combine(_webHostEnvironment.WebRootPath, FileSetting.ImagesPath);

            if (!Directory.Exists(_imagesPath))
            {
                Directory.CreateDirectory(_imagesPath);
            }
        }

        public async Task<IEnumerable<BookReadDto>> GetAllBooksAsync()
        {
            var query = await _repo.GetAllAsync(); // await هنا عشان نحصل على IQueryable<Book>

            return await query
                .Include(b => b.Category)
                .Include(b => b.Author)
                .AsNoTracking()
                .Select(book => new BookReadDto
                {
                    Id = book.Id,
                    Title = book.Title,
                    ISBN = book.ISBN,
                    Cover = book.Cover,
                    AuthorName = book.Author.Name,
                    CategoryName = book.Category.Name
                })
                .ToListAsync();
        }

        public async Task<BookReadDto?> GetBookByIdAsync(int id)
        {
            var query = await _repo.GetAllAsync(); // Await to get IQueryable<Book>

            var book = await query
                .Include(b => b.Author)
                .Include(b => b.Category)
                .AsNoTracking()
                .SingleOrDefaultAsync(b => b.Id == id);

            return book == null ? null : new BookReadDto
            {
                Id = book.Id,
                Title = book.Title,
                ISBN = book.ISBN,
                Cover = book.Cover,
                AuthorName = book.Author.Name,
                CategoryName = book.Category.Name,
                Description = book.Description,
            };
        }

        public async Task CreateBookAsync(BookCreateDto model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            if (model.Cover == null) throw new ArgumentException("Cover image is required.", nameof(model.Cover));

            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(model.Cover.FileName)}";
            var path = Path.Combine(_imagesPath, coverName);

            try
            {
                using var stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None, 4096, true);
                await model.Cover.CopyToAsync(stream);

                var newBook = new Book
                {
                    Title = model.Title,
                    ISBN = model.ISBN,
                    Cover = coverName,
                    AuthorId = model.AuthorId,
                    CategoryId = model.CategoryId,
                    Description = model.Description,
                };

                await _repo.AddAsync(newBook);
                await _repo.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                throw new Exception("Failed to create book.", ex);
            }
        }

        public async Task<BookUpdateDto?> UpdateBookAsync(int id, BookUpdateDto model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            var book = await _repo.GetByIdAsync(id);
            if (book == null) return null;

            var hasNewCover = model.NewCover != null;
            var oldCover = book.Cover;

            book.Title = model.Title;
            book.ISBN = model.ISBN;
            book.AuthorId = model.AuthorId;
            book.CategoryId = model.CategoryId;
            book.Description = model.Description;

            string? newCoverName = null;
            string? newCoverPath = null;

            if (hasNewCover)
            {
                newCoverName = $"{Guid.NewGuid()}{Path.GetExtension(model.NewCover.FileName)}";
                newCoverPath = Path.Combine(_imagesPath, newCoverName);

                try
                {
                    using var stream = new FileStream(newCoverPath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, true);
                    await model.NewCover.CopyToAsync(stream);
                    book.Cover = newCoverName;
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to upload the cover image.", ex);
                }
            }

            try
            {
                await _repo.UpdateAsync(book);
                await _repo.SaveChangesAsync();

                if (hasNewCover && !string.IsNullOrEmpty(oldCover))
                {
                    var oldCoverPath = Path.Combine(_imagesPath, oldCover);
                    if (File.Exists(oldCoverPath))
                    {
                        File.Delete(oldCoverPath);
                    }
                }

                return model;
            }
            catch (Exception ex)
            {
                if (hasNewCover && !string.IsNullOrEmpty(newCoverName) && File.Exists(newCoverPath))
                {
                    File.Delete(newCoverPath);
                }
                throw new Exception("Failed to update book.", ex);
            }
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await _repo.GetByIdAsync(id);
            if (book == null) throw new Exception("Book not found.");

            try
            {
                await _repo.DeleteAsync(book.Id);
                await _repo.SaveChangesAsync();

                if (!string.IsNullOrEmpty(book.Cover))
                {
                    var coverPath = Path.Combine(_imagesPath, book.Cover);
                    if (File.Exists(coverPath))
                    {
                        File.Delete(coverPath);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete book.", ex);
            }
        }

    }
}