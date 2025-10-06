using Biblioteca.Data;
using Biblioteca.Domain.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Configurar Entity Framework con SQL Server
builder.Services.AddDbContext<BibliotecaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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
    context.Database.EnsureCreated();
}

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
app.UseAuthorization();

// API Endpoints
app.MapRazorPages();

// Endpoints para Géneros
app.MapGet("/api/generos", (GeneroService service) =>
{
    return Results.Ok(service.GetAll());
})
.WithTags("Géneros");

app.MapGet("/api/generos/{id}", (int id, GeneroService service) =>
{
    var genero = service.Get(id);
    return genero != null ? Results.Ok(genero) : Results.NotFound();
})
.WithTags("Géneros");

app.MapPost("/api/generos", (Biblioteca.DTOs.CrearGeneroDto dto, GeneroService service) =>
{
    try
    {
        var genero = service.Add(dto);
        return Results.Created($"/api/generos/{genero.Id}", genero);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
})
.WithTags("Géneros");

app.MapPut("/api/generos", (Biblioteca.DTOs.GeneroDto dto, GeneroService service) =>
{
    try
    {
        var success = service.Update(dto);
        return success ? Results.Ok() : Results.NotFound();
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
})
.WithTags("Géneros");

app.MapDelete("/api/generos/{id}", (int id, GeneroService service) =>
{
    try
    {
        var success = service.Delete(id);
        return success ? Results.Ok() : Results.NotFound();
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
})
.WithTags("Géneros");

app.MapGet("/api/generos/criteria", (string? texto, GeneroService service) =>
{
    var criterio = new Biblioteca.DTOs.BusquedaCriterioDto { Texto = texto ?? "" };
    return Results.Ok(service.GetByCriteria(criterio));
})
.WithTags("Géneros");

// Endpoints para Autores
app.MapGet("/api/autores", (AutorService service) =>
{
    return Results.Ok(service.GetAll());
})
.WithTags("Autores");

app.MapGet("/api/autores/{id}", (int id, AutorService service) =>
{
    var autor = service.Get(id);
    return autor != null ? Results.Ok(autor) : Results.NotFound();
})
.WithTags("Autores");

app.MapPost("/api/autores", (Biblioteca.DTOs.CrearAutorDto dto, AutorService service) =>
{
    try
    {
        var autor = service.Add(dto);
        return Results.Created($"/api/autores/{autor.Id}", autor);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
})
.WithTags("Autores");

app.MapPut("/api/autores", (Biblioteca.DTOs.AutorDto dto, AutorService service) =>
{
    try
    {
        var success = service.Update(dto);
        return success ? Results.Ok() : Results.NotFound();
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
})
.WithTags("Autores");

app.MapDelete("/api/autores/{id}", (int id, AutorService service) =>
{
    try
    {
        var success = service.Delete(id);
        return success ? Results.Ok() : Results.NotFound();
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
})
.WithTags("Autores");

app.MapGet("/api/autores/criteria", (string? texto, AutorService service) =>
{
    var criterio = new Biblioteca.DTOs.BusquedaCriterioDto { Texto = texto ?? "" };
    return Results.Ok(service.GetByCriteria(criterio));
})
.WithTags("Autores");

// Endpoints para Editoriales
app.MapGet("/api/editoriales", (EditorialService service) =>
{
    return Results.Ok(service.GetAll());
})
.WithTags("Editoriales");

app.MapGet("/api/editoriales/{id}", (int id, EditorialService service) =>
{
    var editorial = service.Get(id);
    return editorial != null ? Results.Ok(editorial) : Results.NotFound();
})
.WithTags("Editoriales");

app.MapPost("/api/editoriales", (Biblioteca.DTOs.CrearEditorialDto dto, EditorialService service) =>
{
    try
    {
        var editorial = service.Add(dto);
        return Results.Created($"/api/editoriales/{editorial.Id}", editorial);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
})
.WithTags("Editoriales");

app.MapPut("/api/editoriales", (Biblioteca.DTOs.EditorialDto dto, EditorialService service) =>
{
    try
    {
        var success = service.Update(dto);
        return success ? Results.Ok() : Results.NotFound();
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
})
.WithTags("Editoriales");

app.MapDelete("/api/editoriales/{id}", (int id, EditorialService service) =>
{
    try
    {
        var success = service.Delete(id);
        return success ? Results.Ok() : Results.NotFound();
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
})
.WithTags("Editoriales");

app.MapGet("/api/editoriales/criteria", (string? texto, EditorialService service) =>
{
    var criterio = new Biblioteca.DTOs.BusquedaCriterioDto { Texto = texto ?? "" };
    return Results.Ok(service.GetByCriteria(criterio));
})
.WithTags("Editoriales");

// Endpoints para Personas
app.MapGet("/api/personas", (PersonaService service) =>
{
    return Results.Ok(service.GetAll());
})
.WithTags("Personas");

app.MapGet("/api/personas/{id}", (int id, PersonaService service) =>
{
    var persona = service.Get(id);
    return persona != null ? Results.Ok(persona) : Results.NotFound();
})
.WithTags("Personas");

app.MapPost("/api/personas", (Biblioteca.DTOs.CrearPersonaDto dto, PersonaService service) =>
{
    try
    {
        var persona = service.Add(dto);
        return Results.Created($"/api/personas/{persona.Id}", persona);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
})
.WithTags("Personas");

app.MapPut("/api/personas", (Biblioteca.DTOs.PersonaDto dto, PersonaService service) =>
{
    try
    {
        var success = service.Update(dto);
        return success ? Results.Ok() : Results.NotFound();
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
})
.WithTags("Personas");

app.MapDelete("/api/personas/{id}", (int id, PersonaService service) =>
{
    try
    {
        var success = service.Delete(id);
        return success ? Results.Ok() : Results.NotFound();
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
})
.WithTags("Personas");

app.MapGet("/api/personas/criteria", (string? texto, PersonaService service) =>
{
    var criterio = new Biblioteca.DTOs.BusquedaCriterioDto { Texto = texto ?? "" };
    return Results.Ok(service.GetByCriteria(criterio));
})
.WithTags("Personas");

// Endpoints para Usuarios
app.MapGet("/api/usuarios", (UsuarioService service) =>
{
    return Results.Ok(service.GetAll());
})
.WithTags("Usuarios");

app.MapGet("/api/usuarios/{id}", (int id, UsuarioService service) =>
{
    var usuario = service.Get(id);
    return usuario != null ? Results.Ok(usuario) : Results.NotFound();
})
.WithTags("Usuarios");

app.MapPost("/api/usuarios", (Biblioteca.DTOs.CrearUsuarioDto dto, UsuarioService service) =>
{
    try
    {
        var usuario = service.Add(dto);
        return Results.Created($"/api/usuarios/{usuario.Id}", usuario);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
})
.WithTags("Usuarios");

app.MapPut("/api/usuarios", (Biblioteca.DTOs.UsuarioDto dto, UsuarioService service) =>
{
    try
    {
        var success = service.Update(dto);
        return success ? Results.Ok() : Results.NotFound();
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
})
.WithTags("Usuarios");

app.MapDelete("/api/usuarios/{id}", (int id, UsuarioService service) =>
{
    try
    {
        var success = service.Delete(id);
        return success ? Results.Ok() : Results.NotFound();
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
})
.WithTags("Usuarios");

app.MapGet("/api/usuarios/criteria", (string? texto, UsuarioService service) =>
{
    var criterio = new Biblioteca.DTOs.BusquedaCriterioDto { Texto = texto ?? "" };
    return Results.Ok(service.GetByCriteria(criterio));
})
.WithTags("Usuarios");

app.MapGet("/api/usuarios/rol/{rol}", (string rol, UsuarioService service) =>
{
    return Results.Ok(service.GetByRol(rol));
})
.WithTags("Usuarios");

// Endpoints para Libros
app.MapGet("/api/libros", (LibroService service) =>
{
    return Results.Ok(service.GetAll());
})
.WithTags("Libros");

app.MapGet("/api/libros/{id}", (int id, LibroService service) =>
{
    var libro = service.Get(id);
    return libro != null ? Results.Ok(libro) : Results.NotFound();
})
.WithTags("Libros");

app.MapPost("/api/libros", (Biblioteca.DTOs.CrearLibroDto dto, LibroService service) =>
{
    try
    {
        var libro = service.Add(dto);
        return Results.Created($"/api/libros/{libro.Id}", libro);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
})
.WithTags("Libros");

app.MapPut("/api/libros", (Biblioteca.DTOs.LibroDto dto, LibroService service) =>
{
    try
    {
        var success = service.Update(dto);
        return success ? Results.Ok() : Results.NotFound();
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
})
.WithTags("Libros");

app.MapDelete("/api/libros/{id}", (int id, LibroService service) =>
{
    try
    {
        var success = service.Delete(id);
        return success ? Results.Ok() : Results.NotFound();
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
})
.WithTags("Libros");

app.MapGet("/api/libros/criteria", (string? texto, LibroService service) =>
{
    var criterio = new Biblioteca.DTOs.BusquedaCriterioDto { Texto = texto ?? "" };
    return Results.Ok(service.GetByCriteria(criterio));
})
.WithTags("Libros");

app.MapGet("/api/libros/autor/{autorId}", (int autorId, LibroService service) =>
{
    return Results.Ok(service.GetByAutor(autorId));
})
.WithTags("Libros");

app.MapGet("/api/libros/genero/{generoId}", (int generoId, LibroService service) =>
{
    return Results.Ok(service.GetByGenero(generoId));
})
.WithTags("Libros");

app.MapGet("/api/libros/editorial/{editorialId}", (int editorialId, LibroService service) =>
{
    return Results.Ok(service.GetByEditorial(editorialId));
})
.WithTags("Libros");

app.MapGet("/api/libros/estado/{estado}", (string estado, LibroService service) =>
{
    return Results.Ok(service.GetByEstado(estado));
})
.WithTags("Libros");

// Endpoints para Préstamos
app.MapGet("/api/prestamos", (PrestamoService service) =>
{
    return Results.Ok(service.GetAll());
})
.WithTags("Préstamos");

app.MapGet("/api/prestamos/{id}", (int id, PrestamoService service) =>
{
    var prestamo = service.Get(id);
    return prestamo != null ? Results.Ok(prestamo) : Results.NotFound();
})
.WithTags("Préstamos");

app.MapPost("/api/prestamos", (Biblioteca.DTOs.CrearPrestamoDto dto, PrestamoService service) =>
{
    try
    {
        var prestamo = service.Add(dto);
        return Results.Created($"/api/prestamos/{prestamo.Id}", prestamo);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
})
.WithTags("Préstamos");

app.MapPut("/api/prestamos", (Biblioteca.DTOs.PrestamoDto dto, PrestamoService service) =>
{
    try
    {
        var success = service.Update(dto);
        return success ? Results.Ok() : Results.NotFound();
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
})
.WithTags("Préstamos");

app.MapDelete("/api/prestamos/{id}", (int id, PrestamoService service) =>
{
    try
    {
        var success = service.Delete(id);
        return success ? Results.Ok() : Results.NotFound();
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
})
.WithTags("Préstamos");

app.MapGet("/api/prestamos/activos", (PrestamoService service) =>
{
    return Results.Ok(service.GetPrestamosActivos());
})
.WithTags("Préstamos");

app.MapGet("/api/prestamos/socio/{socioId}", (int socioId, PrestamoService service) =>
{
    return Results.Ok(service.GetPrestamosBySocio(socioId));
})
.WithTags("Préstamos");

app.MapGet("/api/prestamos/vencidos", (PrestamoService service) =>
{
    return Results.Ok(service.GetPrestamosVencidos());
})
.WithTags("Préstamos");

app.MapPost("/api/prestamos/{id}/devolver", (int id, DateTime? fechaDevolucion, PrestamoService service) =>
{
    try
    {
        var fecha = fechaDevolucion ?? DateTime.Now;
        var success = service.DevolverLibro(id, fecha);
        return success ? Results.Ok() : Results.NotFound();
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
})
.WithTags("Préstamos");

app.Run();