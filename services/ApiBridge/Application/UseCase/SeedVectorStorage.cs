using Domain.Service;

namespace Application.UseCase;

public class SeedVectorStorage(IDocumentStorageService documents, IVectorStorageService embeddings)
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