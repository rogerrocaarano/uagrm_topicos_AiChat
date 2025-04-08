using Domain.Repository;
using Domain.Service;

namespace Application.UseCase;

public class SeedDatabases(IDocumentStorageRepository documents, IVectorStorageService embeddings)
{
    public async Task Execute()
    {
        
    }
}