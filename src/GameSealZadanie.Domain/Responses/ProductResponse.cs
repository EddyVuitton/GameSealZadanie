namespace GameSealZadanie.Domain.Responses;

public class ProductResponse
{
    public string? ProductId { get; set; }
    public string? Identifier { get; set; }
    public string? Name { get; set; }
    public string? Platform { get; set; }
    public int? Quantity { get; set; }
    public List<ImageResponse>? Images { get; set; }
    public List<string>? Regions { get; set; }
    public List<string>? Languages { get; set; }
    public List<BadgeResponse>? Badges { get; set; }
    public List<PriceResponse>? Prices { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public List<LinkResponse>? Links { get; set; }
}