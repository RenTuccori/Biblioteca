using System;
using System.Linq;
using Biblioteca.Data;
using Biblioteca.Data.Entities;
using Biblioteca.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Biblioteca.Domain.Services.Tests.Services;

public class PersonaServiceTests
{
    private static BibliotecaContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<BibliotecaContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        var ctx = new BibliotecaContext(options);
        ctx.Personas.Add(new PersonaEntity { Id = 200, Nombre = "Nom", Apellido = "Ape", Dni = "D1", Email = "nom@ape.com" });
        ctx.SaveChanges();
        return ctx;
    }

    [Fact]
    public void GetAll_ReturnsItems()
    {
        using var ctx = CreateContext();
        var svc = new PersonaService(new PersonaRepository(ctx));
        var data = svc.GetAll().ToList();
        Assert.True(data.Count >= 1);
    }
}
