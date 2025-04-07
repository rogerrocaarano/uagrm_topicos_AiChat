using Domain.Constant;
using Infrastructure.Providers.Deepseek.Constant;

namespace Infrastructure.Providers.Deepseek.Builder;

public static class ChatCompletionBuilder
{
    public static Models.Message BuildMessage(Domain.Model.Message message)
    {
        return new Models.Message(RoleMapping(message.Type), message.Text);
    }

    public static Models.ChatCompletionRequest BuildRequest(Domain.Model.Conversation conversation,
        List<Domain.Model.Message> context)
    {
        var messages = BuildMessageList(conversation, context);
        return new Models.ChatCompletionRequest(messages);
    }

    private static List<Models.Message> BuildMessageList(Domain.Model.Conversation conversation,
        List<Domain.Model.Message> context)
    {
        var messages = new List<Models.Message>();
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