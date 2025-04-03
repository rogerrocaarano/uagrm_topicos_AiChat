namespace Domain.Model;

public record Message(
    string Type,
    string Content
)
{
    public DateTime TimeSend { get; private set; } = DateTime.UtcNow;
}