# Casos de Prueba - CustomerOrderSystem

Guía rápida para validar el comportamiento de la API. Se recomienda usar *swagger* o *postman* para ejecutar estas pruebas.

---

## Clientes

### 1. Crear un cliente
- **POST** `/api/Customer`
- Body: `{ "name": "Guillermo", "email": "guille@test.com" }`
- Espero: `201 Created` con el ID generado y status "Active".

### 2. Crear cliente sin nombre
- **POST** `/api/Customer`
- Body: `{ "name": "", "email": "guille@test.com" }`
- Espero: `400 Bad Request` con mensaje "Customer name cannot be empty."

### 3. Crear cliente con email inválido
- **POST** `/api/Customer`
- Body: `{ "name": "Guillermo", "email": "email-sin-formato" }`
- Espero: `400 Bad Request` con mensaje "Invalid email format."

### 4. Crear cliente con email duplicado
- Primero crear un cliente, luego intentar crear otro con el mismo email.
- Espero: `400 Bad Request` con mensaje "A customer with this email already exists."

### 5. Listar todos los clientes
- **GET** `/api/Customer`
- Espero: `200 OK` con la lista de clientes.

### 6. Buscar cliente que no existe
- **GET** `/api/Customer/{id-que-no-existe}`
- Espero: `404 Not Found`.

---

## Órdenes

### 7. Crear orden para cliente existente
- **POST** `/api/Order/{customerId}`
- Espero: `201 Created` con estado "Created".

### 8. Crear orden para cliente que no existe
- **POST** `/api/Order/{id-aleatorio}`
- Espero: `400 Bad Request` con mensaje "Customer not found."

### 9. Cancelar una orden
- **PUT** `/api/Order/{orderId}/cancel`
- Espero: `204 No Content`. La orden queda con estado "Cancelled".

### 10. Completar una orden
- **PUT** `/api/Order/{orderId}/complete`
- Espero: `204 No Content`. La orden queda con estado "Completed".

### 11. Cancelar orden ya completada
- Primero completar una orden, luego intentar cancelarla.
- Espero: `400 Bad Request` con mensaje indicando que no se puede cancelar.

### 12. Completar orden ya cancelada
- Primero cancelar una orden, luego intentar completarla.
- Espero: `400 Bad Request` con mensaje indicando que no se puede completar.

---

## Errores del Sistema

### 13. Error interno (500)
- Si ocurre un error inesperado en el servidor, el middleware devuelve:
- `500 Internal Server Error` con mensaje "An internal server error has occurred."
- El detalle del error queda en los logs, no se expone al cliente.
