using Domain.Repository;

namespace Application.UseCase;

public class SeedVectorStorage(IDocumentsRepository documents, IEmbeddingRepository embeddings)
{
    public async Task Execute()
    {
        var documentIds = await documents.GetAllDocumentIds();
        foreach (var id in documentIds)
        {
            await EmbedDocument(id);
        }
    }
    
    private async Task EmbedDocument(Guid documentId)
    {
        var fragments = await documents.GetDocumentFragments(documentId);
        foreach (var fragment in fragments)
        {
            var vectorId = await embeddings.SaveEmbedding(fragment);
            await documents.SetVectorId(documentId, vectorId);
        }
    }
}