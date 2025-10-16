using Biblioteca.DTOs;
using System.Text;
using System.Text.Json;

namespace Biblioteca.API.Clients
{
    public class LibroApiClient : BaseApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public LibroApiClient(HttpClient httpClient) : base(httpClient)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<IEnumerable<LibroDto>> GetAllAsync()
        {
            var client = await CreateHttpClientAsync();
            var response = await client.GetAsync("api/libros");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<LibroDto>>(json, _jsonOptions) ?? new List<LibroDto>();
        }

        public async Task<LibroDto?> GetByIdAsync(int id)
        {
            var client = await CreateHttpClientAsync();
            var response = await client.GetAsync($"api/libros/{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound) return null;
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<LibroDto>(json, _jsonOptions);
        }

        public async Task<IEnumerable<LibroDto>> GetByCriteriaAsync(string texto)
        {
            var client = await CreateHttpClientAsync();
            var encodedTexto = Uri.EscapeDataString(texto ?? "");
            var response = await client.GetAsync($"api/libros/criteria?texto={encodedTexto}");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<LibroDto>>(json, _jsonOptions) ?? new List<LibroDto>();
        }

        public async Task<IEnumerable<LibroDto>> GetByAutor(int autorId)
        {
            var client = await CreateHttpClientAsync();
            var response = await client.GetAsync($"api/libros/autor/{autorId}");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<LibroDto>>(json, _jsonOptions) ?? new List<LibroDto>();
        }

        public async Task<IEnumerable<LibroDto>> GetByGenero(int generoId)
        {
            var client = await CreateHttpClientAsync();
            var response = await client.GetAsync($"api/libros/genero/{generoId}");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<LibroDto>>(json, _jsonOptions) ?? new List<LibroDto>();
        }

        public async Task<IEnumerable<LibroDto>> GetByEditorial(int editorialId)
        {
            var client = await CreateHttpClientAsync();
            var response = await client.GetAsync($"api/libros/editorial/{editorialId}");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<LibroDto>>(json, _jsonOptions) ?? new List<LibroDto>();
        }

        public async Task<IEnumerable<LibroDto>> GetByEstado(string estado)
        {
            var client = await CreateHttpClientAsync();
            var response = await client.GetAsync($"api/libros/estado/{estado}");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<LibroDto>>(json, _jsonOptions) ?? new List<LibroDto>();
        }

        public async Task<LibroDto> CreateAsync(CrearLibroDto libro)
        {
            var client = await CreateHttpClientAsync();
            var json = JsonSerializer.Serialize(libro, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("api/libros", content);
            response.EnsureSuccessStatusCode();
            var responseJson = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<LibroDto>(responseJson, _jsonOptions)!;
        }

        public async Task<bool> UpdateAsync(LibroDto libro)
        {
            var client = await CreateHttpClientAsync();
            var json = JsonSerializer.Serialize(libro, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PutAsync("api/libros", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var client = await CreateHttpClientAsync();
            var response = await client.DeleteAsync($"api/libros/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}