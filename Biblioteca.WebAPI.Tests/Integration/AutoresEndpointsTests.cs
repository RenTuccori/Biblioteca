using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Biblioteca.DTOs;
using Biblioteca.WebAPI.Tests.Auth;
using FluentAssertions;
using Xunit;

namespace Biblioteca.WebAPI.Tests.Integration;

public class AutoresEndpointsTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly JsonSerializerOptions _json = new() { PropertyNameCaseInsensitive = true };

    public AutoresEndpointsTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Admin_CRUD_Autores()
    {
        var client = _factory.CreateClient();
        var token = AuthHelpers.GenerateTestJwt("admin","administrador", new[]{"autores.leer","autores.agregar","autores.actualizar","autores.eliminar"});
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // Crear
        var crear = new CrearAutorDto { Nombre = "Nombre", Apellido = "Apellido" };
        var respCreate = await client.PostAsync("/api/autores", new StringContent(JsonSerializer.Serialize(crear,_json), Encoding.UTF8, "application/json"));
        respCreate.EnsureSuccessStatusCode();
        var created = JsonSerializer.Deserialize<AutorDto>(await respCreate.Content.ReadAsStringAsync(), _json)!;
        created.Id.Should().BeGreaterThan(0);

        // Get por Id
        var respGet = await client.GetAsync($"/api/autores/{created.Id}");
        respGet.EnsureSuccessStatusCode();
        var fetched = JsonSerializer.Deserialize<AutorDto>(await respGet.Content.ReadAsStringAsync(), _json)!;
        fetched.Id.Should().Be(created.Id);

        // Update
        var update = new AutorDto { Id = created.Id, Nombre = "Nuevo", Apellido = "Apellido" };
        var respPut = await client.PutAsync("/api/autores", new StringContent(JsonSerializer.Serialize(update,_json), Encoding.UTF8, "application/json"));
        respPut.EnsureSuccessStatusCode();

        // Delete
        var respDel = await client.DeleteAsync($"/api/autores/{created.Id}");
        respDel.EnsureSuccessStatusCode();
    }
}