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
    public async Task<ActionResult> FetchProductsFromCodesWholeSaleToDatabaseAsync()
    {
        try
        {
            await _homeBusinessLogic.FetchProductsFromCodesWholeSaleToDatabaseAsync();
            return Ok("Pomyślnie pobrano produkty");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Wystąpił błąd w pobraniu produktów");
            return Problem(ex.Message);
        }
    }

    [HttpGet("GetProductByExternalId")]
    public async Task<ActionResult> GetProductByExternalId(string externalId)
    {
        try
        {
            var result = await _homeBusinessLogic.GetProductByExternalId(externalId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Wystąpił błąd w znalezieniu produktu");
            return Problem(ex.Message);
        }
    }
}