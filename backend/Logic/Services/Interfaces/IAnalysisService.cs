using Logic.Models;

namespace Logic.Services.Interfaces;

public interface IAnalysisService
{
    Task<CartAnalysisResponse> AnalyzeCartAsync();
}
