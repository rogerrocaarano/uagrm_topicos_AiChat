namespace Infrastructure.Providers.Deepseek.Model;

public record Choice(
    int Index,
    Message Message,
    string FinishReason);