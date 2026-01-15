using CustomerOrderSystem.Domain.Exceptions;

namespace CustomerOrderSystem.Domain.Entities
{
    /// <summary>
    /// Representa una orden realizada por un cliente.
    /// </summary>
    public class Order
    {
        public Guid Id { get; private set; }
        public Guid CustomerId { get; private set; }
        public OrderStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private Order() { }

        public Order(Guid customerId)
        {
            Id = Guid.NewGuid();
            CustomerId = customerId;
            Status = OrderStatus.Created;
            CreatedAt = DateTime.UtcNow;
        }

        public void Cancel()
        {
            if (Status != OrderStatus.Created)
                throw new DomainException($"Cannot cancel an order that is already {Status.ToString().ToLower()}.");

            Status = OrderStatus.Cancelled;
        }

        public void Complete()
        {
            if (Status != OrderStatus.Created)
                throw new DomainException($"Cannot complete an order that is already {Status.ToString().ToLower()}.");

            Status = OrderStatus.Completed;
        }
    }
}