namespace Infrastructure.Providers.VectorStorage.Models;

public class FragmentCompareResponse
{
    public List<SimilarityResult> Content { get; set; }
    public DateTime DateTime { get; set; }
}