namespace Infrastructure.Providers.Deepseek.Model;

public record Message(
    string Role,
    string Content
);