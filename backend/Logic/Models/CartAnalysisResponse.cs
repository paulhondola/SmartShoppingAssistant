using System.Text.Json.Serialization;

namespace Logic.Models;

public sealed class CartAnalysisResponse
{
    [JsonPropertyName("promotionAnalysis")]
    public PromotionAnalysis PromotionAnalysis { get; set; } = new();

    [JsonPropertyName("suggestions")]
    public SuggestionResult Suggestions { get; set; } = new();
}
