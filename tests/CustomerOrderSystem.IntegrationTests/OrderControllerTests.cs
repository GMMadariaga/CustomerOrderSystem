using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace CustomerOrderSystem.IntegrationTests
{
    /// <summary>
    /// Tests de integración para OrderController
    /// Verifican el flujo completo de creación y gestión de órdenes
    /// </summary>
    public class OrderControllerTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public OrderControllerTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CreateOrder_ReturnsBadRequest_WhenCustomerDoesNotExist()
        {
            // Arrange
            var nonExistentCustomerId = Guid.NewGuid();

            // Act
            var response = await _client.PostAsync($"/api/Order/{nonExistentCustomerId}", null);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateOrder_ReturnsCreated_WhenCustomerExists()
        {
            // Arrange - Primero crear un cliente
            var newCustomer = new { name = "Carlos López", email = "carlos@test.com" };
            var customerJson = JsonSerializer.Serialize(newCustomer);
            var customerContent = new StringContent(customerJson, Encoding.UTF8, "application/json");

            var customerResponse = await _client.PostAsync("/api/Customer", customerContent);
            customerResponse.EnsureSuccessStatusCode();
            var createdCustomer = await customerResponse.Content.ReadFromJsonAsync<CustomerResponse>();

            // Act - Crear orden para el cliente
            var orderResponse = await _client.PostAsync($"/api/Order/{createdCustomer!.Id}", null);

            // Assert
            Assert.Equal(HttpStatusCode.Created, orderResponse.StatusCode);
            var order = await orderResponse.Content.ReadFromJsonAsync<OrderResponse>();
            Assert.Equal(createdCustomer.Id, order!.CustomerId);
            Assert.Equal("Created", order.Status);
        }

        [Fact]
        public async Task CancelOrder_ReturnsNoContent_WhenOrderExists()
        {
            // Arrange - Crear cliente y orden
            var newCustomer = new { name = "Ana Martínez", email = "ana@test.com" };
            var customerJson = JsonSerializer.Serialize(newCustomer);
            var customerContent = new StringContent(customerJson, Encoding.UTF8, "application/json");

            var customerResponse = await _client.PostAsync("/api/Customer", customerContent);
            var createdCustomer = await customerResponse.Content.ReadFromJsonAsync<CustomerResponse>();

            var orderResponse = await _client.PostAsync($"/api/Order/{createdCustomer!.Id}", null);
            var createdOrder = await orderResponse.Content.ReadFromJsonAsync<OrderResponse>();

            // Act - Cancelar la orden
            var cancelResponse = await _client.PutAsync($"/api/Order/{createdOrder!.Id}/cancel", null);

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, cancelResponse.StatusCode);
        }

        [Fact]
        public async Task GetOrdersByCustomer_ReturnsOrders_WhenOrdersExist()
        {
            // Arrange - Crear cliente y orden
            var newCustomer = new { name = "Roberto Díaz", email = "roberto@test.com" };
            var customerJson = JsonSerializer.Serialize(newCustomer);
            var customerContent = new StringContent(customerJson, Encoding.UTF8, "application/json");

            var customerResponse = await _client.PostAsync("/api/Customer", customerContent);
            var createdCustomer = await customerResponse.Content.ReadFromJsonAsync<CustomerResponse>();

            await _client.PostAsync($"/api/Order/{createdCustomer!.Id}", null);

            // Act - Obtener órdenes del cliente
            var getOrdersResponse = await _client.GetAsync($"/api/Order/customer/{createdCustomer.Id}");

            // Assert
            getOrdersResponse.EnsureSuccessStatusCode();
            var orders = await getOrdersResponse.Content.ReadFromJsonAsync<List<OrderResponse>>();
            Assert.NotEmpty(orders!);
        }

        // DTOs para deserializar las respuestas
        private record CustomerResponse(Guid Id, string Name, string Email);
        private record OrderResponse(Guid Id, Guid CustomerId, string Status, DateTime CreatedAt);
    }
}
