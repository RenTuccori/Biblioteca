using Biblioteca.Data;
using Biblioteca.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Biblioteca.DTOs;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Configurar Entity Framework con proveedor según ambiente
if (builder.Environment.IsEnvironment("Testing"))
{
    builder.Services.AddDbContext<BibliotecaContext>(options =>
        options.UseInMemoryDatabase("Biblioteca_TestDB"));
}
else
{
    builder.Services.AddDbContext<BibliotecaContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
}

// Registrar repositorios
builder.Services.AddScoped<AutorRepository>();
builder.Services.AddScoped<GeneroRepository>();
builder.Services.AddScoped<LibroRepository>();
builder.Services.AddScoped<EditorialRepository>();
builder.Services.AddScoped<PersonaRepository>();
builder.Services.AddScoped<UsuarioRepository>();
builder.Services.AddScoped<PrestamoRepository>();

// Registrar servicios
builder.Services.AddScoped<AutorService>();
builder.Services.AddScoped<GeneroService>();
builder.Services.AddScoped<LibroService>();
builder.Services.AddScoped<EditorialService>();
builder.Services.AddScoped<PersonaService>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<PrestamoService>();

// Auth
builder.Services.AddScoped<AuthService>();

// JWT Authentication
var jwtSection = builder.Configuration.GetSection("JwtSettings");
var secretKey = builder.Environment.IsEnvironment("Testing")
    ? "test-secret-key-12345678901234567890"
    : (jwtSection["SecretKey"] ?? throw new InvalidOperationException("JwtSettings:SecretKey no configurado"));
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key,
            ValidateIssuer = !builder.Environment.IsEnvironment("Testing"),
            ValidIssuer = jwtSection["Issuer"],
            ValidateAudience = !builder.Environment.IsEnvironment("Testing"),
            ValidAudience = jwtSection["Audience"],
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            RoleClaimType = ClaimTypes.Role
        };
    });

builder.Services.AddAuthorization(options =>
{
    string[] recursos = ["autores","libros","generos","editoriales","personas","usuarios","prestamos"];
    string[] acciones = ["leer","agregar","actualizar","eliminar"];
    foreach (var r in recursos)
    {
        foreach (var a in acciones)
        {
            var policyName = $"{r}.{a}";
            options.AddPolicy(policyName, policy =>
            {
                policy.RequireAssertion(ctx =>
                    ctx.User.HasClaim(c => c.Type == "permiso" && string.Equals(c.Value, policyName, StringComparison.OrdinalIgnoreCase))
                    || ctx.User.IsInRole("administrador")
                    || ctx.User.IsInRole("bibliotecario")
                );
            });
        }
    }
});

// Configurar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Biblioteca API", Version = "v1" });
});

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

// Crear la base de datos si no existe
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<BibliotecaContext>();
    // Crea DB si no existe
    context.Database.EnsureCreated();

    // En Testing (InMemory) evitar SQL crudo
    if (!app.Environment.IsEnvironment("Testing"))
    {
        // Asegurar que existan tablas de unión si la DB es previa
        EnsureJoinTables(context);

        // Asegurar que la cuenta admin tenga rol 'administrador'
        EnsureAdminRole(context);
    }
}

