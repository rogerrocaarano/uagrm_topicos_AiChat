using Infrastructure.Api.Deepseek.Models;
using RestSharp;

namespace Infrastructure.Api.Deepseek;

public class Client : IDisposable
{
    private RestClient _client;
    private readonly string _apiKey;

    public Client(string apiKey)
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

            Console.WriteLine("Sending request to Deepseek API...");
            var restResponse = await _client.PostAsync<ChatCompletionResponse>(restRequest);
            if (restResponse is not null)
            {
                Console.WriteLine("Received response from Deepseek API.");
            }

            return restResponse;
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