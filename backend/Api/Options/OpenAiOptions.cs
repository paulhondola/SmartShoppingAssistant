namespace Api.Options;

public sealed class OpenAiOptions
{
    public const string SectionName = "OpenAI";

    public string? ApiKey { get; init; }
    public string ModelId { get; init; } = "gpt-4o";

    public bool IsConfigured => !string.IsNullOrWhiteSpace(ApiKey);
}
