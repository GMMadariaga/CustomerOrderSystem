using CustomerOrderSystem.Domain.Entities;
using CustomerOrderSystem.Domain.Interfaces;

namespace CustomerOrderSystem.Application.Services
{
    /// <summary>
    /// Servicio de aplicación para operaciones de clientes
    /// </summary>
    public class CustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _customerRepository.GetAllAsync();
        }

        public async Task<Customer?> GetCustomerByIdAsync(Guid id)
        {
            return await _customerRepository.GetByIdAsync(id);
        }

        public async Task<Customer> CreateCustomerAsync(string name, string email)
        {
            var customer = new Customer(name, email);
            await _customerRepository.AddAsync(customer);
            return customer;
        }
    }
}
