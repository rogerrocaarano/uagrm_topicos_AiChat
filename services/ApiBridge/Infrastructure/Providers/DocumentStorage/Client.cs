using System.Net.Http.Json;
using Domain.Repository;

namespace Infrastructure.Providers.DocumentStorage;

public class Client(string baseUrl) : IDocumentsRepository
{
    private readonly HttpClient _httpClient = new()
    {
        BaseAddress = new Uri(baseUrl)
    };

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
        return await _httpClient.GetFromJsonAsync<string>(
            $"api/documents/GetFragmentByEmbeddedId?embeddedId={embeddedId}");
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
        return await _httpClient.GetFromJsonAsync<List<string>>(
            $"api/documents/GetDocumentById?documentId={documentId}");
    }

    public async Task<List<Guid>> GetDocumentFragmentIds(Guid documentId)
    {
        return await _httpClient.GetFromJsonAsync<List<Guid>>(
            $"api/documents/GetDocumentFragmentIds?documentId={documentId}");
    }

    public async Task SetVectorId(Guid fragmentId, Guid vectorId)
    {
        var response =
            await _httpClient.PostAsync($"api/documents/SetVectorId?fragmentId={fragmentId}&vectorId={vectorId}", null);
    }
}