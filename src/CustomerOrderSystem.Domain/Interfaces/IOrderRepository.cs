using CustomerOrderSystem.Domain.Entities;

namespace CustomerOrderSystem.Domain.Interfaces
{
    /// <summary>
    /// Define el contrato de acceso a datos para Order
    /// </summary>
    public interface IOrderRepository
    {
        Task<Order?> GetByIdAsync(Guid id);
        Task<IEnumerable<Order>> GetByCustomerIdAsync(Guid customerId);
        Task AddAsync(Order order);
        Task UpdateAsync(Order order);
    }
}