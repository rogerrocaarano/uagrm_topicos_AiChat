namespace DeepseekClient.Models;

public record ChatCompletionResponse(
    string Id,
    string Model,
    List<Choice> Choices);