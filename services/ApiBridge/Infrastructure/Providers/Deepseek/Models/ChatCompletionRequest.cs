using Infrastructure.Providers.Deepseek.Constant;

namespace Infrastructure.Providers.Deepseek.Models;

public record ChatCompletionRequest(
    List<Message> Messages,
    bool Stream = false,
    string Model = ChatModel.Chat);