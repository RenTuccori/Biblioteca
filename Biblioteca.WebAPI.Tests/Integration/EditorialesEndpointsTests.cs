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

public class EditorialesEndpointsTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly JsonSerializerOptions _json = new() { PropertyNameCaseInsensitive = true };

    public EditorialesEndpointsTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Admin_CRUD_Editoriales()
    {
        var client = _factory.CreateClient();
        var token = AuthHelpers.GenerateTestJwt("admin","administrador", new[]{"editoriales.leer","editoriales.agregar","editoriales.actualizar","editoriales.eliminar"});
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var crear = new CrearEditorialDto { Nombre = "E" };
        var respCreate = await client.PostAsync("/api/editoriales", new StringContent(JsonSerializer.Serialize(crear,_json), Encoding.UTF8, "application/json"));
        respCreate.EnsureSuccessStatusCode();
        var created = JsonSerializer.Deserialize<EditorialDto>(await respCreate.Content.ReadAsStringAsync(), _json)!;
        created.Id.Should().BeGreaterThan(0);

        var respGet = await client.GetAsync($"/api/editoriales/{created.Id}");
        respGet.EnsureSuccessStatusCode();

        var update = new EditorialDto { Id = created.Id, Nombre = "EE" };
        var respPut = await client.PutAsync("/api/editoriales", new StringContent(JsonSerializer.Serialize(update,_json), Encoding.UTF8, "application/json"));
        respPut.EnsureSuccessStatusCode();

        var respDel = await client.DeleteAsync($"/api/editoriales/{created.Id}");
        respDel.EnsureSuccessStatusCode();
    }
}