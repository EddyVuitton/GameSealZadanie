using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameSealZadanie.Domain.Entities;

public class Badge
{
    [Key]
    public int Id { get; set; }
    [ForeignKey("Product")]
    public int ProductId { get; set; }
    public string Name { get; set; } = null!;
    public string Slug { get; set; } = null!;
}