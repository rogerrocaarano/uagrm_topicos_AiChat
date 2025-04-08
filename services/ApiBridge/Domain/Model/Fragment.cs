namespace Domain.Model;

public class Fragment
{
    public Guid Id { get; set; }
    public required string Content { get; set; }
    public required Guid DocumentId { get; set; }
}