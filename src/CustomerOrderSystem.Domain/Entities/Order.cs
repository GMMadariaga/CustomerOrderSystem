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
            Status = OrderStatus.Cancelled;
        }

        public void Complete()
        {
            Status = OrderStatus.Completed;
        }
    }
}