using ProductionApi.Models;

namespace ProductionApi.Repositories
{
    public interface IProductionRepository
    {
        Task<IEnumerable<ProductionOrder>> GetProductionOrdersAsync();
        Task<ProductionOrder> GetProductionOrderByIdAsync(string id);
        Task CreateProductionOrderAsync(ProductionOrder order);
        Task UpdateProductionOrderStatusAsync(string id, string status);
    }
}
