# Biblioteca

Solución .NET 8 con WebAPI, WinForms y Blazor. Soporta autenticación JWT con permisos por recurso/acción.

## Configuración JWT

Configurar en `appsettings.json` de `Biblioteca.WebAPI`:

```
"JwtSettings": {
  "SecretKey": "<clave-secreta-larga>",
  "Issuer": "Biblioteca.WebAPI",
  "Audience": "Biblioteca.Clients",
  "ExpirationMinutes": 120
}
```

## Usuarios y permisos

- admin (rol `administrador`): acceso total.
- bibliotecario (grupo `bibliotecario`): acceso total por políticas.
- socio: solo lectura de libros/préstamos/autores/géneros/editoriales.

## Correr la solución

- Iniciar `Biblioteca.WebAPI`.
- Iniciar `Biblioteca.UI.Desktop` o Blazor.

## Testing

Proyecto `Biblioteca.WebAPI.Tests`:
- xUnit + WebApplicationFactory.
- EF Core InMemory para DB.
- Ambiente `Testing` evita SQL crudo en Program.cs.

### Ejecutar tests

```
dotnet test
```

### Estructura de tests

- Services: pruebas unitarias de servicios (ej.: `AutorServiceTests`).
- Integration: pruebas de endpoints usando JWT de prueba.

### Semillas de prueba

El `BibliotecaContext` ya hace seeding de entidades base (autores, géneros, editoriales, permisos, usuarios). InMemory las levanta en `EnsureCreated()`.

