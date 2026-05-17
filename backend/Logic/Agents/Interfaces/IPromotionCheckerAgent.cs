using Microsoft.Agents.AI;

namespace Logic.Agents.Interfaces;

public interface IPromotionCheckerAgent
{
    ChatClientAgent Build(string cartJson);
}
