using Logic.Agents.Interfaces;
using Microsoft.Agents.AI;

namespace Logic.Agents;

public class UnavailableSuggestionComposerAgent : ISuggestionComposerAgent
{
    public ChatClientAgent Build(string cartJson, string categoriesJson) =>
        throw new InvalidOperationException("Cart analysis requires OpenAI to be configured.");
}
