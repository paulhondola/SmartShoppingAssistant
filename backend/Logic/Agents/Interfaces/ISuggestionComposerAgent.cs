using Logic.Models;

namespace Logic.Agents.Interfaces;

public interface ISuggestionComposerAgent
{
    Task<SuggestionResult> ComposeAsync(
        string cartJson,
        string categoriesJson,
        string promotionAnalysisJson
    );
}
