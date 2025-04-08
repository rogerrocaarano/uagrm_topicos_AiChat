using System.Text.Json.Serialization;

namespace Infrastructure.Providers.VectorStorage.Documents;

public class PostFragmentCompare
{
    [JsonPropertyName("fragment")] public string Fragment { get; set; }
    [JsonPropertyName("max_matches")] public int MaxMatches { get; set; }
}