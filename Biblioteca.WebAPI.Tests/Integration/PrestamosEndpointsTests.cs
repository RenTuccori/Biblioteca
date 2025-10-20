using System;
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

public class PrestamosEndpointsTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly JsonSerializerOptions _json = new() { PropertyNameCaseInsensitive = true };

    public PrestamosEndpointsTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Admin_Can_Create_And_Return_Prestamo()
    {
        var client = _factory.CreateClient();
        var token = AuthHelpers.GenerateTestJwt(
            "admin",
            "administrador",
            new[]{
                "prestamos.leer","prestamos.agregar","prestamos.actualizar",
                "libros.leer","libros.agregar",
                "autores.leer","autores.agregar",
                "generos.leer","generos.agregar",
                "editoriales.leer","editoriales.agregar",
                "personas.leer","personas.agregar",
                "usuarios.leer","usuarios.agregar"
            });
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var suffix = Guid.NewGuid().ToString("N").Substring(0,8);

        // Crear dependencias mínimas con nombres únicos
        var respAutor = await client.PostAsync("/api/autores", new StringContent(JsonSerializer.Serialize(new CrearAutorDto{ Nombre = $"A-{suffix}", Apellido = $"B-{suffix}"},_json), Encoding.UTF8, "application/json"));
        Assert.True(respAutor.IsSuccessStatusCode, $"POST /api/autores: {(int)respAutor.StatusCode} {respAutor.StatusCode}. Body: {await respAutor.Content.ReadAsStringAsync()}");
        var autor = JsonSerializer.Deserialize<AutorDto>(await respAutor.Content.ReadAsStringAsync(), _json)!;

        var respGenero = await client.PostAsync("/api/generos", new StringContent(JsonSerializer.Serialize(new CrearGeneroDto{ Nombre = $"G-{suffix}"},_json), Encoding.UTF8, "application/json"));
        Assert.True(respGenero.IsSuccessStatusCode, $"POST /api/generos: {(int)respGenero.StatusCode} {respGenero.StatusCode}. Body: {await respGenero.Content.ReadAsStringAsync()}");
        var genero = JsonSerializer.Deserialize<GeneroDto>(await respGenero.Content.ReadAsStringAsync(), _json)!;

        var respEditorial = await client.PostAsync("/api/editoriales", new StringContent(JsonSerializer.Serialize(new CrearEditorialDto{ Nombre = $"E-{suffix}"},_json), Encoding.UTF8, "application/json"));
        Assert.True(respEditorial.IsSuccessStatusCode, $"POST /api/editoriales: {(int)respEditorial.StatusCode} {respEditorial.StatusCode}. Body: {await respEditorial.Content.ReadAsStringAsync()}");
        var editorial = JsonSerializer.Deserialize<EditorialDto>(await respEditorial.Content.ReadAsStringAsync(), _json)!;

        var isbn = Guid.NewGuid().ToString("N");
        var respLibro = await client.PostAsync("/api/libros", new StringContent(JsonSerializer.Serialize(new CrearLibroDto{ Titulo = $"T-{suffix}", ISBN = isbn, AutorId = autor.Id, GeneroId = genero.Id, EditorialId = editorial.Id, Estado = "disponible"},_json), Encoding.UTF8, "application/json"));
        Assert.True(respLibro.IsSuccessStatusCode, $"POST /api/libros: {(int)respLibro.StatusCode} {respLibro.StatusCode}. Body: {await respLibro.Content.ReadAsStringAsync()}");
        var libro = JsonSerializer.Deserialize<LibroDto>(await respLibro.Content.ReadAsStringAsync(), _json)!;

        var respPersona = await client.PostAsync("/api/personas", new StringContent(JsonSerializer.Serialize(new CrearPersonaDto{ Nombre = $"Nom-{suffix}", Apellido = $"Ape-{suffix}", Dni = Guid.NewGuid().ToString("N").Substring(0,8), Email = $"e{Guid.NewGuid():N}@e.com"},_json), Encoding.UTF8, "application/json"));
        Assert.True(respPersona.IsSuccessStatusCode, $"POST /api/personas: {(int)respPersona.StatusCode} {respPersona.StatusCode}. Body: {await respPersona.Content.ReadAsStringAsync()}");
        var persona = JsonSerializer.Deserialize<PersonaDto>(await respPersona.Content.ReadAsStringAsync(), _json)!;

        var respUsuario = await client.PostAsync("/api/usuarios", new StringContent(JsonSerializer.Serialize(new CrearUsuarioDto{ NombreUsuario = $"socio_{suffix}", Clave = "socio123", Rol = "socio", PersonaId = persona.Id},_json), Encoding.UTF8, "application/json"));
        Assert.True(respUsuario.IsSuccessStatusCode, $"POST /api/usuarios: {(int)respUsuario.StatusCode} {respUsuario.StatusCode}. Body: {await respUsuario.Content.ReadAsStringAsync()}");
        var usuario = JsonSerializer.Deserialize<UsuarioDto>(await respUsuario.Content.ReadAsStringAsync(), _json)!;

        // Crear préstamo
        var crear = new CrearPrestamoDto{ LibroId = libro.Id, SocioId = usuario.Id, FechaPrestamo = DateTime.Now, FechaDevolucionPrevista = DateTime.Now.AddDays(7)};
        var respCreate = await client.PostAsync("/api/prestamos", new StringContent(JsonSerializer.Serialize(crear,_json), Encoding.UTF8, "application/json"));
        Assert.True(respCreate.IsSuccessStatusCode, $"POST /api/prestamos: {(int)respCreate.StatusCode} {respCreate.StatusCode}. Body: {await respCreate.Content.ReadAsStringAsync()}");
        var created = JsonSerializer.Deserialize<PrestamoDto>(await respCreate.Content.ReadAsStringAsync(), _json)!;
        created.Should().NotBeNull();

        // Devolver
        var respDev = await client.PostAsync($"/api/prestamos/{created.Id}/devolver", new StringContent("", Encoding.UTF8, "application/json"));
        Assert.True(respDev.IsSuccessStatusCode, $"POST /api/prestamos/{created.Id}/devolver: {(int)respDev.StatusCode} {respDev.StatusCode}. Body: {await respDev.Content.ReadAsStringAsync()}");
    }
}