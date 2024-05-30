using GameSealZadanie.Domain.Entities;

namespace GameSealZadanie.WebAPI.Repositories.Home;

public interface IProducts
{
    Task FetchProductsFromCodesWholeSaleToDatabaseAsync();
    Task<Product?> GetProductByExternalId(string externalId);
}