using Infrastructure.Providers.VectorStorage.Constant;
using Infrastructure.Providers.VectorStorage.Documents;
using Infrastructure.Providers.VectorStorage.Models;
using RestSharp;

namespace Infrastructure.Providers.VectorStorage;

public class Client : IDisposable, Domain.Service.IVectorStorageService
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
        var request = new RestRequest(Endpoint.FragmentIngest, Method.Post)
            .AddHeader("Content-Type", "application/json")
            .AddJsonBody(jsonBody);

        var response = await _client.PostAsync<ApiResponse>(request);
        return response.Content.Id;
    }

    public async Task<List<Guid>> GetSimilarEmbeddingIds(string content)
    {
        var jsonBody = new PostFragmentCompare
        {
            Fragment = content,
            MaxMatches = 20
        };
        var request = new RestRequest(Endpoint.FragmentCompare, Method.Post)
            .AddHeader("Content-Type", "application/json")
            .AddJsonBody(jsonBody);
        var response = await _client.PostAsync<Domain.Model.ApiResponse>(request);
        var similarityResults = SortSimilarityResultsByScore((List<SimilarityResult>)response.Content);
        return similarityResults.Select(result => result.FragmentId).ToList();
    }

    private List<SimilarityResult> SortSimilarityResultsByScore(List<SimilarityResult> similarityResults)
    {
        return similarityResults.OrderByDescending(result => result.Score).ToList();
    }

    public async Task<List<string>> GetFragmentsFromText(string content)
    {
        var jsonBody = new TextProcesors.PostSplitter(content);
        var request = new RestRequest(Endpoint.TextSplitter, Method.Post)
            .AddHeader("Content-Type", "application/json")
            .AddJsonBody(jsonBody);

        var response = await _client.PostAsync<Domain.Model.ApiResponse>(request);
        var fragments = (List<string>)response.Content;
        return fragments;
    }

    public void Dispose()
    {
        _client?.Dispose();
        GC.SuppressFinalize(this);
    }
}