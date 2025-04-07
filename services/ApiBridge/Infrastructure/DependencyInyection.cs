using Application.UseCase;
using Domain.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IDocumentStorageService, Providers.DocumentStorage.Client>(provider =>
            {
                const string baseUrl = ""; // TODO: Reemplazar con la configuración de la API Base URL
                return new Providers.DocumentStorage.Client(baseUrl);
            }
            );
        
        services.AddSingleton<IEmbeddingService, Providers.VectorStorage.Client>(provider =>
            {
                const string baseUrl = ""; // TODO: Reemplazar con la configuración de la API Base URL
                return new Providers.VectorStorage.Client(baseUrl);
            }
        );

        services.AddSingleton<IllmChatService>(provider =>
            {
                const string apiKey = ""; // TODO: Reemplazar con la configuración de la API Key
                return new Providers.Deepseek.Client(apiKey);
            }
        );

        return services;
    }

    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<AskLlm>(provider =>
        {
            var llm = provider.GetRequiredService<IllmChatService>();
            var documents = provider.GetRequiredService<IDocumentStorageService>();
            var embeddings = provider.GetRequiredService<IEmbeddingService>();
            return new AskLlm(llm, documents, embeddings);
        });

        return services;
    }
}