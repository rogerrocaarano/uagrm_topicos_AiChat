using Domain.Model;
using Domain.Repository;
using Domain.Service;

namespace Application.UseCase;

public class AskLlm(ILlmChat llm, IDocumentsRepository documents, IEmbeddingRepository embeddings)
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
                Type = "context",
                Text = fragment
            });
        }

        return context;
    }

    private async Task<Message> GetLlmResponse(Conversation conversation, List<Message> context)
    {
        var llmResponse = await llm.AskLlmChat(conversation, context);
        var responseMessage = new Message
        {
            Type = "assistant",
            Text = llmResponse,
            SendTime = DateTime.UtcNow
        };
        return responseMessage;
    }
}