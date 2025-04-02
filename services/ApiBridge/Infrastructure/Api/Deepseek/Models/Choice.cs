namespace Infrastructure.Api.Deepseek.Models;

public record Choice(
    int Index,
    Message Message,
    string FinishReason);