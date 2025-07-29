using Biblioteca.Domain.Model;
using Biblioteca.Domain.Services;
using Biblioteca.DTOs;

var builder = WebApplication.CreateBuilder(args);

// Configuración de servicios (igual que en el ejemplo)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuración del pipeline de HTTP (igual que en el ejemplo)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// --- INICIO DE NUESTROS ENDPOINTS PARA GÉNEROS ---

// Endpoint para OBTENER TODOS los géneros
// GET /generos
app.MapGet("/generos", () =>
{
    var generoService = new GeneroService();
    var generosDelDominio = generoService.GetAll();

    // Mapeamos de la lista de Dominio a la lista de DTOs (igual que en el Form)
    var dtos = generosDelDominio.Select(g => new GeneroDto
    {
        Id = g.Id,
        Nombre = g.Nombre
    }).ToList();

    return Results.Ok(dtos);
})
.WithName("GetAllGeneros")
.Produces<List<GeneroDto>>(StatusCodes.Status200OK);

// Endpoint para OBTENER UN género por su ID
// GET /generos/5
app.MapGet("/generos/{id}", (int id) =>
{
    var generoService = new GeneroService();
    var genero = generoService.GetById(id);

    if (genero == null)
    {
        return Results.NotFound(); // Si no lo encuentra, devuelve un error 404
    }

    // Mapeamos del objeto de Dominio al DTO
    var dto = new GeneroDto { Id = genero.Id, Nombre = genero.Nombre };

    return Results.Ok(dto);
})
.WithName("GetGeneroById")
.Produces<GeneroDto>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status404NotFound);

// Endpoint para CREAR un nuevo género
// POST /generos
app.MapPost("/generos", (GeneroDto generoDto) =>
{
    try
    {
        var generoService = new GeneroService();

        // Mapeamos del DTO que llega al objeto de Dominio para aplicar las reglas
        var nuevoGenero = new Genero(0, generoDto.Nombre);

        generoService.Add(nuevoGenero);

        // Devolvemos el objeto creado (con su nuevo ID) como un DTO
        var dtoResultado = new GeneroDto { Id = nuevoGenero.Id, Nombre = nuevoGenero.Nombre };

        return Results.Created($"/generos/{dtoResultado.Id}", dtoResultado); // Devuelve un código 201 Created
    }
    catch (ArgumentException ex)
    {
        // Si la validación del dominio falla, devolvemos un error 400
        return Results.BadRequest(new { error = ex.Message });
    }
})
.WithName("AddGenero")
.Produces<GeneroDto>(StatusCodes.Status201Created)
.Produces(StatusCodes.Status400BadRequest);

// Endpoint para MODIFICAR un género existente
// PUT /generos/5
app.MapPut("/generos/{id}", (int id, GeneroDto generoDto) =>
{
    try
    {
        // Es una buena práctica verificar que el ID de la URL coincida con el del cuerpo
        if (id != generoDto.Id)
        {
            return Results.BadRequest("El ID de la URL no coincide con el ID del cuerpo.");
        }

        var generoService = new GeneroService();
        var generoModificado = new Genero(generoDto.Id, generoDto.Nombre);

        var encontrado = generoService.Update(generoModificado);

        if (!encontrado)
        {
            return Results.NotFound();
        }

        return Results.NoContent(); // Devuelve un código 204 NoContent si fue exitoso
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(new { error = ex.Message });
    }
})
.WithName("UpdateGenero")
.Produces(StatusCodes.Status204NoContent)
.Produces(StatusCodes.Status404NotFound)
.Produces(StatusCodes.Status400BadRequest);


// Endpoint para ELIMINAR un género
// DELETE /generos/5
app.MapDelete("/generos/{id}", (int id) =>
{
    var generoService = new GeneroService();
    var eliminado = generoService.Delete(id);

    if (!eliminado)
    {
        return Results.NotFound();
    }

    return Results.NoContent(); // Devuelve un código 204 NoContent
})
.WithName("DeleteGenero")
.Produces(StatusCodes.Status204NoContent)
.Produces(StatusCodes.Status404NotFound);


// --- FIN DE NUESTROS ENDPOINTS ---

// --- INICIO DE ENDPOINTS PARA AUTORES ---

// Endpoint para OBTENER TODOS los autores
// GET /autores
app.MapGet("/autores", () =>
{
    var autorService = new AutorService();
    var autoresDelDominio = autorService.GetAll();
    var dtos = autoresDelDominio.Select(a => new AutorDto
    {
        Id = a.Id,
        Nombre = a.Nombre,
        Apellido = a.Apellido
    }).ToList();
    return Results.Ok(dtos);
})
.WithName("GetAllAutores")
.Produces<List<AutorDto>>(StatusCodes.Status200OK);

// Endpoint para OBTENER UN autor por su ID
// GET /autores/1
app.MapGet("/autores/{id}", (int id) =>
{
    var autorService = new AutorService();
    var autor = autorService.GetById(id);
    if (autor == null)
    {
        return Results.NotFound();
    }
    var dto = new AutorDto
    {
        Id = autor.Id,
        Nombre = autor.Nombre,
        Apellido = autor.Apellido
    };
    return Results.Ok(dto);
})
.WithName("GetAutorById")
.Produces<AutorDto>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status404NotFound);

