using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace CustomerOrderSystem.IntegrationTests
{
    /// <summary>
    /// Tests de integración para CustomerController
    /// Verifican el comportamiento completo del endpoint HTTP
    /// </summary>
    public class CustomerControllerTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public CustomerControllerTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAll_ReturnsEmptyList_WhenNoCustomersExist()
        {
            // Act
            var response = await _client.GetAsync("/api/Customer");

            // Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Assert.Equal("[]", content);
        }

        [Fact]
        public async Task CreateCustomer_ReturnsCreated_WithValidData()
        {
            // Arrange
            var newCustomer = new { name = "Guillermo", email = "guille@test.com" };
            var json = JsonSerializer.Serialize(newCustomer);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/Customer", content);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Contains("Guillermo", responseContent);
            Assert.Contains("guille@test.com", responseContent);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenCustomerDoesNotExist()
        {
            // Arrange
            var nonExistentId = Guid.NewGuid();

            // Act
            var response = await _client.GetAsync($"/api/Customer/{nonExistentId}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task CreateAndRetrieveCustomer_WorksCorrectly()
        {
            // Arrange
            var newCustomer = new { name = "Guillermo", email = "guille.unique@test.com" };
            var json = JsonSerializer.Serialize(newCustomer);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act - Crear cliente
            var createResponse = await _client.PostAsync("/api/Customer", content);
            createResponse.EnsureSuccessStatusCode();

            // Extraer el ID del cliente creado
            var createdCustomer = await createResponse.Content.ReadFromJsonAsync<CustomerResponse>();

            // Act - Obtener cliente por ID
            var getResponse = await _client.GetAsync($"/api/Customer/{createdCustomer!.Id}");

            // Assert
            getResponse.EnsureSuccessStatusCode();
            var retrievedCustomer = await getResponse.Content.ReadFromJsonAsync<CustomerResponse>();

            Assert.Equal("Guillermo", retrievedCustomer!.Name);
            Assert.Equal("guille.unique@test.com", retrievedCustomer.Email);
        }

        // DTO para deserializar la respuesta
        private record CustomerResponse(Guid Id, string Name, string Email);
    }
}
