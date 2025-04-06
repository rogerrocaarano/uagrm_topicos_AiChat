using Domain.Model;

namespace Domain.Service;

public interface ILlmChat
{
    Task<string> AskLlmChat(Conversation conversation, List<Message> context);
}