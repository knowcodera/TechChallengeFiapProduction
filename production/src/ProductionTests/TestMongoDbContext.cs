using MongoDB.Driver;
using ProductionApi.Models;

namespace ProductionTests
{
    public static class TestMongoDbContext
    {
        public static IMongoCollection<ProductionOrder> CreateTestCollection(out MongoClient client)
        {
            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("The connection string for MongoDB is not configured.");
            }

            client = new MongoClient(connectionString);
            var database = client.GetDatabase("TestProductionDb");
            var collection = database.GetCollection<ProductionOrder>("ProductionOrders");

            database.DropCollection("ProductionOrders");

            return collection;
        }
    }
}
