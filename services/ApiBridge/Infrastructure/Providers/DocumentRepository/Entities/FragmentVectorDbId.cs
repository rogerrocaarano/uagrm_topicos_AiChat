namespace Infrastructure.Providers.DocumentRepository.Entities;

public class FragmentVectorDbId
{
    public Guid FragmentId { get; set; }
    public Fragment Fragment { get; set; }
    public Guid ExternalId { get; set; }
}