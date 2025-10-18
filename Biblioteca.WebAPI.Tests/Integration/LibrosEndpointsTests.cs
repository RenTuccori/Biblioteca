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

public class LibrosEndpointsTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly JsonSerializerOptions _json = new() { PropertyNameCaseInsensitive = true };

    public LibrosEndpointsTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Admin_CRUD_Libros()
    {
        var client = _factory.CreateClient();
        var token = AuthHelpers.GenerateTestJwt(
            "admin",
            "administrador",
            new[]{
                "libros.leer","libros.agregar","libros.actualizar","libros.eliminar",
                "autores.leer","autores.agregar",
                "generos.leer","generos.agregar",
                "editoriales.leer","editoriales.agregar"
            });
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // Crear dependencias mínimas
        var crearAutor = new CrearAutorDto{ Nombre = "A", Apellido = "B" };
        var respAutor = await client.PostAsync("/api/autores", new StringContent(JsonSerializer.Serialize(crearAutor,_json), Encoding.UTF8, "application/json"));
        Assert.True(respAutor.IsSuccessStatusCode, $"POST /api/autores: {(int)respAutor.StatusCode} {respAutor.StatusCode}. Body: {await respAutor.Content.ReadAsStringAsync()}");
        var autor = JsonSerializer.Deserialize<AutorDto>(await respAutor.Content.ReadAsStringAsync(), _json)!;

        var crearGenero = new CrearGeneroDto{ Nombre = "G" };
        var respGenero = await client.PostAsync("/api/generos", new StringContent(JsonSerializer.Serialize(crearGenero,_json), Encoding.UTF8, "application/json"));
        Assert.True(respGenero.IsSuccessStatusCode, $"POST /api/generos: {(int)respGenero.StatusCode} {respGenero.StatusCode}. Body: {await respGenero.Content.ReadAsStringAsync()}");
        var genero = JsonSerializer.Deserialize<GeneroDto>(await respGenero.Content.ReadAsStringAsync(), _json)!;

        var crearEditorial = new CrearEditorialDto{ Nombre = "E" };
        var respEditorial = await client.PostAsync("/api/editoriales", new StringContent(JsonSerializer.Serialize(crearEditorial,_json), Encoding.UTF8, "application/json"));
        Assert.True(respEditorial.IsSuccessStatusCode, $"POST /api/editoriales: {(int)respEditorial.StatusCode} {respEditorial.StatusCode}. Body: {await respEditorial.Content.ReadAsStringAsync()}");
        var editorial = JsonSerializer.Deserialize<EditorialDto>(await respEditorial.Content.ReadAsStringAsync(), _json)!;

        // Crear libro (ISBN único)
        var isbn = System.Guid.NewGuid().ToString("N");
        var crearLibro = new CrearLibroDto { Titulo = "T", ISBN = isbn, AutorId = autor.Id, GeneroId = genero.Id, EditorialId = editorial.Id, Estado = "disponible" };
        var respCreate = await client.PostAsync("/api/libros", new StringContent(JsonSerializer.Serialize(crearLibro,_json), Encoding.UTF8, "application/json"));
        Assert.True(respCreate.IsSuccessStatusCode, $"POST /api/libros: {(int)respCreate.StatusCode} {respCreate.StatusCode}. Body: {await respCreate.Content.ReadAsStringAsync()}");
        var created = JsonSerializer.Deserialize<LibroDto>(await respCreate.Content.ReadAsStringAsync(), _json)!;
        created.Id.Should().BeGreaterThan(0);

        var respGet = await client.GetAsync($"/api/libros/{created.Id}");
        Assert.True(respGet.IsSuccessStatusCode, $"GET /api/libros/{created.Id}: {(int)respGet.StatusCode} {respGet.StatusCode}. Body: {await respGet.Content.ReadAsStringAsync()}");

        var update = new LibroDto { Id = created.Id, Titulo = "TT", ISBN = isbn, AutorId = autor.Id, GeneroId = genero.Id, EditorialId = editorial.Id, Estado = "disponible" };
        var respPut = await client.PutAsync("/api/libros", new StringContent(JsonSerializer.Serialize(update,_json), Encoding.UTF8, "application/json"));
        Assert.True(respPut.IsSuccessStatusCode, $"PUT /api/libros: {(int)respPut.StatusCode} {respPut.StatusCode}. Body: {await respPut.Content.ReadAsStringAsync()}");

        var respDel = await client.DeleteAsync($"/api/libros/{created.Id}");
        Assert.True(respDel.IsSuccessStatusCode, $"DELETE /api/libros/{created.Id}: {(int)respDel.StatusCode} {respDel.StatusCode}. Body: {await respDel.Content.ReadAsStringAsync()}");
    }
}