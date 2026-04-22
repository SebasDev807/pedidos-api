# DeliveryApi — Resumen de Progreso

## Stack
- **ASP.NET Core** (net10.0)
- **SQLite** con Entity Framework Core
- **Clean Architecture** ✅
- **JWT** ✅
- **BCrypt** para hash de passwords ✅

## Paquetes instalados
```bash
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
dotnet add package System.IdentityModel.Tokens.Jwt
dotnet add package BCrypt.Net-Next
dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection
dotnet add package DotNetEnv
```

---

## Arquitectura

El proyecto fue migrado a **Clean Architecture**, separando responsabilidades en cuatro capas principales:

- **Domain** — Entidades del negocio e interfaces de repositorios. No tiene dependencias externas.
- **Application** — DTOs y casos de uso (servicios). Solo depende de Domain.
- **Infrastructure** — Implementación de repositorios, contexto de base de datos y configuración. Depende de Domain.
- **Controllers** — Punto de entrada HTTP. Orquesta Application e Infrastructure.

---

## Estructura de carpetas

```
DeliveryApi/
└── Src/
    ├── Application/
    │   ├── DTOs/
    │   │   ├── Auth/
    │   │   │   ├── LoginRequestDto.cs
    │   │   │   ├── LoginResponseDto.cs
    │   │   │   └── RegisterDto.cs
    │   │   ├── Clientes/
    │   │   │   ├── ClienteDto.cs
    │   │   │   └── CrearDireccionDto.cs
    │   │   ├── Pedidos/
    │   │   │   ├── ActualizarEstadoDto.cs
    │   │   │   ├── CrearPedidoDto.cs
    │   │   │   └── PedidoResponseDto.cs
    │   │   └── Productos/
    │   │       ├── CrearProductoDto.cs
    │   │       └── ProductoDto.cs
    │   └── UseCases/
    │       ├── Implementations/
    │       │   ├── AuthService.cs
    │       │   ├── ClienteService.cs
    │       │   ├── PedidoService.cs
    │       │   └── ProductoService.cs
    │       └── Interfaces/
    │           ├── IAuthService.cs
    │           ├── IClienteService.cs
    │           ├── IPedidoService.cs
    │           └── IProductoService.cs
    ├── Controllers/
    │   ├── AuthController.cs
    │   ├── ClienteController.cs
    │   ├── PedidosController.cs
    │   ├── ProductosController.cs
    │   └── PruebaController.cs
    ├── Domain/
    │   ├── Entities/
    │   │   ├── Cliente.cs
    │   │   ├── DetallePedido.cs
    │   │   ├── Direccion.cs
    │   │   ├── EstadoPedido.cs
    │   │   ├── Pedido.cs
    │   │   ├── Producto.cs
    │   │   └── Usuario.cs
    │   └── Interfaces/
    │       ├── IClienteRepository.cs
    │       ├── IDireccionRepository.cs
    │       ├── IGenericRepository.cs
    │       ├── IPedidoRepository.cs
    │       ├── IProductoRepository.cs
    │       └── IUsuarioRepository.cs
    ├── Exceptions/
    │   ├── ConflictException.cs
    │   └── NotFoundException.cs
    └── Infrastructure/
        ├── Config/
        │   └── JwtSettings.cs
        ├── Persistence/
        │   ├── AppDbContext.cs
        │   └── Migrations/
        └── Repositories/
            ├── ClienteRepository.cs
            ├── DireccionRepository.cs
            ├── PedidoRepository.cs
            ├── ProductoRepository.cs
            └── UsuarioRepository.cs
```

---

## Endpoints disponibles

### Auth (público)
```
POST   /api/auth/register     → registra usuario y crea perfil de cliente automáticamente
POST   /api/auth/login        → devuelve JWT token
```

### Productos (requiere token)
```
GET    /api/productos                     → lista todos
GET    /api/productos/{id}                → busca por id
GET    /api/productos/buscar?nombre=xyz   → busca por nombre
POST   /api/productos                     → crea producto
PUT    /api/productos/{id}                → actualiza producto
DELETE /api/productos/{id}                → elimina producto
```

### Pedidos (requiere token)
```
GET    /api/pedidos                → lista todos con detalles y estado
GET    /api/pedidos/{id}           → detalle completo
POST   /api/pedidos                → crea pedido (calcula totales automáticamente)
PATCH  /api/pedidos/{id}/estado    → actualiza estado del pedido
```

### Clientes (requiere token)
```
GET    /api/clientes/perfil                      → perfil del cliente autenticado
POST   /api/clientes/direcciones                 → agrega dirección
DELETE /api/clientes/direcciones/{direccionId}   → elimina dirección
```

### Prueba (eliminar en producción)
```
POST   /api/prueba/setup    → crea datos de prueba
GET    /api/prueba/resumen  → cuenta registros en cada tabla
DELETE /api/prueba/limpiar  → limpia toda la BD
```

---

## Estados del pedido
| Id | Estado |
|---|---|
| 1 | Pendiente |
| 2 | Confirmado |
| 3 | En Preparación |
| 4 | En Camino |
| 5 | Entregado |
| 6 | Cancelado |

---

## Variables de entorno (.env)
```
JWT_KEY=esta-es-mi-clave-secreta-super-larga-minimo-32-caracteres
JWT_ISSUER=DeliveryApi
JWT_AUDIENCE=DeliveryApiUsers
JWT_EXPIRE_MINUTES=60
```

---

## Lo que ya funciona ✅
- Registro y Login con JWT
- Hash de passwords con BCrypt
- Perfil de cliente creado automáticamente al registrarse
- CRUD completo de productos con búsqueda por nombre
- Creación de pedidos con cálculo automático de subtotales y total
- Ver pedidos con detalles, estado y productos
- Actualizar estado del pedido
- Gestión de direcciones por cliente
- Endpoints protegidos con `[Authorize]`
- **Migración a Clean Architecture** — separación por capas Domain / Application / Infrastructure / Controllers

---

## Pendiente ⬜

### 1. Manejo global de errores
- Crear `Middleware/ExceptionMiddleware.cs`
- Captura todas las excepciones no controladas
- Devuelve JSON consistente en vez de HTML

### 2. Validaciones en DTOs
- Usar `DataAnnotations` para validar campos
```csharp
[Required]
[EmailAddress]
public string Email { get; set; } = null!;

[MinLength(6)]
public string Password { get; set; } = null!;

[Range(0.01, double.MaxValue)]
public decimal Precio { get; set; }
```

### 3. Limpieza de código
- Quitar `IgnoreCycles` de `Program.cs`
- Eliminar `PruebaController.cs`
- Obtener `clienteId` automáticamente desde el token en `PedidosController`

### 4. AutoMapper (opcional)
- Eliminar el mapeo manual en controladores y servicios

---

## Notas importantes

### Puerto ocupado
```bash
pkill -f dotnet
dotnet watch run
```

### BD bloqueada al migrar
```bash
# Detener el servidor primero, luego:
dotnet ef migrations add NombreMigracion
dotnet ef database update
dotnet watch run
```

### clienteId en el body del pedido
El `clienteId` aún viene en el body — pendiente obtenerlo automáticamente
desde el token JWT igual que se hace con `usuarioId` en `PedidosController`.

---

## Comandos útiles
```bash
dotnet watch run                          # correr en modo desarrollo
dotnet build                              # compilar
dotnet ef migrations add NombreMigracion  # nueva migración
dotnet ef database update                 # aplicar migraciones
pkill -f dotnet                           # matar procesos dotnet
lsof -i :5140                             # ver qué usa el puerto
```