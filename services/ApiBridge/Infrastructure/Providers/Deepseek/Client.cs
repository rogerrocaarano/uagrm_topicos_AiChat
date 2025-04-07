using Infrastructure.Providers.Deepseek.Builder;
using Infrastructure.Providers.Deepseek.Constant;
using Infrastructure.Providers.Deepseek.Dto;
using Infrastructure.Providers.Deepseek.Model;
using RestSharp;

namespace Infrastructure.Providers.Deepseek;

public class Client : IDisposable, Domain.Service.ILlmChat
{
    private readonly RestClient _client;
    private readonly string _apiKey;

    public Client(string apiKey)
    {
        var options = new RestClientOptions(ApiEndpoint.BaseUrl);
        _client = new RestClient(options);
        _apiKey = apiKey;
    }

    public async Task<ChatCompletionResponse?> PostCompletionChat(ChatCompletionRequest request)
    {
        try
        {
            var restRequest = new RestRequest(ApiEndpoint.ChatCompletions, Method.Post);
            var headers = new List<KeyValuePair<string, string>>
            {
                KeyValuePair.Create("Content-Type", ContentType.Json.Value),
                KeyValuePair.Create("Authorization", $"Bearer {_apiKey}")
            };
            restRequest.AddHeaders(headers);
            restRequest.AddJsonBody(request);

            var restResponse = await _client.PostAsync<ChatCompletionResponse>(restRequest);
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

    public async Task<string> AskLlmChat(Domain.Model.Conversation conversation, List<Domain.Model.Message> context)
    {
        var request = ChatCompletionBuilder.BuildRequest(conversation, context);
        var response = await PostCompletionChat(request);
        return response.Choices[0].Message.Content;
    }
}