namespace Domain.Service;

public interface IVectorStorageService
{
    Task<Guid> SaveEmbedding(string content);
    Task<List<Guid>> GetSimilarEmbeddingIds(string content);
}