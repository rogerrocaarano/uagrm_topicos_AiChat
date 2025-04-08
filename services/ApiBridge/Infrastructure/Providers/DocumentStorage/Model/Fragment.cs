namespace Infrastructure.Providers.DocumentStorage.Model;

public class Fragment
{
    public Guid Id { get; set; }
    public Guid? VectorId { get; set; }
    public string Content { get; set; }
    public Guid DocumentId { get; set; }
    public int SequenceId { get; set; }
}