using Biblioteca.DTOs;
using System.Text;
using System.Text.Json;

namespace Biblioteca.API.Clients
{
    public class PersonaApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public PersonaApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<IEnumerable<PersonaDto>> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/personas");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<IEnumerable<PersonaDto>>(json, _jsonOptions) ?? new List<PersonaDto>();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al obtener personas: {ex.Message}", ex);
            }
        }

        public async Task<PersonaDto?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/personas/{id}");
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    return null;

                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<PersonaDto>(json, _jsonOptions);
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al obtener persona {id}: {ex.Message}", ex);
            }
        }

        public async Task<PersonaDto> CreateAsync(CrearPersonaDto persona)
        {
            try
            {
                var json = JsonSerializer.Serialize(persona, _jsonOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("api/personas", content);
                response.EnsureSuccessStatusCode();

                var responseJson = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<PersonaDto>(responseJson, _jsonOptions)!;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al crear persona: {ex.Message}", ex);
            }
        }

        public async Task<bool> UpdateAsync(PersonaDto persona)
        {
            try
            {
                var json = JsonSerializer.Serialize(persona, _jsonOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync("api/personas", content);
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al actualizar persona: {ex.Message}", ex);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/personas/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al eliminar persona {id}: {ex.Message}", ex);
            }
        }
    }
}
