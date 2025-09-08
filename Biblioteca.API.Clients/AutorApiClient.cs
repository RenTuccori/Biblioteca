using Biblioteca.DTOs;
using System.Text;
using System.Text.Json;

namespace Biblioteca.API.Clients
{
    public class AutorApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public AutorApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<IEnumerable<AutorDto>> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/autores");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<IEnumerable<AutorDto>>(json, _jsonOptions) ?? new List<AutorDto>();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al obtener autores: {ex.Message}", ex);
            }
        }

        public async Task<AutorDto?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/autores/{id}");
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    return null;

                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<AutorDto>(json, _jsonOptions);
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al obtener autor {id}: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<AutorDto>> GetByCriteriaAsync(string texto)
        {
            try
            {
                var encodedTexto = Uri.EscapeDataString(texto ?? "");
                var response = await _httpClient.GetAsync($"api/autores/criteria?texto={encodedTexto}");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<IEnumerable<AutorDto>>(json, _jsonOptions) ?? new List<AutorDto>();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al buscar autores: {ex.Message}", ex);
            }
        }

        public async Task<AutorDto> CreateAsync(CrearAutorDto autor)
        {
            try
            {
                var json = JsonSerializer.Serialize(autor, _jsonOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("api/autores", content);
                response.EnsureSuccessStatusCode();

                var responseJson = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<AutorDto>(responseJson, _jsonOptions)!;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al crear autor: {ex.Message}", ex);
            }
        }

        public async Task<bool> UpdateAsync(AutorDto autor)
        {
            try
            {
                var json = JsonSerializer.Serialize(autor, _jsonOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync("api/autores", content);
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al actualizar autor: {ex.Message}", ex);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/autores/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al eliminar autor {id}: {ex.Message}", ex);
            }
        }
    }
}