namespace CustomerOrderSystem.Domain.Entities
{
    /// <summary>
    /// Representa un cliente del negocio
    /// </summary>
    public class Customer
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public CustomerStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private Customer() { }

        public Customer(string name, string email)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            Status = CustomerStatus.Active;
            CreatedAt = DateTime.UtcNow;
        }
    }
}