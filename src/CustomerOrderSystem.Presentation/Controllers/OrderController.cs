using CustomerOrderSystem.Application.Services;
using CustomerOrderSystem.Domain.Entities;
using CustomerOrderSystem.Domain.Interfaces;
using CustomerOrderSystem.Presentation.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CustomerOrderSystem.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;
        private readonly IOrderRepository _orderRepository;

        public OrderController(OrderService orderService, IOrderRepository orderRepository)
        {
            _orderService = orderService;
            _orderRepository = orderRepository;
        }

        /// <summary>
        /// Obtiene una orden por su ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderResponse>> GetById(Guid id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
                return NotFound();

            return Ok(MapToResponse(order));
        }

        /// <summary>
        /// Obtiene todas las órdenes de un cliente
        /// </summary>
        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<IEnumerable<OrderResponse>>> GetByCustomerId(Guid customerId)
        {
            var orders = await _orderRepository.GetByCustomerIdAsync(customerId);
            return Ok(orders.Select(MapToResponse));
        }

        /// <summary>
        /// Crea una nueva orden para un cliente
        /// </summary>
        [HttpPost("{customerId}")]
        public async Task<ActionResult<OrderResponse>> Create(Guid customerId)
        {
            try
            {
                var order = await _orderService.CreateOrderAsync(customerId);
                return CreatedAtAction(nameof(GetById), new { id = order.Id }, MapToResponse(order));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Cancela una orden existente
        /// </summary>
        [HttpPut("{orderId}/cancel")]
        public async Task<IActionResult> Cancel(Guid orderId)
        {
            try
            {
                await _orderService.CancelOrderAsync(orderId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Completa una orden existente
        /// </summary>
        [HttpPut("{orderId}/complete")]
        public async Task<IActionResult> Complete(Guid orderId)
        {
            try
            {
                await _orderService.CompleteOrderAsync(orderId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        private static OrderResponse MapToResponse(Order order)
        {
            return new OrderResponse
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                Status = order.Status.ToString(),
                CreatedAt = order.CreatedAt
            };
        }
    }
}