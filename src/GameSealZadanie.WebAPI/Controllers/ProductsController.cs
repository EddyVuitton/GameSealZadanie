using GameSealZadanie.Domain.Entities;
using GameSealZadanie.Domain.Http;
using GameSealZadanie.WebAPI.Helpers;
using GameSealZadanie.WebAPI.Repositories.Home;
using Microsoft.AspNetCore.Mvc;

namespace GameSealZadanie.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController(IProducts homeBusinessLogic, ILogger<ProductsController> logger) : Controller
{
    private readonly IProducts _homeBusinessLogic = homeBusinessLogic;
    private readonly ILogger _logger = logger;

    [HttpPost("FetchProductsFromCodesWholeSaleToDatabaseAsync")]
    public async Task<HttpResult> FetchProductsFromCodesWholeSaleToDatabaseAsync()
    {
        try
        {
            await _homeBusinessLogic.FetchProductsFromCodesWholeSaleToDatabaseAsync();

            return HttpHelper.Ok("Pomyślnie pobrano produkty");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Wystąpił błąd w pobraniu produktu");
            return HttpHelper.Error(ex);
        }
    }

    [HttpGet("GetProductByExternalId")]
    public async Task<HttpResultT<Product?>> GetProductByExternalId(string externalId)
    {
        try
        {
            var result = await _homeBusinessLogic.GetProductByExternalId(externalId);

            return HttpHelper.Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Wystąpił błąd w znalezieniu produktu");
            return HttpHelper.Error<Product?>(ex);
        }
    }
}