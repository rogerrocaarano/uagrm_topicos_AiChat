namespace Domain.Repository;

public interface IEmbeddingRepository
{
    Task<Guid> SaveEmbedding(object content);
    Task<List<Guid>> GetSimilarEmbeddingIds(string content);
}