using Domain.Constant;
using Infrastructure.Providers.Deepseek.Constant;
using Infrastructure.Providers.Deepseek.Dto;
using Infrastructure.Providers.Deepseek.Model;

namespace Infrastructure.Providers.Deepseek.Builder;

public static class ChatCompletionBuilder
{
    public static Message BuildMessage(Domain.Model.Message message)
    {
        return new Message(RoleMapping(message.Type), message.Text);
    }

    public static ChatCompletionRequest BuildRequest(Domain.Model.Conversation conversation,
        List<Domain.Model.Message> context)
    {
        var messages = BuildMessageList(conversation, context);
        return new ChatCompletionRequest(messages);
    }

    private static List<Message> BuildMessageList(Domain.Model.Conversation conversation,
        List<Domain.Model.Message> context)
    {
        var messages = new List<Message>();
        messages.Add(BuildMessage(conversation.Rules));
        messages.AddRange(context.Select(BuildMessage));

        foreach (var answeredQuestion in conversation.AnsweredQuestions)
        {
            var question = BuildMessage(answeredQuestion.Item1);
            var answer = BuildMessage(answeredQuestion.Item2);
            messages.Add(question);
            messages.Add(answer);
        }

        messages.Add(BuildMessage(conversation.Question));
        return messages;
    }

    private static string RoleMapping(string domainMessageType)
    {
        return domainMessageType switch
        {
            MessageType.Assistant => MessageRole.Assistant,
            MessageType.User => MessageRole.User,
            MessageType.Context => MessageRole.System,
            MessageType.Rule => MessageRole.System,
            _ => throw new ArgumentException()
        };
    }
}