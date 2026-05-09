using Logic.Models;

namespace Logic.Agents.Interfaces;

public interface IPromotionCheckerAgent
{
    Task<PromotionAnalysis> AnalyzeAsync(string cartJson);
}
