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
        /// Recupera una orden específica por su identificador único.
        /// </summary>
        /// <param name="id">ID de la orden.</param>
        /// <response code="200">Retorna la orden encontrada.</response>
        /// <response code="404">Si la orden no existe.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderResponse>> GetById(Guid id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
                return NotFound();

            return Ok(MapToResponse(order));
        }

        /// <summary>
        /// Obtiene el historial de órdenes asociado a un cliente específico.
        /// </summary>
        /// <param name="customerId">ID del cliente.</param>
        /// <response code="200">Retorna la lista de órdenes (vacía si el cliente no tiene).</response>
        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<IEnumerable<OrderResponse>>> GetByCustomerId(Guid customerId)
        {
            var orders = await _orderService.GetOrdersByCustomerAsync(customerId);
            return Ok(orders.Select(MapToResponse));
        }

        /// <summary>
        /// Crea una nueva orden de compra para un cliente existente.
        /// </summary>
        /// <remarks>
        /// La orden se crea inicialmente con estado 'Created'.
        /// </remarks>
        /// <param name="customerId">Identificador único del cliente.</param>
        /// <response code="201">Orden creada exitosamente.</response>
        /// <response code="400">Si el cliente no existe.</response>
        [HttpPost("{customerId}")]
        public async Task<ActionResult<OrderResponse>> Create(Guid customerId)
        {
            var order = await _orderService.CreateOrderAsync(customerId);
            return CreatedAtAction(nameof(GetById), new { id = order.Id }, MapToResponse(order));
        }

        /// <summary>
        /// Cambia el estado de una orden a 'Cancelled'.
        /// </summary>
        /// <param name="orderId">ID de la orden a cancelar.</param>
        /// <response code="204">Orden cancelada con éxito.</response>
        /// <response code="400">Si la orden no existe.</response>
        [HttpPut("{orderId}/cancel")]
        public async Task<IActionResult> Cancel(Guid orderId)
        {
            await _orderService.CancelOrderAsync(orderId);
            return NoContent();
        }

        /// <summary>
        /// Cambia el estado de una orden a 'Completed'.
        /// </summary>
        /// <param name="orderId">ID de la orden a completar.</param>
        /// <response code="204">Orden completada con éxito.</response>
        /// <response code="400">Si la orden no existe.</response>
        [HttpPut("{orderId}/complete")]
        public async Task<IActionResult> Complete(Guid orderId)
        {
            await _orderService.CompleteOrderAsync(orderId);
            return NoContent();
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