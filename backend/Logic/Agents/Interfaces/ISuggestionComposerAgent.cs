using Microsoft.Agents.AI;

namespace Logic.Agents.Interfaces;

public interface ISuggestionComposerAgent
{
    ChatClientAgent Build(string cartJson, string categoriesJson);
}
