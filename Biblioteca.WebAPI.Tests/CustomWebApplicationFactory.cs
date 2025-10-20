using System.Linq;
using Biblioteca.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebAppProgram = Biblioteca.WebAPI.Program;

namespace Biblioteca.WebAPI.Tests;

public class CustomWebApplicationFactory : WebApplicationFactory<WebAppProgram>
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.UseEnvironment("Testing");
        return base.CreateHost(builder);
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var ctx = scope.ServiceProvider.GetRequiredService<BibliotecaContext>();
            ctx.Database.EnsureCreated();
            // Sin seeds adicionales aquí para evitar duplicados
        });
    }
}