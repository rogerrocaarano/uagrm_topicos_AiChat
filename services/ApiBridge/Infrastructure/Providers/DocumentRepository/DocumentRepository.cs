using Domain.Repository;

namespace Infrastructure.Providers.DocumentRepository;

public class DocumentRepository : IDocumentStorageRepository
{
    public Task<string> GetFragment(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<string>> GetFragments(List<Guid> ids)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetFragmentByEmbeddedId(Guid embeddedId)
    {
        throw new NotImplementedException();
    }

    public Task<List<string>> GetFragmentsByEmbeddedIds(List<Guid> embeddedIds)
    {
        throw new NotImplementedException();
    }

    public Task<List<Guid>> GetAllDocumentIds()
    {
        throw new NotImplementedException();
    }

    public Task<List<string>> GetDocumentFragments(Guid documentId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Guid>> GetDocumentFragmentIds(Guid documentId)
    {
        throw new NotImplementedException();
    }

    public Task SetVectorId(Guid fragmentId, Guid vectorId)
    {
        throw new NotImplementedException();
    }
}