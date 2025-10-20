using System;
using System.Linq;
using Biblioteca.Data;
using Biblioteca.Data.Entities;
using Biblioteca.Domain.Services;
using Biblioteca.DTOs;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Biblioteca.Domain.Services.Tests.Services;

public class AutorServiceTests
{
    private static BibliotecaContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<BibliotecaContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        var ctx = new BibliotecaContext(options);
        return ctx;
    }

    [Fact]
    public void GetAll_ReturnsEmpty_WhenNoSeed()
    {
        using var ctx = CreateContext();
        var svc = new AutorService(new AutorRepository(ctx));
        var autores = svc.GetAll().ToList();
        Assert.Empty(autores);
    }

    [Fact]
    public void Add_ReturnsWithId()
    {
        using var ctx = CreateContext();
        var svc = new AutorService(new AutorRepository(ctx));
        var dto = new CrearAutorDto { Nombre = "Test", Apellido = "Autor" };
        var created = svc.Add(dto);
        Assert.True(created.Id > 0);
        Assert.Equal("Test", created.Nombre);
        Assert.Equal("Autor", created.Apellido);
    }

    [Fact]
    public void Get_ReturnsItem_WhenExists()
    {
        using var ctx = CreateContext();
        var repo = new AutorRepository(ctx);
        var svc = new AutorService(repo);
        var created = svc.Add(new CrearAutorDto { Nombre = "A", Apellido = "B" });
        var found = svc.Get(created.Id);
        Assert.NotNull(found);
        Assert.Equal(created.Id, found!.Id);
    }

    [Fact]
    public void Update_UpdatesFields()
    {
        using var ctx = CreateContext();
        var svc = new AutorService(new AutorRepository(ctx));
        var created = svc.Add(new CrearAutorDto { Nombre = "A", Apellido = "B" });
        var ok = svc.Update(new AutorDto { Id = created.Id, Nombre = "AA", Apellido = "BB" });
        Assert.True(ok);
        var updated = svc.Get(created.Id)!;
        Assert.Equal("AA", updated.Nombre);
        Assert.Equal("BB", updated.Apellido);
    }

    [Fact]
    public void Delete_ReturnsTrueFalse()
    {
        using var ctx = CreateContext();
        var svc = new AutorService(new AutorRepository(ctx));
        var created = svc.Add(new CrearAutorDto { Nombre = "A", Apellido = "B" });
        var ok = svc.Delete(created.Id);
        Assert.True(ok);
        var notOk = svc.Delete(99999);
        Assert.False(notOk);
    }
}
