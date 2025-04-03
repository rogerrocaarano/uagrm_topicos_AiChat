namespace Domain.Repository;

public interface IDocumentsRepository
{
    /// <summary>
    /// Gets a fragment by its unique identifier.
    /// </summary>
    /// <param name="id">Fragment identifier.</param>
    /// <returns>Text of the fragment.</returns>
    Task<string> GetFragment(Guid id);
    
    /// <summary>
    /// Gets a list of fragments by their unique identifiers.
    /// </summary>
    /// <param name="ids">List of fragment's identifiers.</param>
    /// <returns>List of fragment's texts.</returns>
    Task<List<string>> GetFragments(List<Guid> ids);
    
    /// <summary>
    /// Gets a fragment by its embedded identifier.
    /// </summary>
    /// <param name="embeddedId">The identifier on the vector storage.</param>
    /// <returns>Text of the fragment.</returns>
    Task<string> GetFragmentByEmbeddedId(Guid embeddedId);
    
    /// <summary>
    /// Gets a list of fragments by their embedded identifiers.
    /// </summary>
    /// <param name="embeddedIds">List of fragment's identifiers.</param>
    /// <returns>List of fragment's texts.</returns>
    Task<List<string>> GetFragmentsByEmbeddedIds(List<Guid> embeddedIds);
}