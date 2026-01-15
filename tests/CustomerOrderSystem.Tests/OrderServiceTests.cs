using CustomerOrderSystem.Application.Services;
using CustomerOrderSystem.Domain.Entities;
using CustomerOrderSystem.Domain.Interfaces;
using CustomerOrderSystem.Domain.Exceptions;
using Moq;

namespace CustomerOrderSystem.Tests
{
    public class OrderServiceTests
    {
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly Mock<ICustomerRepository> _customerRepositoryMock;
        private readonly OrderService _orderService;

        public OrderServiceTests()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _customerRepositoryMock = new Mock<ICustomerRepository>();
            _orderService = new OrderService(
                _orderRepositoryMock.Object,
                _customerRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateOrderAsync_WithValidCustomer_ReturnsOrder()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var customer = new Customer("Guillermo", "guille@test.com");
            
            _customerRepositoryMock
                .Setup(r => r.GetByIdAsync(customerId))
                .ReturnsAsync(customer);

            // Act
            var result = await _orderService.CreateOrderAsync(customerId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(customerId, result.CustomerId);
            Assert.Equal(OrderStatus.Created, result.Status);
        }

        [Fact]
        public async Task CreateOrderAsync_WithInvalidCustomer_ThrowsException()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            
            _customerRepositoryMock
                .Setup(r => r.GetByIdAsync(customerId))
                .ReturnsAsync((Customer?)null);

            // Act & Assert
            await Assert.ThrowsAsync<DomainException>(
                () => _orderService.CreateOrderAsync(customerId));
        }

        [Fact]
        public async Task CancelOrderAsync_WithValidOrder_CancelsOrder()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var order = new Order(customerId);
            var orderId = order.Id;

            _orderRepositoryMock
                .Setup(r => r.GetByIdAsync(orderId))
                .ReturnsAsync(order);

            // Act
            await _orderService.CancelOrderAsync(orderId);

            // Assert
            Assert.Equal(OrderStatus.Cancelled, order.Status);
        }

        [Fact]
        public async Task CompleteOrderAsync_WithValidOrder_CompletesOrder()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var order = new Order(customerId);
            var orderId = order.Id;

            _orderRepositoryMock
                .Setup(r => r.GetByIdAsync(orderId))
                .ReturnsAsync(order);

            // Act
            await _orderService.CompleteOrderAsync(orderId);

            // Assert
            Assert.Equal(OrderStatus.Completed, order.Status);
        }
    }
}