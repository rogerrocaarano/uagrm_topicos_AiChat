namespace Infrastructure.Providers.VectorStorage.Models;

public record SimilarityResult(
    Guid FragmentId,
    float Score
);