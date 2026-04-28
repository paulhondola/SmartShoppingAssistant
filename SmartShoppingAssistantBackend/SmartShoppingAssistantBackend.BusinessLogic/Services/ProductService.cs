using SmartShoppingAssistantBackend.BusinessLogic.DTOs.Categories;
using SmartShoppingAssistantBackend.BusinessLogic.DTOs.Products;
using SmartShoppingAssistantBackend.BusinessLogic.DTOs.Promotions;
using SmartShoppingAssistantBackend.BusinessLogic.Services.Interfaces;
using SmartShoppingAssistantBackend.DataAccess.Entities;
using SmartShoppingAssistantBackend.DataAccess.Repositories.Interfaces;

namespace SmartShoppingAssistantBackend.BusinessLogic.Services;

public class ProductService(IRepository<Product> productRepository) : IProductService
{
    public async Task DeleteProductAsync(int id)
    {
        var product = await productRepository.GetByIdAsync(id);
        await productRepository.DeleteAsync(product);
    }

    public async Task<ProductGetDto> GetProductByIdAsync(int id)
    {
        var product = await productRepository.GetByIdAsync(id)
                      ?? throw new KeyNotFoundException($"Product with ID {id} not found.");

        return MapToDto(product);
    }

    public async Task<List<ProductGetDto>> GetAllProductsAsync()
    {
        var products = await productRepository.GetAllAsync();
        return products.Select(MapToDto).ToList();
    }

    public async Task<ProductGetDto> AddProductAsync(ProductCreateDto productCreateDto)
    {
        var product = new Product
        {
            Name = productCreateDto.Name,
            Description = productCreateDto.Description,
            Price = productCreateDto.Price,
            ImageUrl = productCreateDto.ImageUrl
        };

        var added = await productRepository.AddAsync(product);
        return MapToDto(added);
    }

    public async Task<ProductGetDto> UpdateProductAsync(int id, ProductUpdateDto productUpdateDto)
    {
        var product = await productRepository.GetByIdAsync(id);

        if (productUpdateDto.Name is not null)
            product.Name = productUpdateDto.Name;

        if (productUpdateDto.Description is not null)
            product.Description = productUpdateDto.Description;

        if (productUpdateDto.Price is not null)
            product.Price = productUpdateDto.Price.Value;

        if (productUpdateDto.ImageUrl is not null)
            product.ImageUrl = productUpdateDto.ImageUrl;

        var updated = await productRepository.UpdateAsync(product);
        return MapToDto(updated);
    }

    private static ProductGetDto MapToDto(Product product)
    {
        return new ProductGetDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description ?? string.Empty,
            Price = product.Price,
            ImageUrl = product.ImageUrl,
            Categories = product.Categories
                .Select(c => new CategorySummaryDto { Id = c.Id, Name = c.Name })
                .ToList(),
            Promotions = product.Promotions
                .Concat(product.Categories.SelectMany(c => c.Promotions))
                .DistinctBy(p => p.Id)
                .Select(p => new PromotionSummaryDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Reward = p.Reward,
                    RewardValue = p.RewardValue,
                    IsActive = p.IsActive
                })
                .ToList()
        };
    }
}