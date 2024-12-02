using MongoDB.Driver;
using ProductionApi.Models;

namespace ProductionApi.Data
{
    public class ProductionContext
    {
        private readonly IMongoDatabase _database;

        public ProductionContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["MongoDB:ConnectionString"]);
            _database = client.GetDatabase(configuration["MongoDB:DatabaseName"]);
        }

        public IMongoCollection<ProductionOrder> ProductionOrders =>
            _database.GetCollection<ProductionOrder>("ProductionOrders");
    }
}
