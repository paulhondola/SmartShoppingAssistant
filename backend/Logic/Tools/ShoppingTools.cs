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
    ) => await promotionService.GetForProductAsync(productId);

    [Description("Get all products available in a specific category.")]
    public static async Task<List<ProductGetDto>> GetProductsByCategory(
        [Description("The category ID to search in")] int categoryId,
        IProductService productService
    ) => await productService.GetByCategoryAsync(categoryId);
}
