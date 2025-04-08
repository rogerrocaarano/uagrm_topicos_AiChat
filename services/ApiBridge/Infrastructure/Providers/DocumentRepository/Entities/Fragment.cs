using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Providers.DocumentRepository.Entities;

[Table("Fragments")]
public class Fragment
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Content { get; set; }
    
    [ForeignKey("Document")]
    public Guid DocumentId { get; set; }
    public Document Document { get; set; }
}