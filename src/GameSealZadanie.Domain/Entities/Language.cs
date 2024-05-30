using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameSealZadanie.Domain.Entities;

public class Language
{
    [Key]
    public int Id { get; set; }
    [ForeignKey("Product")]
    public int ProductId { get; set; }
    public string LanguageName { get; set; } = null!;
}