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

public class GenerosEndpointsTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly JsonSerializerOptions _json = new() { PropertyNameCaseInsensitive = true };

    public GenerosEndpointsTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Admin_CRUD_Generos()
    {
        var client = _factory.CreateClient();
        var token = AuthHelpers.GenerateTestJwt("admin","administrador", new[]{"generos.leer","generos.agregar","generos.actualizar","generos.eliminar"});
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var crear = new CrearGeneroDto { Nombre = "G" };
        var respCreate = await client.PostAsync("/api/generos", new StringContent(JsonSerializer.Serialize(crear,_json), Encoding.UTF8, "application/json"));
        respCreate.EnsureSuccessStatusCode();
        var created = JsonSerializer.Deserialize<GeneroDto>(await respCreate.Content.ReadAsStringAsync(), _json)!;
        created.Id.Should().BeGreaterThan(0);

        var respGet = await client.GetAsync($"/api/generos/{created.Id}");
        respGet.EnsureSuccessStatusCode();

        var update = new GeneroDto { Id = created.Id, Nombre = "GG" };
        var respPut = await client.PutAsync("/api/generos", new StringContent(JsonSerializer.Serialize(update,_json), Encoding.UTF8, "application/json"));
        respPut.EnsureSuccessStatusCode();

        var respDel = await client.DeleteAsync($"/api/generos/{created.Id}");
        respDel.EnsureSuccessStatusCode();
    }
}