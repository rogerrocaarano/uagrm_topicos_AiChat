using Domain.Repository;
using Domain.Service;

namespace Infrastructure.Providers.DatabaseSeeder;

public class DatabaseSeeder(IDocumentStorageRepository documents, IVectorStorageService embeddings) : IDatabaseSeeder
{
    public async Task SeedFromFile(string filePath)
    {
        try
        {
            var content = await File.ReadAllTextAsync(filePath);
            var processedContent = await embeddings.GetFragmentsFromText(content);

            foreach (var fragment in processedContent)
            {
                // documents.
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Task SeedFromDirectory(string directoryPath)
    {
        throw new NotImplementedException();
    }
    
}