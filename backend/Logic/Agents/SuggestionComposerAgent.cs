using System.ComponentModel;
using System.Text.Json;
using Logic.Agents.Interfaces;
using Logic.Models;
using Logic.Services.Interfaces;
using Logic.Tools;
using Microsoft.Extensions.AI;

namespace Logic.Agents;

public class SuggestionComposerAgent(IChatClient chatClient, IProductService productService)
    : ISuggestionComposerAgent
{
    public async Task<SuggestionResult> ComposeAsync(
        string cartJson,
        string categoriesJson,
        string promotionAnalysisJson
    )
    {
        var tool = AIFunctionFactory.Create(
            async ([Description("The category ID to search")] int categoryId) =>
                await ShoppingTools.GetProductsByCategory(categoryId, productService),
            "GetProductsByCategory",
            "Get all products available in a specific category."
        );

        var options = new ChatOptions
        {
            Tools = [tool],
            ResponseFormat = ChatResponseFormat.ForJsonSchema<SuggestionResult>(),
        };

        var messages = new List<ChatMessage>
        {
            new(
                ChatRole.System,
                $"""
                You are a smart shopping assistant.

                Current cart (JSON):
                {cartJson}

                Available categories (JSON):
                {categoriesJson}

                Promotion analysis (JSON):
                {promotionAnalysisJson}

                Instructions:
                1. Call GetProductsByCategory for categories relevant to the cart contents.
                2. Suggest products that naturally complement what is already in the cart.
                3. Prioritise products that would activate near-miss promotions identified in the analysis.
                   For each such suggestion include in Reason: "Add this to unlock: <promotion name>".
                4. Return MAXIMUM 5 suggestions total.
                5. Do not suggest products already in the cart.
                6. Return only the JSON — no commentary.
                """
            ),
            new(ChatRole.User, "Suggest relevant products for this cart."),
        };

        var response = await chatClient.GetResponseAsync(messages, options);
        var text = response.Messages.LastOrDefault()?.Text ?? "{}";
        return JsonSerializer.Deserialize<SuggestionResult>(text) ?? new SuggestionResult();
    }
}
