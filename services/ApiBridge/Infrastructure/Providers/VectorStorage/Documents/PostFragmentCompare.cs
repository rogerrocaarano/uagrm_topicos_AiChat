namespace Infrastructure.Providers.VectorStorage.Documents;

public record PostFragmentCompare(
    string Fragment,
    int MaxMatches
);