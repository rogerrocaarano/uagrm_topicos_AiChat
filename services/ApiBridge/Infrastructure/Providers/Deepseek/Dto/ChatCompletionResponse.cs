using Infrastructure.Providers.Deepseek.Model;

namespace Infrastructure.Providers.Deepseek.Dto;

public record ChatCompletionResponse(
    string Id,
    string Model,
    List<Choice> Choices);