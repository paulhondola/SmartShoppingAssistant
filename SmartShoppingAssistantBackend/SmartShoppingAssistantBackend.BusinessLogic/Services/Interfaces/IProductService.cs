using SmartShoppingAssistantBackend.BusinessLogic.DTOs.Products;

namespace SmartShoppingAssistantBackend.BusinessLogic.Services.Interfaces;

public interface IProductService
{
    Task<ProductGetDto> GetProductByIdAsync(int id);
    Task<List<ProductGetDto>> GetAllProductsAsync();
    Task<ProductGetDto> AddProductAsync(ProductCreateDto productCreateDto);
    Task<ProductGetDto> UpdateProductAsync(int id, ProductUpdateDto productUpdateDto);
    Task DeleteProductAsync(int id);
}