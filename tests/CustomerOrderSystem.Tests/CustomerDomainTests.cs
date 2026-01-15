using CustomerOrderSystem.Domain.Entities;
using CustomerOrderSystem.Domain.Exceptions;

namespace CustomerOrderSystem.Tests
{
    public class CustomerDomainTests
    {
        [Fact]
        public void CreateCustomer_WithEmptyName_ThrowsDomainException()
        {
            // Act & Assert
            var ex = Assert.Throws<DomainException>(() => new Customer("", "test@test.com"));
            Assert.Equal("Customer name cannot be empty.", ex.Message);
        }

        [Fact]
        public void CreateCustomer_WithInvalidEmail_ThrowsDomainException()
        {
            // Act & Assert
            var ex = Assert.Throws<DomainException>(() => new Customer("Guillermo", "email-invalido"));
            Assert.Equal("Invalid email format.", ex.Message);
        }

        [Fact]
        public void CreateCustomer_WithValidData_CreatesCustomer()
        {
            // Act
            var customer = new Customer("Guillermo", "guille@test.com");

            // Assert
            Assert.NotNull(customer);
            Assert.Equal("Guillermo", customer.Name);
            Assert.Equal("guille@test.com", customer.Email);
            Assert.Equal(CustomerStatus.Active, customer.Status);
        }
    }
}
