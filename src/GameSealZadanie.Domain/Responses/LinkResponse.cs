namespace GameSealZadanie.Domain.Responses;

public class LinkResponse
{
    public string? Productid { get; set; } = null!;
    public string? Deprecation { get; set; } = null!;
    public string? Href { get; set; } = null!;
    public string? Hreflang { get; set; } = null!;
    public string? Media { get; set; } = null!;
    public string? Rel { get; set; } = null!;
    public bool? Templated { get; set; }
    public string? Title { get; set; } = null!;
    public string? Type { get; set; } = null!;
}