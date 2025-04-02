namespace Infrastructure.Providers.Deepseek.Models;

public record Message(
    string Role,
    string Content
);