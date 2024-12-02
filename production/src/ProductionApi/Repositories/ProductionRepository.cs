using MongoDB.Driver;
using ProductionApi.Models;

namespace ProductionApi.Repositories
{
    public class ProductionRepository : IProductionRepository
    {
        private readonly IMongoCollection<ProductionOrder> _collection;

        public ProductionRepository(IMongoCollection<ProductionOrder> collection)
        {
            _collection = collection;
        }

        public async Task<IEnumerable<ProductionOrder>> GetProductionOrdersAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<ProductionOrder> GetProductionOrderByIdAsync(string id)
        {
            return await _collection.Find(order => order.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateProductionOrderAsync(ProductionOrder order)
        {
            await _collection.InsertOneAsync(order);
        }

        public async Task UpdateProductionOrderStatusAsync(string id, string status)
        {
            var update = Builders<ProductionOrder>.Update.Set(o => o.Status, status);
            await _collection.UpdateOneAsync(order => order.Id == id, update);
        }
    }
}
