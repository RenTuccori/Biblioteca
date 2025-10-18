using System;
using Biblioteca.Data;
using Biblioteca.Data.Entities;
using Biblioteca.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Biblioteca.Domain.Services.Tests.Services;

public class PrestamoServiceTests
{
    private static BibliotecaContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<BibliotecaContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        var ctx = new BibliotecaContext(options);
        // Seed
        ctx.Autores.Add(new AutorEntity { Id = 10, Nombre = "A", Apellido = "B" });
        ctx.Generos.Add(new GeneroEntity { Id = 10, Nombre = "G" });
        ctx.Editoriales.Add(new EditorialEntity { Id = 10, Nombre = "E" });
        ctx.Libros.Add(new LibroEntity { Id = 10, Titulo = "L", ISBN = "I", AutorId = 10, GeneroId = 10, EditorialId = 10, Estado = "disponible" });
        ctx.Personas.Add(new PersonaEntity { Id = 10, Nombre = "Nom", Apellido = "Ape", Dni = "D3", Email = "e@e.com" });
        ctx.Usuarios.Add(new UsuarioEntity { Id = 10, NombreUsuario = "socio10", Rol = "socio", PersonaId = 10, PasswordHash = new byte[32], Salt = new byte[16], Activo = true, FechaCreacion = DateTime.UtcNow });
        ctx.SaveChanges();
        return ctx;
    }

    [Fact]
    public void Add_And_Return_Works()
    {
        using var ctx = CreateContext();
        var prestamoSvc = new PrestamoService(new PrestamoRepository(ctx), new LibroRepository(ctx), new UsuarioRepository(ctx));
        var created = prestamoSvc.Add(new Biblioteca.DTOs.CrearPrestamoDto
        {
            LibroId = 10,
            SocioId = 10,
            FechaPrestamo = DateTime.Now,
            FechaDevolucionPrevista = DateTime.Now.AddDays(7)
        });
        Assert.NotNull(created);
        var ok = prestamoSvc.DevolverLibro(created.Id, DateTime.Now);
        Assert.True(ok);
    }
}
