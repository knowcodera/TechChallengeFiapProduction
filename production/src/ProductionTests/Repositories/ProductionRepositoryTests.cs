using ProductionApi.Models;
using ProductionApi.Repositories;

namespace ProductionTests.Repositories
{
    public class ProductionRepositoryTests
    {
        [Fact]
        public async Task CreateProductionOrderAsync_ShouldAddOrderToDatabase()
        {
            // Arrange
            var collection = TestMongoDbContext.CreateTestCollection(out _);
            var repository = new ProductionRepository(collection);

            var order = new ProductionOrder
            {
                OrderId = 1,
                CreatedAt = DateTime.UtcNow,
                Status = "Pending",
                Items = new List<string> { "Burger", "Fries" }
            };

            // Act
            await repository.CreateProductionOrderAsync(order);
            var orders = await repository.GetProductionOrdersAsync();

            // Assert
            Assert.Single(orders);
            Assert.Equal("Pending", orders.First().Status);
            Assert.Equal(2, orders.First().Items.Count);
        }

        [Fact]
        public async Task GetProductionOrderByIdAsync_ShouldReturnCorrectOrder()
        {
            // Arrange
            var collection = TestMongoDbContext.CreateTestCollection(out _);
            var repository = new ProductionRepository(collection);

            var order = new ProductionOrder
            {
                OrderId = 1,
                CreatedAt = DateTime.UtcNow,
                Status = "Pending",
                Items = new List<string> { "Burger", "Fries" }
            };

            await repository.CreateProductionOrderAsync(order);

            // Act
            var retrievedOrder = await repository.GetProductionOrderByIdAsync(order.Id);

            // Assert
            Assert.NotNull(retrievedOrder);
            Assert.Equal("Pending", retrievedOrder.Status);
        }

        [Fact]
        public async Task UpdateProductionOrderStatusAsync_ShouldUpdateOrderStatus()
        {
            // Arrange
            var collection = TestMongoDbContext.CreateTestCollection(out _);
            var repository = new ProductionRepository(collection);

            var order = new ProductionOrder
            {
                OrderId = 1,
                CreatedAt = DateTime.UtcNow,
                Status = "Pending"
            };

            await repository.CreateProductionOrderAsync(order);

            // Act
            await repository.UpdateProductionOrderStatusAsync(order.Id, "Completed");
            var updatedOrder = await repository.GetProductionOrderByIdAsync(order.Id);

            // Assert
            Assert.NotNull(updatedOrder);
            Assert.Equal("Completed", updatedOrder.Status);
        }
    }
}
