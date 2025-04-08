using Domain.Model;

namespace Domain.Repository;

public interface IDocumentStorageRepository
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
    
    /// <summary>
    /// Gets a List of all the documents stored in the database.
    /// </summary>
    /// <returns>List of Document's unique IDs.</returns>
    Task<List<Guid>> GetAllDocumentIds();
    
    /// <summary>
    /// Gets all the fragments of a document by its unique identifier.
    /// </summary>
    /// <param name="documentId">The document's unique ID.</param>
    /// <returns>List of fragments of the document.</returns>
    Task<List<string>> GetDocumentFragments(Guid documentId);
    
    
    Task<List<Guid>> GetDocumentFragmentIds(Guid documentId);
    
    /// <summary>
    /// Set the vector ID for a fragment.
    /// </summary>
    /// <param name="fragmentId">Fragment unique ID on DocumentsRepository.</param>
    /// <param name="vectorId">Vector unique ID on EmbeddingRepository.</param>
    Task SetVectorId(Guid fragmentId, Guid vectorId);
    Task SaveFragment(Fragment fragment);
}