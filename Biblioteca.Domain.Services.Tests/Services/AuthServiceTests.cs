using System.Collections.Generic;
using System.Threading.Tasks;
using Biblioteca.Data;
using Biblioteca.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Biblioteca.Domain.Services.Tests.Services;

public class AuthServiceTests
{
    [Fact]
    public async Task Login_With_DefaultAdmin_FromSeeding_Works()
    {
        var options = new DbContextOptionsBuilder<BibliotecaContext>()
            .UseInMemoryDatabase("AuthTestSeedDomain").Options;
        using var ctx = new BibliotecaContext(options);
        ctx.Database.EnsureCreated();

        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["JwtSettings:SecretKey"] = "test-secret-key-12345678901234567890",
                ["JwtSettings:Issuer"] = "Biblioteca.WebAPI",
                ["JwtSettings:Audience"] = "Biblioteca.Clients",
                ["JwtSettings:ExpirationMinutes"] = "60"
            }!)
            .Build();

        var svc = new AuthService(new UsuarioRepository(ctx), config);
        var res = await svc.LoginAsync(new Biblioteca.DTOs.LoginRequest { Username = "admin", Password = "admin123" });
        Assert.NotNull(res);
        Assert.False(string.IsNullOrEmpty(res!.Token));
    }
}
