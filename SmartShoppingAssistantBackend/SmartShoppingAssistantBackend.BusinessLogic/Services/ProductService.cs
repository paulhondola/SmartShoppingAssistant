using SmartShoppingAssistantBackend.BusinessLogic.DTOs;
using SmartShoppingAssistantBackend.BusinessLogic.Services.Interfaces;
using SmartShoppingAssistantBackend.DataAccess.Entities;
using SmartShoppingAssistantBackend.DataAccess.Repositories;

namespace SmartShoppingAssistantBackend.BusinessLogic.Services;

public class ProductService(IRepository<Product> productRepository) : IProductService
{
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

    public async Task DeleteProductAsync(int id)
    {
        var product = await productRepository.GetByIdAsync(id);
        await productRepository.DeleteAsync(product);
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
            Categories = product.ProductCategories
                .Select(pc => pc.Category.Name)
                .ToList()
        };
    }
}