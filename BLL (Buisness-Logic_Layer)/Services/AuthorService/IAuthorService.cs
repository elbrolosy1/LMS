using BLL__Buisness_Logic_Layer_.Dtos.AuthorDto;
using BLL__Buisness_Logic_Layer_.Dtos.CategoryDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL__Buisness_Logic_Layer_.Services.AuthorService
{
    public interface IAuthorService
    {
        Task<IEnumerable<ReadAuthorDto>> GetAllAsync();
        Task<ReadAuthorDto?> GetByIdAsync(int id);
        Task CreateAsync(CreateAuthorDto dto);
        Task<bool> UpdateAsync(UpdateAuthorDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
