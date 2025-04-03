using Infrastructure.Providers.Deepseek.Models;

namespace Infrastructure.Api.Deepseek.Models;

public record Choice(
    int Index,
    Message Message,
    string FinishReason);