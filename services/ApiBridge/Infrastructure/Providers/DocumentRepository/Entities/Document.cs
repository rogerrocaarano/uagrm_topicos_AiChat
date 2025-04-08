using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Providers.DocumentRepository.Entities;

[Table("Documents")]
public class Document
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Name { get; set; }
    
    public ICollection<Fragment> Fragments { get; set; } = new List<Fragment>();
}