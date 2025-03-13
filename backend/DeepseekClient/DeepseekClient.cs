using DeepseekClient.Models;
using RestSharp;

namespace DeepseekClient;

public class DeepseekClient : IDisposable
{
    private RestClient _client;
    private readonly string _apiKey;

    public DeepseekClient(string apiKey)
    {
        var options = new RestClientOptions("https://api.deepseek.com/");
        _client = new RestClient(options);
        _apiKey = apiKey;
    }
    
    public async Task<ChatCompletionResponse?> PostCompletionChat(ChatCompletionRequest request)
    {
        try
        {
            var restRequest = new RestRequest("/chat/completions", Method.Post);
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddHeader("Authorization", "Bearer " + _apiKey);
            restRequest.AddJsonBody(request);

            return await _client.PostAsync<ChatCompletionResponse>(restRequest);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
        
    }
    
    public void Dispose()
    {
        _client?.Dispose();
        GC.SuppressFinalize(this);
    }
}