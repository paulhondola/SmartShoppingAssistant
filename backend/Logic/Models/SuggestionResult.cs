using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Logic.Models;

[Description("Product suggestions for the shopping cart")]
public sealed class SuggestionResult
{
    [JsonPropertyName("suggestions")]
    public List<Suggestion> Suggestions { get; set; } = [];
}

public sealed class Suggestion
{
    [JsonPropertyName("productId")]
    public int ProductId { get; set; }

    [JsonPropertyName("productName")]
    public string ProductName { get; set; } = "";

    [JsonPropertyName("price")]
    public decimal Price { get; set; }

    [JsonPropertyName("categoryName")]
    public string CategoryName { get; set; } = "";

    [JsonPropertyName("reason")]
    public string Reason { get; set; } = "";
}
