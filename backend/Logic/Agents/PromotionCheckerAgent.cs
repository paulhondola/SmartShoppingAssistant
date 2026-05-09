using System.ComponentModel;
using System.Text.Json;
using Logic.Agents.Interfaces;
using Logic.Models;
using Logic.Services.Interfaces;
using Microsoft.Extensions.AI;

namespace Logic.Agents;

public class PromotionCheckerAgent(IChatClient chatClient, IPromotionService promotionService)
    : IPromotionCheckerAgent
{
    public async Task<PromotionAnalysis> AnalyzeAsync(string cartJson)
    {
        var tool = AIFunctionFactory.Create(
            async ([Description("The product ID to check")] int productId) =>
                await promotionService.GetForProductAsync(productId),
            "GetPromotionsForProduct",
            "Get active promotions that apply to a specific product (by product ID or its category)."
        );

        var options = new ChatOptions
        {
            Tools = [tool],
            ResponseFormat = ChatResponseFormat.ForJsonSchema<PromotionAnalysis>(),
        };

        var messages = new List<ChatMessage>
        {
            new(
                ChatRole.System,
                $"""
                You are a promotion analyzer for a shopping cart.
                Current cart (JSON):
                {cartJson}

                Instructions:
                1. Call GetPromotionsForProduct for each unique product ID in the cart.
                2. Compare each promotion's threshold against the cart quantities and cart subtotal.
                3. Classify promotions as:
                   - activeDeals: threshold already met (deal is applied)
                   - nearMissDeals: threshold not yet met but close (within 20% or 1-2 items)
                4. For nearMissDeals, set Action (e.g. "Add 1 more item") and Savings (potential saving value).
                5. Return only the JSON — no commentary.
                """
            ),
            new(ChatRole.User, "Analyze the promotions for this cart."),
        };

        var response = await chatClient.GetResponseAsync(messages, options);
        var text = response.Messages.LastOrDefault()?.Text ?? "{}";
        return JsonSerializer.Deserialize<PromotionAnalysis>(text) ?? new PromotionAnalysis();
    }
}
