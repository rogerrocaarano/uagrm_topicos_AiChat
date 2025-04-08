namespace Infrastructure.Providers.DocumentStorage.Model;

public class FragmentWrapper
{
    public DocumentWrapper Document { get; set; }
    public List<Fragment> Fragments { get; set; }
}