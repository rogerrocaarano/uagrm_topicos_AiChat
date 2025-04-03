namespace Infrastructure.Providers.VectorStorage.Documents;

public record PostFragmentIngest(
    string Fragment,
    string DocumentName
);