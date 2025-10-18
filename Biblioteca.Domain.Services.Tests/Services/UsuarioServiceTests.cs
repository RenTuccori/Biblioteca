using System;
using System.Linq;
using Biblioteca.Data;
using Biblioteca.Data.Entities;
using Biblioteca.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Biblioteca.Domain.Services.Tests.Services;

public class UsuarioServiceTests
{
    private static BibliotecaContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<BibliotecaContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        var ctx = new BibliotecaContext(options);
        ctx.Personas.Add(new PersonaEntity { Id = 300, Nombre = "Nom", Apellido = "Ape", Dni = "D2", Email = "nom2@ape.com" });
        ctx.Usuarios.Add(new UsuarioEntity { Id = 300, NombreUsuario = "user300", Rol = "socio", PersonaId = 300, PasswordHash = new byte[32], Salt = new byte[16], Activo = true, FechaCreacion = DateTime.UtcNow });
        ctx.SaveChanges();
        return ctx;
    }

    [Fact]
    public void GetAll_ReturnsItems()
    {
        using var ctx = CreateContext();
        var svc = new UsuarioService(new UsuarioRepository(ctx), new PersonaRepository(ctx));
        var data = svc.GetAll().ToList();
        Assert.True(data.Count >= 1);
    }
}
