namespace Infrastructure.Api.Deepseek.Models;

public record Message(
    string Role,
    string Content);