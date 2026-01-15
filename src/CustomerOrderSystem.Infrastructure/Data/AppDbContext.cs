using CustomerOrderSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerOrderSystem.Infrastructure.Data
{
    /// <summary>
    /// Contexto de base de datos de la aplicacion
    /// </summary>
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Order> Orders => Set<Order>();
    }
}