static void EnsureJoinTables(BibliotecaContext context)
{
    // Crear UsuarioGrupos si no existe
    var createUsuarioGrupos = @"
IF OBJECT_ID('dbo.UsuarioGrupos','U') IS NULL
BEGIN
    CREATE TABLE [dbo].[UsuarioGrupos](
        [UsuariosId] INT NOT NULL,
        [GruposId] INT NOT NULL,
        CONSTRAINT [PK_UsuarioGrupos] PRIMARY KEY ([UsuariosId],[GruposId]),
        CONSTRAINT [FK_UsuarioGrupos_Usuarios_UsuariosId] FOREIGN KEY ([UsuariosId]) REFERENCES [dbo].[Usuarios]([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_UsuarioGrupos_GruposPermiso_GruposId] FOREIGN KEY ([GruposId]) REFERENCES [dbo].[GruposPermiso]([Id]) ON DELETE CASCADE
    );
END";
    context.Database.ExecuteSqlRaw(createUsuarioGrupos);

    // Crear GrupoPermisos si no existe
    var createGrupoPermisos = @"
IF OBJECT_ID('dbo.GrupoPermisos','U') IS NULL
BEGIN
    CREATE TABLE [dbo].[GrupoPermisos](
        [GruposId] INT NOT NULL,
        [PermisosId] INT NOT NULL,
        CONSTRAINT [PK_GrupoPermisos] PRIMARY KEY ([GruposId],[PermisosId]),
        CONSTRAINT [FK_GrupoPermisos_GruposPermiso_GruposId] FOREIGN KEY ([GruposId]) REFERENCES [dbo].[GruposPermiso]([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_GrupoPermisos_Permisos_PermisosId] FOREIGN KEY ([PermisosId]) REFERENCES [dbo].[Permisos]([Id]) ON DELETE CASCADE
    );
END";
    context.Database.ExecuteSqlRaw(createGrupoPermisos);

    // Sembrar asignaciones mínimas si no hay filas
    var seedSql = @"
IF NOT EXISTS (SELECT 1 FROM dbo.UsuarioGrupos)
BEGIN
    -- admin -> bibliotecario (1->1) y usuarios 2/3 -> socio (2 y 3 -> 2)
    IF EXISTS (SELECT 1 FROM dbo.Usuarios WHERE Id = 1) AND EXISTS (SELECT 1 FROM dbo.GruposPermiso WHERE Id = 1)
        INSERT INTO dbo.UsuarioGrupos(UsuariosId, GruposId) VALUES (1,1);
    IF EXISTS (SELECT 1 FROM dbo.Usuarios WHERE Id = 2) AND EXISTS (SELECT 1 FROM dbo.GruposPermiso WHERE Id = 2)
        INSERT INTO dbo.UsuarioGrupos(UsuariosId, GruposId) VALUES (2,2);
    IF EXISTS (SELECT 1 FROM dbo.Usuarios WHERE Id = 3) AND EXISTS (SELECT 1 FROM dbo.GruposPermiso WHERE Id = 2)
        INSERT INTO dbo.UsuarioGrupos(UsuariosId, GruposId) VALUES (3,2);
END

IF NOT EXISTS (SELECT 1 FROM dbo.GrupoPermisos)
BEGIN
    -- bibliotecario: todos los permisos
    INSERT INTO dbo.GrupoPermisos(GruposId, PermisosId)
        SELECT 1 AS GruposId, p.Id AS PermisosId FROM dbo.Permisos p;

    -- socio: permisos de lectura de libros y préstamos únicamente (configuración inicial)
    INSERT INTO dbo.GrupoPermisos(GruposId, PermisosId)
        SELECT 2 AS GruposId, p.Id AS PermisosId FROM dbo.Permisos p
        WHERE p.Nombre = 'leer' AND p.Categoria IN ('libros','prestamos');
END";
    context.Database.ExecuteSqlRaw(seedSql);

    // Asegurar que el grupo 'socio' (Id=2) tenga lectura de autores, generos y editoriales (idempotente)
    var ensureSocioRead = @"
INSERT INTO dbo.GrupoPermisos(GruposId, PermisosId)
SELECT 2 AS GruposId, p.Id AS PermisosId
FROM dbo.Permisos p
WHERE p.Nombre = 'leer' AND p.Categoria IN ('autores','generos','editoriales')
  AND NOT EXISTS (
      SELECT 1 FROM dbo.GrupoPermisos gp WHERE gp.GruposId = 2 AND gp.PermisosId = p.Id
  );";
    context.Database.ExecuteSqlRaw(ensureSocioRead);
}

static void EnsureAdminRole(BibliotecaContext context)
{
    // Si existe un usuario 'admin' o Id=1 y su rol no es 'administrador', elevarlo.
    var elevateSql = @"
UPDATE u SET Rol = 'administrador'
FROM dbo.Usuarios u
WHERE (u.Id = 1 OR LOWER(u.NombreUsuario) = 'admin') AND u.Rol <> 'administrador'";
    context.Database.ExecuteSqlRaw(elevateSql);
}

static int? GetUserId(HttpContext http)
{
    // Buscar primero NameIdentifier, luego el claim estándar "sub"
    var c = http.User.FindFirst(ClaimTypes.NameIdentifier)
            ?? http.User.FindFirst("sub");
    return c != null && int.TryParse(c.Value, out var id) ? id : null;
}

static bool IsInRole(HttpContext http, string role)
    => http.User.IsInRole(role) || http.User.Claims.Any(c => (c.Type == ClaimTypes.Role || c.Type == "role" || c.Type == "roles") && string.Equals(c.Value, role, StringComparison.OrdinalIgnoreCase));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("AllowAll");
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// API Endpoints
app.MapRazorPages();

// Auth endpoint
app.MapPost("/auth/login", async (LoginRequest request, AuthService authService) =>
{
    var result = await authService.LoginAsync(request);
    if (result == null)
        return Results.Unauthorized();
    return Results.Ok(result);
}).AllowAnonymous().WithTags("Auth");

app.MapPost("/auth/change-password", (ChangePasswordDto dto, HttpContext http, UsuarioService usuarioService) =>
{
    if (!http.User.Identity?.IsAuthenticated ?? true)
        return Results.Unauthorized();

    var userId = GetUserId(http);
    if (userId == null)
        return Results.BadRequest("Usuario inválido en token");

    var ok = usuarioService.ChangePassword(userId.Value, dto.CurrentPassword, dto.NewPassword);
    return ok ? Results.Ok() : Results.BadRequest("Contraseña actual inválida o error al actualizar");
}).RequireAuthorization().WithTags("Auth");

// Políticas aplicadas por recurso
app.MapGet("/api/autores", (AutorService s) => Results.Ok(s.GetAll())).RequireAuthorization("autores.leer").WithTags("Autores");
app.MapGet("/api/autores/{id}", (int id, AutorService s) => { var a = s.Get(id); return a != null ? Results.Ok(a) : Results.NotFound(); }).RequireAuthorization("autores.leer").WithTags("Autores");
app.MapPost("/api/autores", (Biblioteca.DTOs.CrearAutorDto dto, AutorService s) => { try { var a = s.Add(dto); return Results.Created($"/api/autores/{a.Id}", a);} catch (Exception ex) { return Results.BadRequest(ex.Message);} }).RequireAuthorization("autores.agregar").WithTags("Autores");
app.MapPut("/api/autores", (Biblioteca.DTOs.AutorDto dto, AutorService s) => { try { var ok = s.Update(dto); return ok ? Results.Ok() : Results.NotFound(); } catch (Exception ex) { return Results.BadRequest(ex.Message);} }).RequireAuthorization("autores.actualizar").WithTags("Autores");
app.MapDelete("/api/autores/{id}", (int id, AutorService s) => { try { var ok = s.Delete(id); return ok ? Results.Ok() : Results.NotFound(); } catch (Exception ex) { return Results.BadRequest(ex.Message);} }).RequireAuthorization("autores.eliminar").WithTags("Autores");
app.MapGet("/api/autores/criteria", (string? texto, AutorService s) => { var c = new Biblioteca.DTOs.BusquedaCriterioDto { Texto = texto ?? "" }; return Results.Ok(s.GetByCriteria(c)); }).RequireAuthorization("autores.leer").WithTags("Autores");

app.MapGet("/api/generos", (GeneroService s) => Results.Ok(s.GetAll())).RequireAuthorization("generos.leer").WithTags("Géneros");
app.MapGet("/api/generos/{id}", (int id, GeneroService s) => { var g = s.Get(id); return g != null ? Results.Ok(g) : Results.NotFound(); }).RequireAuthorization("generos.leer").WithTags("Géneros");
app.MapPost("/api/generos", (Biblioteca.DTOs.CrearGeneroDto dto, GeneroService s) => { try { var g = s.Add(dto); return Results.Created($"/api/generos/{g.Id}", g);} catch (Exception ex) { return Results.BadRequest(ex.Message);} }).RequireAuthorization("generos.agregar").WithTags("Géneros");
app.MapPut("/api/generos", (Biblioteca.DTOs.GeneroDto dto, GeneroService s) => { try { var ok = s.Update(dto); return ok ? Results.Ok() : Results.NotFound(); } catch (Exception ex) { return Results.BadRequest(ex.Message);} }).RequireAuthorization("generos.actualizar").WithTags("Géneros");
app.MapDelete("/api/generos/{id}", (int id, GeneroService s) => { try { var ok = s.Delete(id); return ok ? Results.Ok() : Results.NotFound(); } catch (Exception ex) { return Results.BadRequest(ex.Message);} }).RequireAuthorization("generos.eliminar").WithTags("Géneros");
app.MapGet("/api/generos/criteria", (string? texto, GeneroService s) => { var c = new Biblioteca.DTOs.BusquedaCriterioDto { Texto = texto ?? "" }; return Results.Ok(s.GetByCriteria(c)); }).RequireAuthorization("generos.leer").WithTags("Géneros");

app.MapGet("/api/editoriales", (EditorialService s) => Results.Ok(s.GetAll())).RequireAuthorization("editoriales.leer").WithTags("Editoriales");
app.MapGet("/api/editoriales/{id}", (int id, EditorialService s) => { var e = s.Get(id); return e != null ? Results.Ok(e) : Results.NotFound(); }).RequireAuthorization("editoriales.leer").WithTags("Editoriales");
app.MapPost("/api/editoriales", (Biblioteca.DTOs.CrearEditorialDto dto, EditorialService s) => { try { var e = s.Add(dto); return Results.Created($"/api/editoriales/{e.Id}", e);} catch (Exception ex) { return Results.BadRequest(ex.Message);} }).RequireAuthorization("editoriales.agregar").WithTags("Editoriales");
app.MapPut("/api/editoriales", (Biblioteca.DTOs.EditorialDto dto, EditorialService s) => { try { var ok = s.Update(dto); return ok ? Results.Ok() : Results.NotFound(); } catch (Exception ex) { return Results.BadRequest(ex.Message);} }).RequireAuthorization("editoriales.actualizar").WithTags("Editoriales");
app.MapDelete("/api/editoriales/{id}", (int id, EditorialService s) => { try { var ok = s.Delete(id); return ok ? Results.Ok() : Results.NotFound(); } catch (Exception ex) { return Results.BadRequest(ex.Message);} }).RequireAuthorization("editoriales.eliminar").WithTags("Editoriales");
app.MapGet("/api/editoriales/criteria", (string? texto, EditorialService s) => { var c = new Biblioteca.DTOs.BusquedaCriterioDto { Texto = texto ?? "" }; return Results.Ok(s.GetByCriteria(c)); }).RequireAuthorization("editoriales.leer").WithTags("Editoriales");

app.MapGet("/api/personas", (PersonaService s) => Results.Ok(s.GetAll())).RequireAuthorization("personas.leer").WithTags("Personas");
app.MapGet("/api/personas/{id}", (int id, PersonaService s) => { var p = s.Get(id); return p != null ? Results.Ok(p) : Results.NotFound(); }).RequireAuthorization("personas.leer").WithTags("Personas");
app.MapPost("/api/personas", (Biblioteca.DTOs.CrearPersonaDto dto, PersonaService s) => { try { var p = s.Add(dto); return Results.Created($"/api/personas/{p.Id}", p);} catch (Exception ex) { return Results.BadRequest(ex.Message);} }).RequireAuthorization("personas.agregar").WithTags("Personas");
app.MapPut("/api/personas", (Biblioteca.DTOs.PersonaDto dto, PersonaService s) => { try { var ok = s.Update(dto); return ok ? Results.Ok() : Results.NotFound(); } catch (Exception ex) { return Results.BadRequest(ex.Message);} }).RequireAuthorization("personas.actualizar").WithTags("Personas");
app.MapDelete("/api/personas/{id}", (int id, PersonaService s) => { try { var ok = s.Delete(id); return ok ? Results.Ok() : Results.NotFound(); } catch (Exception ex) { return Results.BadRequest(ex.Message);} }).RequireAuthorization("personas.eliminar").WithTags("Personas");
app.MapGet("/api/personas/criteria", (string? texto, PersonaService s) => { var c = new Biblioteca.DTOs.BusquedaCriterioDto { Texto = texto ?? "" }; return Results.Ok(s.GetByCriteria(c)); }).RequireAuthorization("personas.leer").WithTags("Personas");

app.MapGet("/api/usuarios", (HttpContext http, UsuarioService s) =>
{
    if (IsInRole(http, "socio")) return Results.Forbid();
    return Results.Ok(s.GetAll());
}).RequireAuthorization("usuarios.leer").WithTags("Usuarios");
app.MapGet("/api/usuarios/{id}", (int id, HttpContext http, UsuarioService s) =>
{
    if (IsInRole(http, "socio")) return Results.Forbid();
    var u = s.Get(id);
    return u != null ? Results.Ok(u) : Results.NotFound();
}).RequireAuthorization("usuarios.leer").WithTags("Usuarios");
app.MapPost("/api/usuarios", (Biblioteca.DTOs.CrearUsuarioDto dto, HttpContext http, UsuarioService s) =>
{
    try
    {
        if (IsInRole(http, "socio")) return Results.Forbid();
        if (IsInRole(http, "bibliotecario") && (dto.Rol.Equals("bibliotecario", StringComparison.OrdinalIgnoreCase) || dto.Rol.Equals("administrador", StringComparison.OrdinalIgnoreCase)))
            return Results.Forbid();
        var u = s.Add(dto);
        return Results.Created($"/api/usuarios/{u.Id}", u);
    }
    catch (Exception ex) { return Results.BadRequest(ex.Message); }
}).RequireAuthorization("usuarios.agregar").WithTags("Usuarios");
app.MapPut("/api/usuarios", (Biblioteca.DTOs.UsuarioDto dto, HttpContext http, UsuarioService s) =>
{
    try
    {
        if (IsInRole(http, "socio")) return Results.Forbid();
        if (IsInRole(http, "bibliotecario") && (dto.Rol.Equals("bibliotecario", StringComparison.OrdinalIgnoreCase) || dto.Rol.Equals("administrador", StringComparison.OrdinalIgnoreCase)))
            return Results.Forbid();
        var ok = s.Update(dto);
        return ok ? Results.Ok() : Results.NotFound();
    }
    catch (Exception ex) { return Results.BadRequest(ex.Message); }
}).RequireAuthorization("usuarios.actualizar").WithTags("Usuarios");
app.MapDelete("/api/usuarios/{id}", (int id, HttpContext http, UsuarioService s) =>
{
    try
    {
        if (IsInRole(http, "socio")) return Results.Forbid();
        var ok = s.Delete(id);
        return ok ? Results.Ok() : Results.NotFound();
    }
    catch (Exception ex) { return Results.BadRequest(ex.Message); }
}).RequireAuthorization("usuarios.eliminar").WithTags("Usuarios");
app.MapGet("/api/usuarios/criteria", (string? texto, HttpContext http, UsuarioService s) =>
{
    if (IsInRole(http, "socio")) return Results.Forbid();
    var c = new Biblioteca.DTOs.BusquedaCriterioDto { Texto = texto ?? "" };
    return Results.Ok(s.GetByCriteria(c));
}).RequireAuthorization("usuarios.leer").WithTags("Usuarios");
app.MapGet("/api/usuarios/rol/{rol}", (string rol, HttpContext http, UsuarioService s) =>
{
    if (IsInRole(http, "socio")) return Results.Forbid();
    return Results.Ok(s.GetByRol(rol));
}).RequireAuthorization("usuarios.leer").WithTags("Usuarios");

app.MapGet("/api/libros", (HttpContext http, LibroService s) =>
{
    if (IsInRole(http, "socio"))
        return Results.Ok(s.GetByEstado("disponible"));
    return Results.Ok(s.GetAll());
}).RequireAuthorization("libros.leer").WithTags("Libros");
app.MapGet("/api/libros/{id}", (int id, LibroService s) => { var l = s.Get(id); return l != null ? Results.Ok(l) : Results.NotFound(); }).RequireAuthorization("libros.leer").WithTags("Libros");
app.MapPost("/api/libros", (Biblioteca.DTOs.CrearLibroDto dto, LibroService s) => { try { var l = s.Add(dto); return Results.Created($"/api/libros/{l.Id}", l);} catch (Exception ex) { return Results.BadRequest(ex.Message);} }).RequireAuthorization("libros.agregar").WithTags("Libros");
app.MapPut("/api/libros", (Biblioteca.DTOs.LibroDto dto, LibroService s) => { try { var ok = s.Update(dto); return ok ? Results.Ok() : Results.NotFound(); } catch (Exception ex) { return Results.BadRequest(ex.Message);} }).RequireAuthorization("libros.actualizar").WithTags("Libros");
app.MapDelete("/api/libros/{id}", (int id, LibroService s) => { try { var ok = s.Delete(id); return ok ? Results.Ok() : Results.NotFound(); } catch (Exception ex) { return Results.BadRequest(ex.Message);} }).RequireAuthorization("libros.eliminar").WithTags("Libros");
app.MapGet("/api/libros/criteria", (string? texto, LibroService s) => { var c = new Biblioteca.DTOs.BusquedaCriterioDto { Texto = texto ?? "" }; return Results.Ok(s.GetByCriteria(c)); }).RequireAuthorization("libros.leer").WithTags("Libros");
app.MapGet("/api/libros/autor/{autorId}", (int autorId, LibroService s) => Results.Ok(s.GetByAutor(autorId))).RequireAuthorization("libros.leer").WithTags("Libros");
app.MapGet("/api/libros/genero/{generoId}", (int generoId, LibroService s) => Results.Ok(s.GetByGenero(generoId))).RequireAuthorization("libros.leer").WithTags("Libros");
app.MapGet("/api/libros/editorial/{editorialId}", (int editorialId, LibroService s) => Results.Ok(s.GetByEditorial(editorialId))).RequireAuthorization("libros.leer").WithTags("Libros");
app.MapGet("/api/libros/estado/{estado}", (string estado, LibroService s) => Results.Ok(s.GetByEstado(estado))).RequireAuthorization("libros.leer").WithTags("Libros");

// Endpoints para Préstamos
app.MapGet("/api/prestamos", (HttpContext http, PrestamoService s) =>
{
    if (IsInRole(http, "socio"))
    {
        var userId = GetUserId(http);
        if (userId == null) return Results.Unauthorized();
        return Results.Ok(s.GetPrestamosBySocio(userId.Value));
    }
    return Results.Ok(s.GetAll());
}).RequireAuthorization("prestamos.leer").WithTags("Préstamos");
app.MapGet("/api/prestamos/{id}", (int id, HttpContext http, PrestamoService s) =>
{
    var p = s.Get(id);
    if (p == null) return Results.NotFound();
    if (IsInRole(http, "socio"))
    {
        var userId = GetUserId(http);
        if (userId == null || p.SocioId != userId.Value) return Results.Forbid();
    }
    return Results.Ok(p);
}).RequireAuthorization("prestamos.leer").WithTags("Préstamos");
app.MapPost("/api/prestamos", (Biblioteca.DTOs.CrearPrestamoDto dto, PrestamoService s) => { try { var p = s.Add(dto); return Results.Created($"/api/prestamos/{p.Id}", p);} catch (Exception ex) { return Results.BadRequest(ex.Message);} }).RequireAuthorization("prestamos.agregar").WithTags("Préstamos");
app.MapPut("/api/prestamos", (Biblioteca.DTOs.PrestamoDto dto, PrestamoService s) => { try { var ok = s.Update(dto); return ok ? Results.Ok() : Results.NotFound(); } catch (Exception ex) { return Results.BadRequest(ex.Message);} }).RequireAuthorization("prestamos.actualizar").WithTags("Préstamos");
app.MapDelete("/api/prestamos/{id}", (int id, PrestamoService s) => { try { var ok = s.Delete(id); return ok ? Results.Ok() : Results.NotFound(); } catch (Exception ex) { return Results.BadRequest(ex.Message);} }).RequireAuthorization("prestamos.eliminar").WithTags("Préstamos");
app.MapGet("/api/prestamos/activos", (HttpContext http, PrestamoService s) =>
{
    if (IsInRole(http, "socio"))
    {
        var userId = GetUserId(http);
        if (userId == null) return Results.Unauthorized();
        return Results.Ok(s.GetPrestamosBySocio(userId.Value).Where(x => !x.FechaDevolucionReal.HasValue));
    }
    return Results.Ok(s.GetPrestamosActivos());
}).RequireAuthorization("prestamos.leer").WithTags("Préstamos");
app.MapGet("/api/prestamos/socio/{socioId}", (int socioId, HttpContext http, PrestamoService s) =>
{
    if (IsInRole(http, "socio"))
    {
        var userId = GetUserId(http);
        if (userId == null || userId.Value != socioId) return Results.Forbid();
    }
    return Results.Ok(s.GetPrestamosBySocio(socioId));
}).RequireAuthorization("prestamos.leer").WithTags("Préstamos");
app.MapGet("/api/prestamos/vencidos", (HttpContext http, PrestamoService s) =>
{
    if (IsInRole(http, "socio"))
    {
        var userId = GetUserId(http);
        if (userId == null) return Results.Unauthorized();
        return Results.Ok(s.GetPrestamosBySocio(userId.Value).Where(x => !x.FechaDevolucionReal.HasValue && x.FechaDevolucionPrevista < DateTime.UtcNow));
    }
    return Results.Ok(s.GetPrestamosVencidos());
}).RequireAuthorization("prestamos.leer").WithTags("Préstamos");
app.MapPost("/api/prestamos/{id}/devolver", (int id, DateTime? fechaDevolucion, PrestamoService s) => { try { var fecha = fechaDevolucion ?? DateTime.Now; var ok = s.DevolverLibro(id, fecha); return ok ? Results.Ok() : Results.NotFound(); } catch (Exception ex) { return Results.BadRequest(ex.Message);} }).RequireAuthorization("prestamos.actualizar").WithTags("Préstamos");

app.Run();

namespace Biblioteca.WebAPI
{
    public partial class Program { }
}