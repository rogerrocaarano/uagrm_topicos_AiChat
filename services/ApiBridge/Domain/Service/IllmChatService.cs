using Domain.Model;

namespace Domain.Service;

public interface IllmChatService
{
    Task<string> AskLlmChat(Conversation conversation, List<Message> context);
}