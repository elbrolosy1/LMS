using BLL__Buisness_Logic_Layer_.Dtos.CategoryDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL__Buisness_Logic_Layer_.Services.CategoryServices
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryReadDto>> GetAllAsync();
        Task<CategoryReadDto?> GetByIdAsync(int id);
        Task CreateAsync(CategoryCreateDto dto);
        Task<bool> UpdateAsync(CategoryUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
