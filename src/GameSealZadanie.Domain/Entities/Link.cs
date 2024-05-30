using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameSealZadanie.Domain.Entities;

public class Link
{
    [Key]
    public int Id { get; set; }
    [ForeignKey("Product")]
    public int ProductId { get; set; }
    public string Deprecation { get; set; } = null!;
    public string Href { get; set; } = null!;
    public string Hreflang { get; set; } = null!;
    public string Media { get; set; } = null!;
    public string Rel { get; set; } = null!;
    public bool Templated { get; set; }
    public string Title { get; set; } = null!;
    public string Type { get; set; } = null!;
}