using System.ComponentModel;
using Logic.DTOs.Products;
using Logic.DTOs.Promotions;
using Logic.Services.Interfaces;

namespace Logic.Tools;

public static class ShoppingTools
{
    [Description(
        "Get all active promotions that apply to a specific product (by product ID or its category)."
    )]
    public static async Task<List<PromotionGetDto>> GetPromotionsForProduct(
        [Description("The product ID to check")] int productId,
        IPromotionService promotionService
    )
    {
        return await promotionService.GetForProductAsync(productId);
    }

    [Description("Search products by keyword. Returns matching products from the catalog.")]
    public static async Task<List<ProductGetDto>> SearchProducts(
        [Description("Search query (e.g. 'cheese', 'bread', 'olive oil')")] string query,
        IProductService productService
    )
    {
        return await productService.SearchAsync(query);
    }

    [Description("Get all products in a category.")]
    public static async Task<List<ProductGetDto>> GetProductsByCategory(
        [Description("The category ID")] int categoryId,
        IProductService productService
    )
    {
        return await productService.GetByCategoryAsync(categoryId);
    }
}
