namespace Domain.Model;

public class Document
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required List<Fragment> Fragments { get; set; }
}