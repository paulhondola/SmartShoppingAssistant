using Logic.Agents.Interfaces;
using Microsoft.Agents.AI;

namespace Logic.Agents;

public class UnavailablePromotionCheckerAgent : IPromotionCheckerAgent
{
    public ChatClientAgent Build(string cartJson) =>
        throw new InvalidOperationException("Cart analysis requires OpenAI to be configured.");
}
