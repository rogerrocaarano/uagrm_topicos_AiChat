namespace Domain.Service;

public interface IEmbeddingService
{
    Task<Guid> SaveEmbedding(string content);
    Task<List<Guid>> GetSimilarEmbeddingIds(string content);
}