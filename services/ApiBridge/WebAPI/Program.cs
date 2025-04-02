using DeepseekClient.Models;
using WebAPI.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


var deepseekApiKey = builder.Configuration["Deepseek:ApiKey"];
if (string.IsNullOrEmpty(deepseekApiKey))
{
    throw new Exception("Missing Deepseek__ApiKey configuration.");
}
var deepseekClient = new DeepseekClient.DeepseekClient(deepseekApiKey);



app.MapPost("/chat", async (ChatRequest request) =>
{
    Console.WriteLine("/chat[POST] :: Received request.");
    if (!request.Messages.Last().IsUserMessage)
    {
        return Results.BadRequest(new ApiResponse("Last message must be from user.", false));
    }

    if (request.Messages.Any(message => message.Text is null or ""))
    {
        return Results.BadRequest(new ApiResponse("A message cannot be empty.", false));
    }

    var messages = new List<Message> { new Message("system", "Eres un asistente amable y conciso.") };
    messages.AddRange(request.Messages.Select(message => 
            message.IsUserMessage ? 
                new Message("user", message.Text) : 
                new Message("assistant", message.Text))
    );
    
    var completionRequest = new ChatCompletionRequest(messages);
    var response = await deepseekClient.PostCompletionChat(completionRequest);
    
    if (response is null)
    {
        return Results.BadRequest(new ApiResponse("An error occurred while processing the request.", false));
    }
    
    Console.WriteLine("/chat[POST] :: Sending response.");
    return Results.Ok(new ApiResponse(new ChatMessage(response.Choices[0].Message.Content, false), true));
});

app.Run();
