using System.ComponentModel.DataAnnotations;

namespace GameSealZadanie.Domain.Entities;

public class Product
{
    [Key]
    public int Id { get; set; }
    public string? Identifier { get; set; }
    public string? Name { get; set; }
    public string? Platform { get; set; }
    public int Quantity { get; set; }
    public string ExternalProductId { get; set; } = null!;

    public virtual List<Badge> Badges { get; set; } = null!;
    public virtual List<Image> Images { get; set; } = null!;
    public virtual List<Language> Languages { get; set; } = null!;
    public virtual List<Link> Links { get; set; } = null!;
    public virtual List<Price> Prices { get; set; } = null!;
    public virtual List<Region> Regions { get; set; } = null!;
}