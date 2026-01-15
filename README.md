# CustomerOrderSystem 

Este es un proyecto API backend diseñado para gestionar clientes y sus ordenes, aplicando conceptos de arquitectura limpia, separación de responsabilidades, escalabilidad, y mantenibilidad, siguiendo los lineamientos de un buen diseño de software.

El objetivo del proyecto es exponer una base sólida y bien estructurada para operaciones comunes como la creación de clientes, la gestión de órdenes y la aplicación de reglas de negocio.

## Tecnologías Utilizadas

- Lenguaje: C#
- Framework: .NET 6
- Base de Datos: InMemoryDatabase (desarrollo), SQL Server (próximamente en producción)
- ORM: Entity Framework Core
- Patrón de Arquitectura: Arquitectura Limpia (Clean Architecture)
- Control de Versiones: Git

## Estructura del Proyecto

El proyecto está organizado en varias capas para asegurar una clara separación de responsabilidades:
- **Domain**: Contiene las entidades del dominio, interfaces de repositorios y servicios, y reglas de negocio.
- **Application**: Implementa los casos de uso, servicios de aplicación e identidades.     
- **Infrastructure**: Implementa la persistencia de datos, servicios externos y configuraciones específicas de infraestructura.
- **Presentation**: Exposición de los endpoints en controladores y configuración de la API.    
- **Tests**: Pruebas unitarias e integrales.

## Como ejecutar el proyecto localmente:
1. Clonar el repositorio
2. Abrir en Visual Studio Code
3. Restaurar dependencias: `dotnet restore`
4. Ejecutar: `dotnet run --project src/CustomerOrderSystem.Presentation`
5. Abrir en el navegador: https://localhost:7188/swagger

## Ejecutar tests
 
`dotnet test`
 
## Endpoints

### Customer
- `GET /api/Customer` - Obtiene todos los clientes
- `GET /api/Customer/{id}` - Obtiene un cliente por su ID
- `POST /api/Customer` - Crea un nuevo cliente

### Order
- `POST /api/Order/{customerId}` - Crea una nueva orden para un cliente
- `PUT /api/Order/{orderId}/cancel` - Cancela una orden existente

## Pendientes

- Tests de integración
- Endpoints de Order en el controlador
- Configuración para SQL Server en producción

## Ejemplo de uso

```json
{
  "description": "Ejemplos de requests para la API",
  "createCustomer": {
    "method": "POST",
    "url": "/api/Customer",
    "body": {
      "name": "Guillermo Madariaga",
      "email": "guille@test.com"
    }
  },
  "getCustomers": {
    "method": "GET",
    "url": "/api/Customer"
  },
  "getCustomerById": {
    "method": "GET",
    "url": "/api/Customer/{id}"
  },
  "createOrder": {
    "method": "POST",
    "url": "/api/Order/{customerId}",
    "description": "Crear una nueva orden para un cliente"
  },
  "cancelOrder": {
    "method": "PUT",
    "url": "/api/Order/{orderId}/cancel",
    "description": "Cancelar una orden existente"
  }
}
```