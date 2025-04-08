namespace DocumentStorage.Models
{
    public class DocumentJsonModel
    {
        public DocumentData Document { get; set; }
        public List<FragmentJsonModel> Fragments { get; set; }
    }

        public class DocumentModel
    {
        public DocumentData Document { get; set; }
    }


    public class DocumentData
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UploadDateTime { get; set; }
    }

    public class FragmentJsonModel
    {
        public string Id { get; set; }
        public string VectorId { get; set; }
        public string Content { get; set; }
        public string DocumentId { get; set; }
        public int SequenceId { get; set; }
    }
}
