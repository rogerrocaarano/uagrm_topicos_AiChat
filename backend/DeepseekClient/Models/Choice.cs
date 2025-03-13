namespace DeepseekClient.Models;

public record Choice(
    int Index,
    Message Message,
    string FinishReason);