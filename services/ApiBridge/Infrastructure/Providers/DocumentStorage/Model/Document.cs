namespace Infrastructure.Providers.DocumentStorage.Model;

public class Document
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    // public DateTime UploadDateTime { get; set; }
}