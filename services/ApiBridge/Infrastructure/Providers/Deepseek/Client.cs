using Domain.Model;
using Domain.Service;
using Infrastructure.Api.Deepseek.Models;
using Infrastructure.Providers.Deepseek.Builder;
using Infrastructure.Providers.Deepseek.Models;
using RestSharp;
using Message = Domain.Model.Message;

namespace Infrastructure.Providers.Deepseek;

public class Client : IDisposable, ILlmChat
{
    private readonly RestClient _client;
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

    public async Task<string> AskLlmChat(Conversation conversation, List<Message> context)
    {
        var request = ChatCompletionBuilder.BuildRequest(conversation, context);
        var response = await PostCompletionChat(request);
        return response.Choices[0].Message.Content;
    }
}