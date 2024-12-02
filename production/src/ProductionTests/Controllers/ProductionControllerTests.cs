using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductionApi.Controllers;
using ProductionApi.Models;
using ProductionApi.Repositories;
using ProductionApi.Services;

namespace ProductionTests.Controllers
{
    public class ProductionControllerTests
    {
        [Fact]
        public async Task GetProductionOrders_ShouldReturnAllOrders()
        {
            // Arrange
            var mockRepo = new Mock<IProductionRepository>();
            mockRepo.Setup(repo => repo.GetProductionOrdersAsync())
                    .ReturnsAsync(new List<ProductionOrder>
                    {
                        new ProductionOrder { OrderId = 1, Status = "Pending" },
                        new ProductionOrder { OrderId = 2, Status = "Completed" }
                    });

            var mockPublisher = new Mock<IRabbitMQPublisher>();
            var controller = new ProductionController(mockRepo.Object, mockPublisher.Object);

            // Act
            var result = await controller.GetProductionOrders();
            var okResult = Assert.IsType<OkObjectResult>(result);
            var orders = Assert.IsAssignableFrom<IEnumerable<ProductionOrder>>(okResult.Value);

            // Assert
            Assert.Equal(2, orders.Count());
        }

        [Fact]
        public async Task CreateProductionOrder_ShouldReturnCreatedOrder()
        {
            // Arrange
            var mockRepo = new Mock<IProductionRepository>();
            var mockPublisher = new Mock<IRabbitMQPublisher>();

            var order = new ProductionOrder
            {
                OrderId = 1,
                CreatedAt = DateTime.UtcNow,
                Status = "Pending",
                Items = new List<string> { "Burger", "Fries" }
            };

            var controller = new ProductionController(mockRepo.Object, mockPublisher.Object);

            // Act
            var result = await controller.CreateProductionOrder(order);
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);

            // Assert
            Assert.Equal("GetProductionOrderById", createdAtActionResult.ActionName);
            Assert.Equal(order, createdAtActionResult.Value);
            mockPublisher.Verify(p => p.Publish(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task UpdateProductionOrderStatus_ShouldReturnNoContent()
        {
            // Arrange
            var mockRepo = new Mock<IProductionRepository>();
            mockRepo.Setup(repo => repo.GetProductionOrderByIdAsync(It.IsAny<string>()))
                    .ReturnsAsync(new ProductionOrder { Id = "1", Status = "Pending" });

            var mockPublisher = new Mock<IRabbitMQPublisher>();
            var controller = new ProductionController(mockRepo.Object, mockPublisher.Object);

            // Act
            var result = await controller.UpdateProductionOrderStatus("1", "Completed");

            // Assert
            Assert.IsType<NoContentResult>(result);
            mockPublisher.Verify(p => p.Publish(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}
