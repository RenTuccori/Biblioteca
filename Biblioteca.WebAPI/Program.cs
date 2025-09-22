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

// Registrar servicios
builder.Services.AddScoped<AutorService>();
builder.Services.AddScoped<GeneroService>();
builder.Services.AddScoped<LibroService>();

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

// Crear la base de datos si no existe usando configuraci�n centralizada
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

// Endpoints para G�neros
app.MapGet("/api/generos", (GeneroService service) =>
{
    return Results.Ok(service.GetAll());
})
.WithTags("G�neros");

app.MapGet("/api/generos/{id}", (int id, GeneroService service) =>
{
    var genero = service.Get(id);
    return genero != null ? Results.Ok(genero) : Results.NotFound();
})
.WithTags("G�neros");

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
.WithTags("G�neros");

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
.WithTags("G�neros");

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
.WithTags("G�neros");

app.MapGet("/api/generos/criteria", (string? texto, GeneroService service) =>
{
    var criterio = new Biblioteca.DTOs.BusquedaCriterioDto { Texto = texto ?? "" };
    return Results.Ok(service.GetByCriteria(criterio));
})
.WithTags("G�neros");

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

app.Run();