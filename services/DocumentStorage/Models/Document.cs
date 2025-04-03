namespace PuebaTopicosSpacy.Models
{
    public class Document
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime UploadDateTime { get; set; } = DateTime.UtcNow;

        public ICollection<Fragment> Fragments { get; set; }
    }
}
