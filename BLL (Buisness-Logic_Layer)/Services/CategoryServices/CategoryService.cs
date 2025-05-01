using DAL__Data_Access_Layer_.Models;
using DAL__Data_Access_Layer_.Repo.GenericRepo;
using BLL__Buisness_Logic_Layer_.Dtos.CategoryDto;
using BLL__Buisness_Logic_Layer_.Services.CategoryServices;
using Microsoft.EntityFrameworkCore;

public class CategoryService : ICategoryService
{
    private readonly IGenericRepo<Category> _repo;

    public CategoryService(IGenericRepo<Category> repo)
    {
        _repo = repo ?? throw new ArgumentNullException(nameof(repo));
    }

    public async Task<IEnumerable<CategoryReadDto>> GetAllAsync()
    {
        var categories = await _repo.GetAllAsync();
        return await categories
            .Select(c => new CategoryReadDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            })
            .ToListAsync();
    }

    public async Task<CategoryReadDto?> GetByIdAsync(int id)
    {
        var category = await _repo.GetByIdAsync(id);
        if (category == null) return null;

        return new CategoryReadDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description

        };
    }

    public async Task CreateAsync(CategoryCreateDto dto)
    {
        if (dto == null) throw new ArgumentNullException(nameof(dto));

        var category = new Category
        {
            Name = dto.Name,
            Description = dto.Description
        };

        await _repo.AddAsync(category);
        await _repo.SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(CategoryUpdateDto dto)
    {
        if (dto == null) throw new ArgumentNullException(nameof(dto));

        var category = await _repo.GetByIdAsync(dto.Id);
        if (category == null) return false;

        category.Name = dto.Name;
        category.Description = dto.Description;
        await _repo.UpdateAsync(category);
        await _repo.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var category = await _repo.GetByIdAsync(id);
        if (category == null) return false;

        await _repo.DeleteAsync(id);
        await _repo.SaveChangesAsync();

        return true;
    }
}