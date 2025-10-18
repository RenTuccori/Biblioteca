using System;
using System.Linq;
using Biblioteca.Data;
using Biblioteca.Data.Entities;
using Biblioteca.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Biblioteca.Domain.Services.Tests.Services;

public class EditorialServiceTests
{
    private static BibliotecaContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<BibliotecaContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        var ctx = new BibliotecaContext(options);
        ctx.Editoriales.Add(new EditorialEntity { Id = 400, Nombre = "Ed Prueba" });
        ctx.SaveChanges();
        return ctx;
    }

    [Fact]
    public void GetAll_ReturnsItems()
    {
        using var ctx = CreateContext();
        var svc = new EditorialService(new EditorialRepository(ctx));
        var data = svc.GetAll().ToList();
        Assert.True(data.Count >= 1);
    }
}
