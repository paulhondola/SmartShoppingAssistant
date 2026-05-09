using Logic.DTOs.Products;

namespace Logic.Services.Interfaces;

public interface IProductService
{
    Task<List<ProductGetDto>> GetAllAsync(
        int? categoryId,
        string? name,
        decimal? minPrice,
        decimal? maxPrice
    );
    Task<ProductGetDto> GetByIdAsync(int id);
    Task<ProductGetDto> CreateAsync(ProductCreateDto dto);
    Task<ProductGetDto> UpdateAsync(int id, ProductUpdateDto dto);
    Task DeleteAsync(int id);
    Task<List<ProductGetDto>> SearchAsync(string query);
    Task<List<ProductGetDto>> GetByCategoryAsync(int categoryId);
}
