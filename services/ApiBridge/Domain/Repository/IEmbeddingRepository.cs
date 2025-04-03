namespace Domain.Repository;

public interface IEmbeddingRepository
{
    Task<Guid> SaveEmbedding(string content);
    Task<List<Guid>> GetSimilarEmbeddingIds(string content);
}