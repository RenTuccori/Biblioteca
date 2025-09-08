using Biblioteca.DTOs;
using System.Text;
using System.Text.Json;

namespace Biblioteca.API.Clients
{
    public class GeneroApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public GeneroApiClient(HttpClient httpClient)
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
                var response = await _httpClient.GetAsync("api/generos");
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
                var response = await _httpClient.GetAsync($"api/generos/{id}");
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
                var encodedTexto = Uri.EscapeDataString(texto ?? "");
                var response = await _httpClient.GetAsync($"api/generos/criteria?texto={encodedTexto}");
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
                var json = JsonSerializer.Serialize(genero, _jsonOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("api/generos", content);
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
                var json = JsonSerializer.Serialize(genero, _jsonOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync("api/generos", content);
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
                var response = await _httpClient.DeleteAsync($"api/generos/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al eliminar género {id}: {ex.Message}", ex);
            }
        }
    }
}