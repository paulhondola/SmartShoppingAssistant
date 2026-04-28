using SmartShoppingAssistantBackend.BusinessLogic.DTOs.Categories;
using SmartShoppingAssistantBackend.BusinessLogic.Services.Interfaces;
using SmartShoppingAssistantBackend.DataAccess.Entities;
using SmartShoppingAssistantBackend.DataAccess.Repositories;

namespace SmartShoppingAssistantBackend.BusinessLogic.Services;

public class CategoryService(IRepository<Category> categoryRepository) : ICategoryService
{
    public async Task<CategoryGetDto> GetCategoryByIdAsync(int id)
    {
        var category = await categoryRepository.GetByIdAsync(id)
                       ?? throw new KeyNotFoundException($"Category with ID {id} not found.");

        return MapToDto(category);
    }

    public async Task<List<CategoryGetDto>> GetAllCategoriesAsync()
    {
        var categories = await categoryRepository.GetAllAsync();
        return categories.Select(MapToDto).ToList();
    }

    public async Task<CategoryGetDto> AddCategoryAsync(CategoryCreateDto categoryCreateDto)
    {
        var category = new Category
        {
            Name = categoryCreateDto.Name,
            Description = categoryCreateDto.Description
        };

        var added = await categoryRepository.AddAsync(category);
        return MapToDto(added);
    }

    public async Task<CategoryGetDto> UpdateCategoryAsync(int id, CategoryUpdateDto categoryUpdateDto)
    {
        var category = await categoryRepository.GetByIdAsync(id)
                       ?? throw new KeyNotFoundException($"Category with ID {id} not found.");

        if (categoryUpdateDto.Name is not null)
            category.Name = categoryUpdateDto.Name;

        if (categoryUpdateDto.Description is not null)
            category.Description = categoryUpdateDto.Description;

        var updated = await categoryRepository.UpdateAsync(category);
        return MapToDto(updated);
    }

    public async Task DeleteCategoryAsync(int id)
    {
        var category = await categoryRepository.GetByIdAsync(id)
                       ?? throw new KeyNotFoundException($"Category with ID {id} not found.");

        await categoryRepository.DeleteAsync(category);
    }

    private static CategoryGetDto MapToDto(Category category)
    {
        return new CategoryGetDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description ?? string.Empty,
            Products = category.Products
                .Select(pc => pc.Name)
                .ToList()
        };
    }
}