// Endpoint para CREAR un nuevo autor
// POST /autores
app.MapPost("/autores", (AutorDto autorDto) =>
{
    try
    {
        var autorService = new AutorService();
        var nuevoAutor = new Autor(0, autorDto.Nombre, autorDto.Apellido);
        autorService.Add(nuevoAutor);
        var dtoResultado = new AutorDto
        {
            Id = nuevoAutor.Id,
            Nombre = nuevoAutor.Nombre,
            Apellido = nuevoAutor.Apellido
        };
        return Results.Created($"/autores/{dtoResultado.Id}", dtoResultado);
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(new { error = ex.Message });
    }
})
.WithName("AddAutor")
.Produces<AutorDto>(StatusCodes.Status201Created)
.Produces(StatusCodes.Status400BadRequest);

// Endpoint para MODIFICAR un autor existente
// PUT /autores/1
app.MapPut("/autores/{id}", (int id, AutorDto autorDto) =>
{
    try
    {
        if (id != autorDto.Id)
        {
            return Results.BadRequest("El ID de la URL no coincide con el ID del cuerpo.");
        }

        var autorService = new AutorService();
        var autorModificado = new Autor(autorDto.Id, autorDto.Nombre, autorDto.Apellido);

        // -- CÓDIGO ORIGINAL RESTAURADO --
        var encontrado = autorService.Update(autorModificado);

        if (!encontrado)
        {
            return Results.NotFound();
        }

        return Results.NoContent();
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(new { error = ex.Message });
    }
})
.WithName("UpdateAutor")
.Produces(StatusCodes.Status204NoContent)
.Produces(StatusCodes.Status404NotFound)
.Produces(StatusCodes.Status400BadRequest);

// Endpoint para ELIMINAR un autor
// DELETE /autores/1
app.MapDelete("/autores/{id}", (int id) =>
{
    var autorService = new AutorService();

    // -- CÓDIGO ORIGINAL RESTAURADO --
    var eliminado = autorService.Delete(id);

    if (!eliminado)
    {
        return Results.NotFound();
    }

    return Results.NoContent();
})
.WithName("DeleteAutor")
.Produces(StatusCodes.Status204NoContent)
.Produces(StatusCodes.Status404NotFound);

// --- FIN DE ENDPOINTS PARA AUTORES ---

// --- INICIO DE ENDPOINTS PARA LIBROS ---

// Endpoint para OBTENER TODOS los libros
// GET /libros
app.MapGet("/libros", () =>
{
    var libroService = new LibroService();
    var librosDelDominio = libroService.GetAll();
    var dtos = librosDelDominio.Select(l => new LibroDto
    {
        Id = l.Id,
        Titulo = l.Titulo,
        ISBN = l.ISBN,
        AutorNombreCompleto = $"{l.Autor.Nombre} {l.Autor.Apellido}",
    }).ToList();
    return Results.Ok(dtos);
})
.WithName("GetAllLibros")
.Produces<List<LibroDto>>(StatusCodes.Status200OK);

// Endpoint para OBTENER UN libro por su ID
// GET /libros/1
app.MapGet("/libros/{id}", (int id) =>
{
    var libroService = new LibroService();
    var libro = libroService.GetById(id);
    if (libro == null)
    {
        return Results.NotFound();
    }
    var dto = new LibroDto
    {
        Id = libro.Id,
        Titulo = libro.Titulo,
        ISBN = libro.ISBN,
        AutorNombreCompleto = $"{libro.Autor.Nombre} {libro.Autor.Apellido}",
        GeneroNombre = libro.Genero.Nombre
    };
    return Results.Ok(dto);
})
.WithName("GetLibroById")
.Produces<LibroDto>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status404NotFound);

// Endpoint para CREAR un nuevo libro
app.MapPost("/libros", (CrearLibroDto libroDto) =>
{
    try
    {
        var libroService = new LibroService();
        // Capturamos el libro que nos devuelve el servicio
        var libroCreado = libroService.Add(libroDto);

        // Creamos un DTO de respuesta para el cliente
        var dtoRespuesta = new LibroDto
        {
            Id = libroCreado.Id,
            Titulo = libroCreado.Titulo,
            ISBN = libroCreado.ISBN,
            AutorNombreCompleto = $"{libroCreado.Autor.Nombre} {libroCreado.Autor.Apellido}",
            GeneroNombre = libroCreado.Genero.Nombre
        };

        return Results.Created($"/libros/{dtoRespuesta.Id}", dtoRespuesta);
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(new { error = ex.Message });
    }
})
.WithName("AddLibro")
.Produces<LibroDto>(StatusCodes.Status201Created)
.Produces(StatusCodes.Status400BadRequest);

// Endpoint para MODIFICAR un libro existente
app.MapPut("/libros/{id}", (int id, CrearLibroDto libroDto) =>
{
    try
    {
        var libroService = new LibroService();
        var actualizado = libroService.Update(id, libroDto);

        if (!actualizado)
        {
            return Results.NotFound();
        }

        return Results.NoContent();
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(new { error = ex.Message });
    }
})
.WithName("UpdateLibro")
.Produces(StatusCodes.Status204NoContent)
.Produces(StatusCodes.Status404NotFound)
.Produces(StatusCodes.Status400BadRequest);

// Endpoint para ELIMINAR un libro
app.MapDelete("/libros/{id}", (int id) =>
{
    var libroService = new LibroService();
    var eliminado = libroService.Delete(id);
    if (!eliminado)
    {
        return Results.NotFound();
    }
    return Results.NoContent();
})
.WithName("DeleteLibro")
.Produces(StatusCodes.Status204NoContent)
.Produces(StatusCodes.Status404NotFound);

// --- FIN DE ENDPOINTS PARA LIBROS ---

app.Run();