using MongoDB.Driver;
using ProductionApi.Models;

namespace ProductionTests
{
    public static class TestMongoDbContext
    {
        public static IMongoCollection<ProductionOrder> CreateTestCollection(out MongoClient client, string connectionString = "mongodb://localhost:27017")
        {
            client = new MongoClient(connectionString);
            var database = client.GetDatabase("TestProductionDb");
            var collection = database.GetCollection<ProductionOrder>("ProductionOrders");

            database.DropCollection("ProductionOrders");

            return collection;
        }
    }
}
