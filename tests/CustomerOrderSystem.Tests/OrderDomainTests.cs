using CustomerOrderSystem.Domain.Entities;
using CustomerOrderSystem.Domain.Exceptions;

namespace CustomerOrderSystem.Tests
{
    public class OrderDomainTests
    {
        [Fact]
        public void Cancel_WhenStatusIsCreated_ChangesStatusToCancelled()
        {
            // Arrange
            var order = new Order(Guid.NewGuid());

            // Act
            order.Cancel();

            // Assert
            Assert.Equal(OrderStatus.Cancelled, order.Status);
        }

        [Fact]
        public void Cancel_WhenStatusIsCompleted_ThrowsDomainException()
        {
            // Arrange
            var order = new Order(Guid.NewGuid());
            order.Complete();

            // Act & Assert
            var ex = Assert.Throws<DomainException>(() => order.Cancel());
            Assert.Contains("Cannot cancel an order that is already completed", ex.Message);
        }

        [Fact]
        public async Task Cancel_WhenStatusIsAlreadyCancelled_ThrowsDomainException()
        {
            // Arrange
            var order = new Order(Guid.NewGuid());
            order.Cancel();

            // Act & Assert
            var ex = Assert.Throws<DomainException>(() => order.Cancel());
            Assert.Contains("Cannot cancel an order that is already cancelled", ex.Message);
        }

        [Fact]
        public void Complete_WhenStatusIsCreated_ChangesStatusToCompleted()
        {
            // Arrange
            var order = new Order(Guid.NewGuid());

            // Act
            order.Complete();

            // Assert
            Assert.Equal(OrderStatus.Completed, order.Status);
        }

        [Fact]
        public void Complete_WhenStatusIsCancelled_ThrowsDomainException()
        {
            // Arrange
            var order = new Order(Guid.NewGuid());
            order.Cancel();

            // Act & Assert
            var ex = Assert.Throws<DomainException>(() => order.Complete());
            Assert.Contains("Cannot complete an order that is already cancelled", ex.Message);
        }
    }
}
