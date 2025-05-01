using BLL__Buisness_Logic_Layer_.Dtos.BookDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL__Buisness_Logic_Layer_.Services.BookServices
{
    public interface IBookService
    {
        Task<IEnumerable<BookReadDto>> GetAllBooksAsync();
        Task<BookReadDto?> GetBookByIdAsync(int id);
        Task CreateBookAsync(BookCreateDto bookCreateDto);
        Task<BookUpdateDto?> UpdateBookAsync(int id, BookUpdateDto bookUpdateDto);
        Task DeleteBookAsync(int id); // خليك consistent وخليها async برضو


    }
}
