using Backend.BusinessLogic.DTOs.Categories;

namespace Backend.BusinessLogic.Services.Interfaces;

public interface ICategoryService
{
    Task<CategoryGetDto> GetCategoryByIdAsync(int id);
    Task<List<CategoryGetDto>> GetAllCategoriesAsync();
    Task<CategoryGetDto> AddCategoryAsync(CategoryCreateDto categoryCreateDto);
    Task<CategoryGetDto> UpdateCategoryAsync(int id, CategoryUpdateDto categoryUpdateDto);
    Task DeleteCategoryAsync(int id);
}
