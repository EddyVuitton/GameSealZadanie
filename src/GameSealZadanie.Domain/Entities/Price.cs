using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameSealZadanie.Domain.Entities;

public class Price
{
    [Key]
    public int Id { get; set; }
    [ForeignKey("Product")]
    public int ProductId { get; set; }
    public int From { get; set; }
    public int? To { get; set; }
    public decimal Value { get; set; }
}