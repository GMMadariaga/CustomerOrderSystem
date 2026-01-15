using CustomerOrderSystem.Domain.Entities;

namespace CustomerOrderSystem.Domain.Interfaces
{
    /// <summary>
    /// Define el contrato de acceso a datos para Customer
    /// </summary>
    public interface ICustomerRepository
    {
        Task<Customer?> GetByIdAsync(Guid id);
        Task<IEnumerable<Customer>> GetAllAsync();
        Task AddAsync(Customer customer);
        Task UpdateAsync(Customer customer);
    }
}