using Microsoft.AspNetCore.Mvc;
using Presentation.Dto.App;

namespace Presentation.Endpoint;

public static class AppEndpoint
{
    public static void MapAppEndpoints(this WebApplication app)
    {
        app.MapPost(Constant.Endpoint.AskLlm, AskLlm);
    }

    private static async Task<IResult> AskLlm(AskLlmRequest request, [FromServices] Application.UseCase.AskLlm useCase)
    {
        var conversation = new Domain.Model.Conversation(request.Messages, request.Question);
        conversation = await useCase.Execute(conversation);
        var response = new Domain.Model.ApiResponse(conversation);
        return Results.Ok(response);
    }
}