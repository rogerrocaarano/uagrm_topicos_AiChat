namespace Domain.Repository;

public interface IDocumentsRepository
{
    Task<string> GetFragment(Guid id);
    Task<List<string>> GetFragments(List<Guid> ids);
    Task<string> GetFragmentByEmbeddedId(Guid embeddedId);
    Task<List<string>> GetFragmentsByEmbeddedIds(List<Guid> embeddedIds);
}