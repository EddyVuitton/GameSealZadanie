namespace GameSealZadanie.Domain.Responses;

public class TokenResponse
{
    public string? Access_token { get; set; }
    public string? Token_type { get; set; }
    public int? Expires_in { get; set; }
}