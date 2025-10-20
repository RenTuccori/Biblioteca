using System;
using System.Linq;
using Biblioteca.Data;
using Biblioteca.Data.Entities;
using Biblioteca.Domain.Services;
using Biblioteca.DTOs;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Biblioteca.Domain.Services.Tests.Services;

public class LibroServiceTests
{
    private static BibliotecaContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<BibliotecaContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        var ctx = new BibliotecaContext(options);
        // Seed dependencias requeridas
        ctx.Autores.Add(new AutorEntity { Id = 1, Nombre = "A", Apellido = "B" });
        ctx.Generos.Add(new GeneroEntity { Id = 1, Nombre = "G" });
        ctx.Editoriales.Add(new EditorialEntity { Id = 1, Nombre = "E" });
        ctx.SaveChanges();
        return ctx;
    }

    [Fact]
    public void Add_ReturnsBook()
    {
        using var ctx = CreateContext();
        var svc = new LibroService(new LibroRepository(ctx), new AutorRepository(ctx), new GeneroRepository(ctx), new EditorialRepository(ctx));
        var dto = new CrearLibroDto { Titulo = "T", ISBN = "I", AutorId = 1, GeneroId = 1, EditorialId = 1, Estado = "disponible" };
        var created = svc.Add(dto);
        Assert.True(created.Id > 0);
        Assert.Equal("T", created.Titulo);
    }
}
