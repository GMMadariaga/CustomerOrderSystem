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
        /// Obtiene todos los clientes
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAll()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return Ok(customers);
        }

        /// <summary>
        /// Obtiene un cliente por ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetById(Guid id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        /// <summary>
        /// Crea un nuevo cliente
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Customer>> Create([FromBody] CustomerCreateRequest request)
        {
            var customer = await _customerService.CreateCustomerAsync(request.Name, request.Email);
            return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
        }
    }
 
}