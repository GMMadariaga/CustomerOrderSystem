using CustomerOrderSystem.Domain.Exceptions;

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
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Customer name cannot be empty.");

            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
                throw new DomainException("Invalid email format.");

            Id = Guid.NewGuid();
            Name = name;
            Email = email.ToLowerInvariant();
            Status = CustomerStatus.Active;
            CreatedAt = DateTime.UtcNow;
        }
    }
}