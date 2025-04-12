using System.Net.Http.Json;
using Domain.Service;
using Infrastructure.Providers.Deepseek.Constant;
using Infrastructure.Providers.DocumentStorage.Model;
using RestSharp;

namespace Infrastructure.Providers.DocumentStorage;

public class Client(string baseUrl) : IDocumentStorageService
{
    private readonly HttpClient _httpClient = new()
    {
        BaseAddress = new Uri(baseUrl)
    };

    public async Task<string> GetFragment(Guid id)
    {
        return await _httpClient.GetFromJsonAsync<string>($"/GetFragmentById?fragmentId={id}");
    }

    public async Task<List<string>> GetFragments(List<Guid> ids)
    {
        var response = await _httpClient.PostAsJsonAsync("/GetFragmentsByIds", ids);
        return await response.Content.ReadFromJsonAsync<List<string>>();
    }

    public async Task<string> GetFragmentByEmbeddedId(Guid embeddedId)
    {
        var request = await _httpClient.GetAsync($"/GetFragmentByEmbeddedId?embeddedId={embeddedId}");
        return await request.Content.ReadAsStringAsync();
    }

    public async Task<List<string>> GetFragmentsByEmbeddedIds(List<Guid> embeddedIds)
    {
        var response = await _httpClient.PostAsJsonAsync("/GetFragmentsByEmbeddedIds", embeddedIds);
        return await response.Content.ReadFromJsonAsync<List<string>>();
    }

    public async Task<List<Guid>> GetAllDocumentIds()
    {
        var documents = await _httpClient.GetFromJsonAsync<List<Model.DocumentWrapper>>("/GetAllDocuments");
        return documents.Select(document => document.Document.Id).ToList();
    }

    public async Task<List<Domain.Model.Fragment>> GetDocumentFragments(Guid documentId)
    {
        var document = await _httpClient.GetFromJsonAsync<Model.FragmentWrapper>(
            $"/GetDocumentById?documentId={documentId}");
        return document.Fragments.Select(fragment =>
            new Domain.Model.Fragment
            {
                Id = fragment.Id,
                Content = fragment.Content,
                DocumentId = fragment.DocumentId
            }
        ).ToList();
    }

    public async Task<List<Guid>> GetDocumentFragmentIds(Guid documentId)
    {
        return await _httpClient.GetFromJsonAsync<List<Guid>>(
            $"/GetDocumentFragmentIds?documentId={documentId}");
    }

    public async Task SetVectorId(Guid fragmentId, Guid vectorId)
    {
        await _httpClient.PostAsync($"/UpdateFragmentVectorId?fragmentId={fragmentId}&vectorId={vectorId}", null);
    }
}