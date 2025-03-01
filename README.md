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
  Genera tokens JWT para pruebas: 
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

## 🏗️ Despliegue con Docker
  Para desplegar la aplicación usando contenedores Docker (API + MySQL):

  1. Asegúrate de tener Docker Desktop y Docker Compose instalados.
  2. En la raíz del proyecto (donde se encuentra docker-compose.yml), ejecuta:
      ```bash
      docker compose up --build

  3. Cuando finalice el build, Docker levantará:
    Un contenedor MySQL configurado con la base de datos TravelAgencyDB.
    Un contenedor API que se mapeará en el puerto especificado en docker-compose.yml (por ejemplo, 5086 u 5886).
  
  4. Accede a la aplicación en tu navegador:
      ```bash
      http://localhost:5086/swagger



 