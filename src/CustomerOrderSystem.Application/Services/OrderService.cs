using CustomerOrderSystem.Domain.Entities;
using CustomerOrderSystem.Domain.Interfaces;
using CustomerOrderSystem.Domain.Exceptions;

namespace CustomerOrderSystem.Application.Services
{
    /// <summary>
    /// Servicio de aplicacion para operaciones de ordenes
    /// </summary>
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;

        public OrderService(IOrderRepository orderRepository, ICustomerRepository customerRepository)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
        }

        public async Task<IEnumerable<Order>> GetOrdersByCustomerAsync(Guid customerId)
        {
            var customer = await _customerRepository.GetByIdAsync(customerId);
            if (customer == null)
                throw new DomainException("Customer not found.");

            return await _orderRepository.GetByCustomerIdAsync(customerId);
        }

        /// <summary>
        /// Crea una nueva orden para un cliente
        /// </summary>
        public async Task<Order> CreateOrderAsync(Guid customerId)
        {
            var customer = await _customerRepository.GetByIdAsync(customerId);
            if (customer == null)
                throw new DomainException("Customer not found.");

            var order = new Order(customerId);
            await _orderRepository.AddAsync(order);
            return order;
        }

        /// <summary>
        /// Cancela una orden existente
        /// </summary>
        public async Task CancelOrderAsync(Guid orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null)
                throw new DomainException("Order not found.");

            order.Cancel();
            await _orderRepository.UpdateAsync(order);
        }

        /// <summary>
        /// Completa una orden existente
        /// </summary>
        public async Task CompleteOrderAsync(Guid orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null)
                throw new DomainException("Order not found.");

            order.Complete();
            await _orderRepository.UpdateAsync(order);
        }
    }
}