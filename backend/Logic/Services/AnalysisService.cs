using System.Text.Json;
using Logic.Agents.Interfaces;
using Logic.Models;
using Logic.Services.Interfaces;

namespace Logic.Services;

public class AnalysisService(
    ICartService cartService,
    ICategoryService categoryService,
    IPromotionCheckerAgent promotionCheckerAgent,
    ISuggestionComposerAgent suggestionComposerAgent
) : IAnalysisService
{
    private static readonly JsonSerializerOptions JsonOpts = new(JsonSerializerDefaults.Web);

    public async Task<CartAnalysisResponse> AnalyzeCartAsync()
    {
        var cart = await cartService.GetCartAsync();
        var cartJson = JsonSerializer.Serialize(cart, JsonOpts);

        var categories = await categoryService.GetAllAsync();
        var categoriesJson = JsonSerializer.Serialize(categories, JsonOpts);

        var promotionAnalysis = await promotionCheckerAgent.AnalyzeAsync(cartJson);
        var promotionAnalysisJson = JsonSerializer.Serialize(promotionAnalysis, JsonOpts);

        var suggestions = await suggestionComposerAgent.ComposeAsync(
            cartJson,
            categoriesJson,
            promotionAnalysisJson
        );

        return new CartAnalysisResponse
        {
            PromotionAnalysis = promotionAnalysis,
            Suggestions = suggestions,
        };
    }
}
