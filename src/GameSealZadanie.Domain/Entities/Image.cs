using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameSealZadanie.Domain.Entities;

public class Image
{
    [Key]
    public int Id { get; set; }
    [ForeignKey("Product")]
    public int ProductId { get; set; }
    public string Format { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
}