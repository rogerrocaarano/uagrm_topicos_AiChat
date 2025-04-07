using Domain.Constant;
using Domain.Model;
using Domain.Repository;
using Domain.Service;

namespace Application.UseCase;

public class AskLlm(ILlmChatService illm, IDocumentStorageRepository documents, IVectorStorageService embeddings)
{
    public async Task<Conversation> Execute(Conversation conversation)
    {
        var contextDbIds = await embeddings.GetSimilarEmbeddingIds(conversation.Question.Text);
        var context = await GetContext(contextDbIds);
        var llmResponse = await GetLlmResponse(conversation, context);
        llmResponse.ContextDbIds = contextDbIds;
        conversation.AnswerQuestion(llmResponse);
        return conversation;
    }
    
    private async Task<List<Message>> GetContext(List<Guid> contextDbIds)
    {
        var context = new List<Message>();
        foreach (var id in contextDbIds)
        {
            var fragment = await documents.GetFragment(id);
            context.Add(new Message
            {
                Type = MessageType.Context,
                Text = fragment
            });
        }

        return context;
    }

    private async Task<Message> GetLlmResponse(Conversation conversation, List<Message> context)
    {
        var llmResponse = await illm.AskLlmChat(conversation, context);
        conversation.Question.LinkId = Guid.NewGuid();
        var responseMessage = new Message
        {
            Type = MessageType.Assistant,
            Text = llmResponse,
            SendTime = DateTime.UtcNow,
            LinkId = conversation.Question.LinkId
        };
        return responseMessage;
    }
}