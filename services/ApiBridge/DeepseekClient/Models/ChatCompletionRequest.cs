namespace DeepseekClient.Models;

public record ChatCompletionRequest(
    List<Message> Messages,
    bool Stream = false,
    string Model = "deepseek-chat");