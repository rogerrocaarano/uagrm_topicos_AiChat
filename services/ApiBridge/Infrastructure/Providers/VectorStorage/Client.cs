using Domain.Model;
using Domain.Repository;
using Infrastructure.Providers.VectorStorage.Documents;
using Infrastructure.Providers.VectorStorage.Models;
using RestSharp;

namespace Infrastructure.Providers.VectorStorage;

public class Client : IDisposable, IEmbeddingRepository
{
    private readonly RestClient _client;

    public Client(string apiBaseUrl)
    {
        var options = new RestClientOptions(apiBaseUrl);
        _client = new RestClient(options);
    }

    public async Task<Guid> SaveEmbedding(string content)
    {
        var jsonBody = new PostFragmentIngest(content, "test");
        var request = new RestRequest("/documents/fragment-ingest", Method.Post)
            .AddHeader("Content-Type", "application/json")
            .AddJsonBody(jsonBody);

        var response = await _client.PostAsync<ApiResponse>(request);
        var fragment = (Fragment)response.Content;
        return fragment.Id;
    }

    public async Task<List<Guid>> GetSimilarEmbeddingIds(string content)
    {
        var jsonBody = new PostFragmentCompare(content, 5);
        var request = new RestRequest("/documents/fragment-compare", Method.Post)
            .AddHeader("Content-Type", "application/json")
            .AddJsonBody(jsonBody);
        
        var response = await _client.PostAsync<ApiResponse>(request);
        var fragments = (List<Fragment>)response.Content;
        return fragments.Select(f => f.Id).ToList();
    }

    public void Dispose()
    {
        _client?.Dispose();
        GC.SuppressFinalize(this);
    }
}