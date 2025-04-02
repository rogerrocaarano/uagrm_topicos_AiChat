namespace Infrastructure.Api.Deepseek.Models;

public record ChatCompletionResponse(
    string Id,
    string Model,
    List<Choice> Choices);