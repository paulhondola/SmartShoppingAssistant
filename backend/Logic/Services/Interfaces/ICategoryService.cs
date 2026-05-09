using Logic.DTOs.Categories;

namespace Logic.Services.Interfaces;

public interface ICategoryService
{
    Task<List<CategoryGetDto>> GetAllAsync();
    Task<CategoryGetDto> GetByIdAsync(int id);
    Task<CategoryGetDto> CreateAsync(CategoryCreateDto dto);
    Task<CategoryGetDto> UpdateAsync(int id, CategoryUpdateDto dto);
    Task DeleteAsync(int id);
}
