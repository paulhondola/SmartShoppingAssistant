using Logic.DTOs.Products;
using Logic.Services.Interfaces;
using Data.Entities;
using Data.Repositories;
using Data.Repositories.Interfaces;

namespace Logic.Services;

public class ProductService(
    IProductRepository productRepository,
    ICategoryRepository categoryRepository
) : IProductService
{
    public async Task<List<ProductGetDto>> GetAllAsync(
        int? categoryId,
        string? name,
        decimal? minPrice,
        decimal? maxPrice
    )
    {
        var products = await productRepository.GetAllAsync(categoryId, name, minPrice, maxPrice);
        return products.Select(MapToDto).ToList();
    }

    public async Task<ProductGetDto> GetByIdAsync(int id)
    {
        var product = await productRepository.GetByIdWithCategoriesAsync(id);
        return MapToDto(product);
    }

    public async Task<ProductGetDto> CreateAsync(ProductCreateDto dto)
    {
        var categories = await categoryRepository.GetByIdsAsync(dto.CategoryIds);
        var product = new Product
        {
            Name = dto.Name,
            Description = dto.Description,
            ImageUrl = dto.ImageUrl,
            Price = dto.Price,
            Categories = categories,
        };
        var created = await productRepository.AddAsync(product);
        return MapToDto(created);
    }

    public async Task<ProductGetDto> UpdateAsync(int id, ProductUpdateDto dto)
    {
        var product = await productRepository.GetByIdWithCategoriesAsync(id);
        product.Name = dto.Name;
        product.Description = dto.Description;
        product.ImageUrl = dto.ImageUrl;
        product.Price = dto.Price;
        product.Categories = await categoryRepository.GetByIdsAsync(dto.CategoryIds);
        var updated = await productRepository.UpdateAsync(product);
        return MapToDto(updated);
    }

    public Task DeleteAsync(int id) => productRepository.DeleteAsync(id);

    public async Task<List<ProductGetDto>> SearchAsync(string query)
    {
        var products = await productRepository.SearchAsync(query);
        return products.Select(MapToDto).ToList();
    }

    public async Task<List<ProductGetDto>> GetByCategoryAsync(int categoryId)
    {
        var products = await productRepository.GetByCategoryAsync(categoryId);
        return products.Select(MapToDto).ToList();
    }

    private static ProductGetDto MapToDto(Product p) =>
        new()
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            ImageUrl = p.ImageUrl,
            Price = p.Price,
            Categories = p.Categories.Select(c => c.Name).ToList(),
        };
}
