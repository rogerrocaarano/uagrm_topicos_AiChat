using Domain.Repository;

namespace Infrastructure.Providers.VectorStorage;

public class Client : IEmbeddingRepository
{
    public Task<Guid> SaveEmbedding(string content)
    {
        throw new NotImplementedException();
    }

    public Task<List<Guid>> GetSimilarEmbeddingIds(string content)
    {
        throw new NotImplementedException();
    }
}