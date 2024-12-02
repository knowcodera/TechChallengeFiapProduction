using Microsoft.AspNetCore.Mvc;
using ProductionApi.Models;
using ProductionApi.Repositories;
using ProductionApi.Services;

namespace ProductionApi.Controllers
{
    [ApiController]
    [Route("v1/production")]
    public class ProductionController : ControllerBase
    {
        private readonly IProductionRepository _repository;
        private readonly IRabbitMQPublisher _publisher;

        public ProductionController(IProductionRepository repository, IRabbitMQPublisher publisher)
        {
            _repository = repository;
            _publisher = publisher;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductionOrders() =>
            Ok(await _repository.GetProductionOrdersAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductionOrderById(string id)
        {
            var order = await _repository.GetProductionOrderByIdAsync(id);
            if (order == null)
                return NotFound();
            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductionOrder([FromBody] ProductionOrder order)
        {
            order.CreatedAt = DateTime.UtcNow;
            await _repository.CreateProductionOrderAsync(order);

            var message = $"Production order for OrderId {order.OrderId} created";
            _publisher.Publish("production_queue", message);

            return CreatedAtAction(nameof(GetProductionOrderById), new { id = order.Id }, order);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateProductionOrderStatus(string id, [FromBody] string status)
        {
            await _repository.UpdateProductionOrderStatusAsync(id, status);

            var message = $"Production order {id} updated to status {status}";
            _publisher.Publish("production_queue", message);

            return NoContent();
        }
    }
}
