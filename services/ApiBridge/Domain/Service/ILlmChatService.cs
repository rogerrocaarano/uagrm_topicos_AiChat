using Domain.Model;

namespace Domain.Service;

public interface ILlmChatService
{
    Task<string> AskLlmChat(Conversation conversation, List<Message> context);
}