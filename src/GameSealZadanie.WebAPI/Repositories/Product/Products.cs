using GameSealZadanie.Domain.Context;
using GameSealZadanie.Domain.Entities;
using GameSealZadanie.Domain.Responses;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace GameSealZadanie.WebAPI.Repositories.Home;

public class Products(IHttpClientFactory httpClientFactory, IConfiguration configuration, DBContext context) : IProducts
{
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
    private readonly IConfiguration _configuration = configuration;
    private readonly DBContext _context = context;
    private const string _Route = "https://api.codeswholesale.com";

    #region Publics

    //Metoda pobiera wszystkie produkty udostępnione przez CodesWholeSale,
    //następnie usuwa wszystkie istniejące produkty w bazie, w celu późniejszego uzupełnienia tabel
    public async Task FetchProductsFromCodesWholeSaleToDatabaseAsync()
    {
        var products = await GetAllProductsFromCodesWholeSaleAsync();
        var existingProducts = await GetAllProductsFromDatabaseAsync();

        if (products is not null && products?.Items?.Count > 0)
        {
            //Usuwanie wszystkich wpisów, aby dodać je później na nowo
            if (existingProducts is not null)
            {
                _context.Product.RemoveRange(existingProducts);
            }

            var newProductList = new List<Product>();

            foreach (var product in products.Items)
            {
                var badges = product?.Badges?.Select(x => new Badge()
                {
                    Name = x.Name ?? string.Empty,
                    Slug = x.Slug ?? string.Empty,
                }).ToList();

                var images = product?.Images?.Select(x => new Image()
                {
                    Format = x.Format ?? string.Empty,
                    ImageUrl = x.Image ?? string.Empty
                }).ToList();

                var languages = product?.Languages?.Select(x => new Language()
                {
                    LanguageName = x
                }).ToList();

                var links = product?.Links?.Select(x => new Link()
                {
                    Deprecation = x.Deprecation ?? string.Empty,
                    Href = x.Href ?? string.Empty,
                    Hreflang = x.Hreflang ?? string.Empty,
                    Media = x.Media ?? string.Empty,
                    Rel = x.Rel ?? string.Empty,
                    Templated = x.Templated ?? default,
                    Title = x.Title ?? string.Empty,
                    Type = x.Type ?? string.Empty
                }).ToList();

                var prices = product?.Prices?.Select(x => new Price()
                {
                    From = x.From ?? default,
                    To = x.To,
                    Value = x.Value ?? default
                }).ToList();

                var regions = product?.Regions?.Select(x => new Region()
                {
                    RegionName = x
                }).ToList();

                newProductList.Add(new Product()
                {
                    Identifier = product?.Identifier,
                    Name = product?.Name,
                    Platform = product?.Platform,
                    Quantity = product?.Quantity ?? default,
                    ExternalProductId = product?.ProductId ?? string.Empty,
                    Badges = badges ?? [],
                    Images = images ?? [],
                    Languages = languages ?? [],
                    Links = links ?? [],
                    Prices = prices ?? [],
                    Regions = regions ?? []
                });
            }

            await _context.Product.AddRangeAsync(newProductList);
            await _context.SaveChangesAsync();

            return;
        }
    }

    //Metoda pobiera szczegółowe informacje odnośnie produktu
    //szukając produktu po polu "ProductId" modelu przesłanego przez CodesWholeSale
    public async Task<Product?> GetProductByExternalId(string externalId) =>
        await _context.Product
            .Include(x => x.Badges)
            .Include(x => x.Images)
            .Include(x => x.Languages)
            .Include(x => x.Links)
            .Include(x => x.Prices)
            .Include(x => x.Regions)
            .FirstOrDefaultAsync(x => x.ExternalProductId == externalId);

    #endregion Publics

    #region Privates

    //Metoda pobierająca ważny token od CodesWholeSale
    private async Task<TokenResponse?> GetAccessTokenAsync()
    {
        var clientId = _configuration.GetSection("Client:ClientID").Value;
        var clientSecret = _configuration.GetSection("Client:ClientSecret").Value;

        var parameters = new List<string>
        {
            $"grant_type=client_credentials",
            $"client_id={clientId}",
            $"&client_secret={clientSecret}"
        };

        string queryString = string.Join("&", parameters);
        string url = $"{_Route}/oauth/token?" + queryString;

        var httpClient = _httpClientFactory.CreateClient();

        var response = await httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        var deserialisedResponse = JsonConvert.DeserializeObject<TokenResponse>(responseContent);

        return deserialisedResponse;
    }

    //Metoda pobierająca wszystkie produkty z CodesWholeSale
    private async Task<GetProductsResponse?> GetAllProductsFromCodesWholeSaleAsync()
    {
        string url = $"{_Route}/v2/products";
        var token = await GetAccessTokenAsync();

        var httpClient = _httpClientFactory.CreateClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token?.Access_token);

        var response = await httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        var products = JsonConvert.DeserializeObject<GetProductsResponse>(responseContent);

        return products;
    }

    //Metoda pobierająca wszystkie produkty z bazy danych
    private async Task<List<Product>?> GetAllProductsFromDatabaseAsync()
        => await _context.Product
            .Include(x => x.Badges)
            .Include(x => x.Images)
            .Include(x => x.Languages)
            .Include(x => x.Links)
            .Include(x => x.Prices)
            .Include(x => x.Images)
            .ToListAsync();

    #endregion Privates
}