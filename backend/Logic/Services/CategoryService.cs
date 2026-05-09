using Data.Entities;
using Data.Repositories.Interfaces;
using Logic.DTOs.Categories;
using Logic.Services.Interfaces;

namespace Logic.Services;

public class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
{
    public async Task<List<CategoryGetDto>> GetAllAsync()
    {
        var categories = await categoryRepository.GetAllAsync();
        return categories.Select(MapToDto).ToList();
    }

    public async Task<CategoryGetDto> GetByIdAsync(int id)
    {
        var category = await categoryRepository.GetByIdAsync(id);
        return MapToDto(category);
    }

    public async Task<CategoryGetDto> CreateAsync(CategoryCreateDto dto)
    {
        var category = new Category { Name = dto.Name, Description = dto.Description };
        var created = await categoryRepository.AddAsync(category);
        return MapToDto(created);
    }

    public async Task<CategoryGetDto> UpdateAsync(int id, CategoryUpdateDto dto)
    {
        var category = await categoryRepository.GetByIdAsync(id);
        category.Name = dto.Name;
        category.Description = dto.Description;
        var updated = await categoryRepository.UpdateAsync(category);
        return MapToDto(updated);
    }

    public Task DeleteAsync(int id) => categoryRepository.DeleteAsync(id);

    private static CategoryGetDto MapToDto(Category c) =>
        new()
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description,
        };
}
