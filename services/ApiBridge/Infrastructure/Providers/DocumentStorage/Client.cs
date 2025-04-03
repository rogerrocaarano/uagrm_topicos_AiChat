using Domain.Repository;

namespace Infrastructure.Providers.DocumentStorage;

public class Client : IDocumentsRepository
{
    // public Task<string> GetFragment(Guid id)
    // {
    //     throw new NotImplementedException();
    // }

    // public Task<List<string>> GetFragments(List<Guid> ids)
    // {
    //     throw new NotImplementedException();
    // }

    // public Task<string> GetFragmentByEmbeddedId(Guid embeddedId)
    // {
    //     throw new NotImplementedException();
    // }
    
    // public Task<List<string>> GetFragmentsByEmbeddedIds(List<Guid> embeddedIds)
    // {
    //     throw new NotImplementedException();
    // }

    // public Task<List<Guid>> GetAllDocumentIds()
    // {
    //     throw new NotImplementedException();
    // }

    // public Task<List<string>> GetDocumentFragments(Guid documentId)
    // {
    //     throw new NotImplementedException();
    // }

    // public Task<List<Guid>> GetDocumentFragmentIds(Guid documentId)
    // {
    //     throw new NotImplementedException();
    // }

    // public Task SetVcetorId(Guid fragmentId, Guid vectorId)
    // {
    //     throw new NotImplementedException();
    // }

    private readonly HttpClient _httpClient;

        private readonly HttpClient _httpClient;

    public Client()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:5001/")
        };
    }

        public async Task<string> GetFragment(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<string>($"api/documents/GetFragmentById?fragmentId={id}");
        }

        public async Task<List<string>> GetFragments(List<Guid> ids)
        {
            var response = await _httpClient.PostAsJsonAsync("api/documents/GetFragmentsByIds", ids);
            return await response.Content.ReadFromJsonAsync<List<string>>();
        }

        public async Task<string> GetFragmentByEmbeddedId(Guid embeddedId)
        {
            return await _httpClient.GetFromJsonAsync<string>($"api/documents/GetFragmentByEmbeddedId?embeddedId={embeddedId}");
        }

        public async Task<List<string>> GetFragmentsByEmbeddedIds(List<Guid> embeddedIds)
        {
            var response = await _httpClient.PostAsJsonAsync("api/documents/GetFragmentsByEmbeddedIds", embeddedIds);
            return await response.Content.ReadFromJsonAsync<List<string>>();
        }

        public async Task<List<Guid>> GetAllDocumentIds()
        {
            return await _httpClient.GetFromJsonAsync<List<Guid>>("api/documents/GetAllDocuments");
        }

        public async Task<List<string>> GetDocumentFragments(Guid documentId)
        {
            return await _httpClient.GetFromJsonAsync<List<string>>($"api/documents/GetDocumentById?documentId={documentId}");
        }

        public async Task<List<Guid>> GetDocumentFragmentIds(Guid documentId)
        {
            return await _httpClient.GetFromJsonAsync<List<Guid>>($"api/documents/GetDocumentFragmentIds?documentId={documentId}");
        }

        public async Task<bool> SetVcetorId(Guid fragmentId, Guid vectorId)
        {
             var response = await _httpClient.PostAsync($"api/documents/SetVectorId?fragmentId={fragmentId}&vectorId={vectorId}", null);

            return response.IsSuccessStatusCode; 
         }
}