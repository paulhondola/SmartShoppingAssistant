using System.ComponentModel;
using Logic.Agents.Interfaces;
using Logic.Models;
using Logic.Services.Interfaces;
using Logic.Tools;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;

namespace Logic.Agents;

public class PromotionCheckerAgent(IChatClient chatClient, IPromotionService promotionService)
    : IPromotionCheckerAgent
{
    public ChatClientAgent Build(string cartJson)
    {
        return new ChatClientAgent(
            chatClient,
            new ChatClientAgentOptions
            {
                Name = "PromotionChecker",
                Description = "Checks promotions for cart items",
                ChatOptions = new ChatOptions
                {
                    Instructions = $"""
                    You are a promotion analyst. Here is the current cart (each item includes its categories):
                    {cartJson}

                    Steps:
                    1. Call GetPromotionsForProduct for EVERY product in the cart.
                    2. Evaluate each promotion's threshold:
                       - Quantity promotions: compare Threshold against item quantity (product-level)
                         or total items in that category across ALL cart items (category-level).
                       - CartTotal promotions: compare Threshold against the line total of the product
                         (product-level) or the SUM of line totals of ALL cart items sharing that
                         category (category-level).
                    3. A promotion with a CategoryId (not ProductId) is category-level — you MUST
                       aggregate quantities/totals across every cart item that belongs to that category.
                    4. Classify each promotion as an active deal (threshold met) or a near-miss deal
                       (threshold not met but close). For near-miss deals, explain what the user needs
                       to add/change and calculate the potential savings.
                    5. When multiple promotions compete on the same product or category, note which
                       one gives the best value.
                    """,
                    ResponseFormat = ChatResponseFormat.ForJsonSchema<PromotionAnalysis>(),
                    Tools =
                    [
                        AIFunctionFactory.Create(
                            ([Description("The product ID to check")] int productId) =>
                                ShoppingTools.GetPromotionsForProduct(productId, promotionService),
                            "GetPromotionsForProduct",
                            "Get all active promotions that apply to a specific product (by product ID or its category)."
                        ),
                    ],
                },
            },
            null!,
            null!
        );
    }
}
