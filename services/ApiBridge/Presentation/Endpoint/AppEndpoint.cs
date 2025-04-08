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
        if (request.Messages == null)
        {
            request.Messages = new List<Domain.Model.Message>();
        }

        var conversation = new Domain.Model.Conversation(request.Messages, request.Question);
        conversation = await useCase.Execute(conversation);
        var responseContent = new AskLlmResponse
        {
            Question = conversation.AnsweredQuestions.Last().Item1,
            Answer = conversation.AnsweredQuestions.Last().Item2
        };
        var response = new Domain.Model.ApiResponse(responseContent);
        return Results.Ok(response);
    }
}