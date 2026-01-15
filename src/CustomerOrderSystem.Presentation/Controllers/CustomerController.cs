using CustomerOrderSystem.Domain.Entities;
using CustomerOrderSystem.Domain.Interfaces;
using CustomerOrderSystem.Application.Services;
using CustomerOrderSystem.Presentation.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CustomerOrderSystem.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        /// <summary>
        /// Obtiene la lista completa de clientes registrados.
        /// </summary>
        /// <response code="200">Retorna la lista de clientes.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerResponse>>> GetAll()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return Ok(customers.Select(MapToResponse));
        }

        /// <summary>
        /// Recupera un cliente específico mediante su identificador único (GUID).
        /// </summary>
        /// <param name="id">Identificador único del cliente.</param>
        /// <response code="200">Si el cliente es encontrado.</response>
        /// <response code="404">Si el cliente no existe.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerResponse>> GetById(Guid id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
                return NotFound();

            return Ok(MapToResponse(customer));
        }

        /// <summary>
        /// Registra un nuevo cliente en el sistema.
        /// </summary>
        /// <remarks>
        /// Ejemplo de petición:
        /// 
        ///     POST /api/Customer
        ///     {
        ///        "name": "Guillermo",
        ///        "email": "guille@test.com"
        ///     }
        /// 
        /// </remarks>
        /// <param name="request">Datos del nuevo cliente.</param>
        /// <response code="201">Cliente creado exitosamente.</response>
        /// <response code="400">Si los datos enviados son inválidos (formato de email o nombre vacío).</response>
        [HttpPost]
        public async Task<ActionResult<CustomerResponse>> Create([FromBody] CustomerCreateRequest request)
        {
            var customer = await _customerService.CreateCustomerAsync(request.Name, request.Email);
            return CreatedAtAction(nameof(GetById), new { id = customer.Id }, MapToResponse(customer));
        }

        private static CustomerResponse MapToResponse(Customer customer)
        {
            return new CustomerResponse
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                Status = customer.Status.ToString(),
                CreatedAt = customer.CreatedAt
            };
        }
    }
}