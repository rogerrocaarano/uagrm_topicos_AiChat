using Infrastructure.Providers.Deepseek.Constant;
using Infrastructure.Providers.Deepseek.Model;

namespace Infrastructure.Providers.Deepseek.Dto;

public record ChatCompletionRequest(
    List<Message> Messages,
    bool Stream = false,
    string Model = ChatModel.Chat);