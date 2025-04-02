using Application.Dto;
using Domain.Model;
using Domain.Repository;
using Domain.Service;

namespace Application.UseCase;

public class AskLlm(ILlmChat llm, IDocumentsRepository documents, IEmbeddingRepository embeddings)
{
    public async Task<MessageDto> Execute(List<MessageDto> messages)
    {
        var conversation = new Conversation(
            BuildUserMessages(messages),
            BuildLlmMessages(messages),
            await BuildContextMessages(messages)
        );
        var llmResponse = await llm.AskLlmChat(conversation);

        return new MessageDto(false, llmResponse);
    }

    private List<Message> BuildUserMessages(List<MessageDto> messages)
    {
        return messages
            .Where(message => message.IsUser)
            .Select(message => new Message("user", message.Content))
            .ToList();
    }

    private List<Message> BuildLlmMessages(List<MessageDto> messages)
    {
        return messages
            .Where(message => !message.IsUser)
            .Select(message => new Message("llm", message.Content))
            .ToList();
    }

    private async Task<List<Message>> BuildContextMessages(List<MessageDto> messages)
    {
        var similarEmbeddedFragments = await embeddings.GetSimilarEmbeddingIds(messages
            .LastOrDefault(message => message.IsUser)!
            .Content
        );

        var similarFragments = await documents.GetFragmentsByEmbeddedIds(similarEmbeddedFragments);

        return [..similarFragments.Select(fragment => new Message("context", fragment))];
    }
}