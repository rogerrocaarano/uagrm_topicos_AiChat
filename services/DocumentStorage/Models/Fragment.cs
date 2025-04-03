namespace DocumentStorage.Models
{
    public class Fragment
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid? VectorId { get; set; }
        public string Content { get; set; }
        public Guid DocumentId { get; set; }
        public int SequenceId { get; set; }

        public Document Document { get; set; }
    }
}
