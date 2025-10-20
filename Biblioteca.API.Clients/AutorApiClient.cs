using Biblioteca.DTOs;
using System.Text;
using System.Text.Json;

namespace Biblioteca.API.Clients
{
    public class AutorApiClient : BaseApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public AutorApiClient(HttpClient httpClient) : base(httpClient)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<IEnumerable<AutorDto>> GetAllAsync()
        {
            var client = await CreateHttpClientAsync();
            var response = await client.GetAsync("api/autores");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<AutorDto>>(json, _jsonOptions) ?? new List<AutorDto>();
        }

        public async Task<AutorDto?> GetByIdAsync(int id)
        {
            var client = await CreateHttpClientAsync();
            var response = await client.GetAsync($"api/autores/{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound) return null;
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<AutorDto>(json, _jsonOptions);
        }

        public async Task<IEnumerable<AutorDto>> GetByCriteriaAsync(string texto)
        {
            var client = await CreateHttpClientAsync();
            var encodedTexto = Uri.EscapeDataString(texto ?? "");
            var response = await client.GetAsync($"api/autores/criteria?texto={encodedTexto}");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<AutorDto>>(json, _jsonOptions) ?? new List<AutorDto>();
        }

        public async Task<AutorDto> CreateAsync(CrearAutorDto autor)
        {
            var client = await CreateHttpClientAsync();
            var json = JsonSerializer.Serialize(autor, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("api/autores", content);
            response.EnsureSuccessStatusCode();
            var responseJson = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<AutorDto>(responseJson, _jsonOptions)!;
        }

        public async Task<bool> UpdateAsync(AutorDto autor)
        {
            var client = await CreateHttpClientAsync();
            var json = JsonSerializer.Serialize(autor, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PutAsync("api/autores", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var client = await CreateHttpClientAsync();
            var response = await client.DeleteAsync($"api/autores/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}