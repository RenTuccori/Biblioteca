using Biblioteca.DTOs;
using System.Text;
using System.Text.Json;

namespace Biblioteca.API.Clients
{
    public class GeneroApiClient : BaseApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public GeneroApiClient(HttpClient httpClient) : base(httpClient)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<IEnumerable<GeneroDto>> GetAllAsync()
        {
            try
            {
                var client = await CreateHttpClientAsync();
                var response = await client.GetAsync("api/generos");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<IEnumerable<GeneroDto>>(json, _jsonOptions) ?? new List<GeneroDto>();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al obtener géneros: {ex.Message}", ex);
            }
        }

        public async Task<GeneroDto?> GetByIdAsync(int id)
        {
            try
            {
                var client = await CreateHttpClientAsync();
                var response = await client.GetAsync($"api/generos/{id}");
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    return null;

                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<GeneroDto>(json, _jsonOptions);
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al obtener género {id}: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<GeneroDto>> GetByCriteriaAsync(string texto)
        {
            try
            {
                var client = await CreateHttpClientAsync();
                var encodedTexto = Uri.EscapeDataString(texto ?? "");
                var response = await client.GetAsync($"api/generos/criteria?texto={encodedTexto}");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<IEnumerable<GeneroDto>>(json, _jsonOptions) ?? new List<GeneroDto>();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al buscar géneros: {ex.Message}", ex);
            }
        }

        public async Task<GeneroDto> CreateAsync(CrearGeneroDto genero)
        {
            try
            {
                var client = await CreateHttpClientAsync();
                var json = JsonSerializer.Serialize(genero, _jsonOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("api/generos", content);
                response.EnsureSuccessStatusCode();

                var responseJson = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<GeneroDto>(responseJson, _jsonOptions)!;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al crear género: {ex.Message}", ex);
            }
        }

        public async Task<bool> UpdateAsync(GeneroDto genero)
        {
            try
            {
                var client = await CreateHttpClientAsync();
                var json = JsonSerializer.Serialize(genero, _jsonOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PutAsync("api/generos", content);
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al actualizar género: {ex.Message}", ex);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var client = await CreateHttpClientAsync();
                var response = await client.DeleteAsync($"api/generos/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al eliminar género {id}: {ex.Message}", ex);
            }
        }
    }
}