namespace Domain.Service;

public interface IVectorStorageService
{
    Task<Guid> SaveEmbedding(string content);
    Task<List<Guid>> GetSimilarEmbeddingIds(string content);
    Task<List<string>> GetFragmentsFromText(string content);
}