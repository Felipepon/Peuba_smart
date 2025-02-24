# 🏨 TravelAgencyAPI - Prueba Técnica Backend

API para gestión de hoteles, habitaciones y reservas, desarrollada en C# (.NET 8) con arquitectura limpia y patrones DDD.

## 🛠️ Tecnologías Utilizadas
- **.NET 8** | **Entity Framework Core** | **MySQL**
- **MediatR** (CQRS) | **FluentValidation** | **AutoMapper**
- **JWT Authentication** | **Swagger/OpenAPI**
- **Clean Architecture** | **Repository Pattern** | **Unit of Work**

## 📋 Requisitos
- [.NET SDK 8.0](https://dotnet.microsoft.com/download)
- [MySQL Server](https://dev.mysql.com/downloads/) o Docker
- [Visual Studio 2022](https://visualstudio.microsoft.com/) o VS Code

## 🚀 Instalación
1. Clonar repositorio:
   ```bash
   git clone https://github.com/tu-usuario/Peuba_smart.git
   cd Peuba_smart
2. Configurar base de datos en appsettings.json:
json 
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=TravelAgencyDB;User=root;Password=tu_contraseña;"
}
3. Ejecutar Migraciones 
    ```bash
    dotnet ef database update --project src/TravelAgency.Infrastructure

## 🔑 Autenticación
-Genera tokens JWT para pruebas: 
  ```bash
  POST /api/auth/login
  Body: "Agent"  # Rol: Agent (Gestión) | Traveler (Reservas)

## 📚 Documentación de Endpoints
Accede a Swagger UI en desarrollo:  
**http://localhost:5886/swagger**

| Método | Ruta                         | Descripción                   | Rol Requerido |
|--------|----------------------------- |-------------------------------|---------------|
| **POST** | `/api/hotels`              | Crear hotel                   | Agent         |
| **POST** | `/api/hotels/{id}/rooms`   | Agregar habitación al hotel   | Agent         |
| **GET**  | `/api/hotels/search`       | Buscar hoteles disponibles    | Traveler      |
| **POST** | `/api/bookings`            | Crear reserva                 | Traveler      |

## 🧪 Ejecutar Pruebas
- **Unitarias**: `dotnet test`



 