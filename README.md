# DeliveryApi — Resumen de Progreso

## Stack
- **ASP.NET Core** (net10.0)
- **SQLite** con Entity Framework Core
- **Patrón Repositorio**
- **JWT** (pendiente)
- **AutoMapper** (pendiente)

## Paquetes instalados
```bash
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
dotnet add package System.IdentityModel.Tokens.Jwt
dotnet add package BCrypt.Net-Next
dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection
```

---

## Estructura de carpetas actual
```
DeliveryApi/
├── Controllers/
│   ├── PruebaController.cs        ✅ (solo para testing, eliminar en prod)
│   ├── ProductosController.cs     ✅
│   └── PedidosController.cs       ✅
├── Models/
│   ├── Usuario.cs                 ✅
│   ├── Cliente.cs                 ✅
│   ├── Direccion.cs               ✅
│   ├── Producto.cs                ✅
│   ├── Pedido.cs                  ✅
│   ├── DetallePedido.cs           ✅
│   └── EstadoPedido.cs            ✅
├── DTOs/
│   ├── Productos/
│   │   ├── ProductoDto.cs         ✅
│   │   └── CrearProductoDto.cs    ✅
│   └── Pedidos/
│       ├── CrearPedidoDto.cs      ✅
│       ├── PedidoResponseDto.cs   ✅
│       └── ActualizarEstadoDto.cs ✅
├── Repositories/
│   ├── Interfaces/
│   │   ├── IGenericRepository.cs  ✅
│   │   ├── IUsuarioRepository.cs  ✅
│   │   ├── IProductoRepository.cs ✅
│   │   └── IPedidoRepository.cs   ✅
│   └── Implementations/
│       ├── UsuarioRepository.cs   ✅
│       ├── ProductoRepository.cs  ✅
│       └── PedidoRepository.cs    ✅
├── Services/
│   ├── Interfaces/
│   │   ├── IUsuarioService.cs     ✅
│   │   ├── IProductoService.cs    ✅
│   │   └── IPedidoService.cs      ✅
│   └── Implementations/
│       ├── UsuarioService.cs      ✅
│       ├── ProductoService.cs     ✅
│       └── PedidoService.cs       ✅
├── Data/
│   ├── AppDbContext.cs            ✅
│   ├── pedidos.db                 ✅
│   └── Migrations/                ✅
├── appsettings.json               ✅
└── Program.cs                     ✅
```

---

## Lo que ya funciona
- ✅ Base de datos SQLite creada con migraciones
- ✅ Seed de 6 estados de pedido (Pendiente, Confirmado, En Preparación, En Camino, Entregado, Cancelado)
- ✅ CRUD de productos (`GET`, `POST`, `PUT`, `DELETE`, búsqueda por nombre)
- ✅ Creación de pedidos con cálculo automático de subtotales y total
- ✅ Cambio de estado de pedido (`PATCH /api/pedidos/{id}/estado`)
- ✅ Consulta de pedido con detalles (`GET /api/pedidos/{id}`)

---

## Endpoints disponibles

### Prueba (eliminar en producción)
```
POST   /api/prueba/setup      → crea usuario, cliente y productos de prueba
GET    /api/prueba/resumen    → cuenta registros en cada tabla
DELETE /api/prueba/limpiar    → limpia toda la BD
```

### Productos
```
GET    /api/productos                    → lista todos
GET    /api/productos/{id}               → busca por id
GET    /api/productos/buscar?nombre=xyz  → busca por nombre
POST   /api/productos                    → crea producto
PUT    /api/productos/{id}               → actualiza producto
DELETE /api/productos/{id}               → elimina producto
```

### Pedidos
```
GET    /api/pedidos           → lista todos
GET    /api/pedidos/{id}      → detalle con productos y estado
POST   /api/pedidos           → crea pedido
PATCH  /api/pedidos/{id}/estado → cambia estado
```

---

## Pendiente

### 1. Repositorios y servicios faltantes
- `IClienteRepository` + `ClienteRepository`
- `IClienteService` + `ClienteService`
- (Direcciones es opcional por ahora)

### 2. JWT — Autenticación
- Configurar `JwtSettings` en `appsettings.json`
- Crear `IAuthService` + `AuthService` (genera y valida tokens)
- Crear `AuthController` con endpoints `POST /auth/register` y `POST /auth/login`
- Proteger endpoints con `[Authorize]`
- Reemplazar `usuarioId = 1` hardcodeado en `PedidosController` por el claim del token

### 3. DTOs de Auth
- `RegisterDto`
- `LoginRequestDto`
- `LoginResponseDto` (devuelve el token)

### 4. Hash de passwords
- Usar `BCrypt.Net-Next` en `AuthService` al registrar y verificar usuarios

### 5. Quitar `IgnoreCycles` de `Program.cs`
- Una vez todos los controladores retornen DTOs en vez de modelos directamente

### 6. AutoMapper (opcional pero recomendado)
- Configurar perfiles de mapeo para evitar mapeo manual en controladores

---

## Notas importantes

### Ciclos circulares en JSON
```csharp
// Program.cs — temporal mientras desarrollamos
builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.ReferenceHandler =
            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);
```
> Quitar esto cuando todos los endpoints retornen DTOs.

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

### El usuarioId está hardcodeado
```csharp
// PedidosController.cs línea ~61
// Reemplazar con el claim del JWT cuando esté implementado
var pedido = await _service.CreateAsync(dto.ClienteId, 1, ...);
```

---

## Comandos útiles
```bash
dotnet watch run                              # correr en modo desarrollo
dotnet build                                  # compilar
dotnet ef migrations add NombreMigracion      # nueva migración
dotnet ef database update                     # aplicar migraciones
pkill -f dotnet                               # matar procesos dotnet
lsof -i :5140                                 # ver qué usa el puerto
```