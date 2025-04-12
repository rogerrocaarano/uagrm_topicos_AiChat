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
                string baseUrl = configuration["Services:DocumentStorage:BaseUrl"] ??
                                 throw new InvalidOperationException();
                return new Providers.DocumentStorage.Client(baseUrl);
            }
        );
        
        services.AddSingleton<IVectorStorageService, Providers.VectorStorage.Client>(provider =>
            {
                string baseUrl = configuration["Services:VectorStorage:BaseUrl"] ??
                                 throw new InvalidOperationException();
                return new Providers.VectorStorage.Client(baseUrl);
            }
        );

        services.AddSingleton<ILlmChatService>(provider =>
            {
                string apiKey = configuration["Services:LlmChat:ApiKey"] ??
                                throw new InvalidOperationException();
                return new Providers.Deepseek.Client(apiKey);
            }
        );

        return services;
    }

    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<AskLlm>(provider =>
        {
            var llm = provider.GetRequiredService<ILlmChatService>();
            var documents = provider.GetRequiredService<IDocumentStorageService>();
            var embeddings = provider.GetRequiredService<IVectorStorageService>();
            return new AskLlm(llm, documents, embeddings);
        });

        services.AddScoped<SeedVectorStorage>(provider =>
        {
            var documents = provider.GetRequiredService<IDocumentStorageService>();
            var embeddings = provider.GetRequiredService<IVectorStorageService>();
            return new SeedVectorStorage(documents, embeddings);
        });

        return services;
    }
}