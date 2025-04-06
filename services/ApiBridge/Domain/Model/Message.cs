namespace Domain.Model;

public class Message
{
    public required string Type { get; set; }
    public required string Text { get; set; }
    public DateTime? SendTime { get; set; }
    public List<Guid> ContextDbIds { get; set; } = [];
};