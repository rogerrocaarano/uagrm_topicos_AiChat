using Microsoft.AspNetCore.Mvc;

namespace Presentation.Endpoint;

public static class SystemEndpoint
{
    public static void MapSystemEndpoints(this WebApplication app)
    {
        app.MapGet(Constant.Endpoint.HeartbeatVectorStorage, HeartbeatVectorStorage);
        app.MapGet(Constant.Endpoint.HeartbeatDocumentStorage, HeartbeatDocumentStorage);
        app.MapGet(Constant.Endpoint.BuildVectorStorage, BuildVectorStorage);
    }

    private static async Task<IResult> BuildVectorStorage([FromServices] Application.UseCase.SeedVectorStorage useCase)
    {
        await useCase.Execute();
        return Results.Ok();
    }

    private static async Task<IResult> HeartbeatVectorStorage()
    {
        throw new NotImplementedException();
    }

    private static async Task<IResult> HeartbeatDocumentStorage()
    {
        throw new NotImplementedException();
    }
}