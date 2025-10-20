using Biblioteca.DTOs;
using System.Text;
using System.Text.Json;

namespace Biblioteca.API.Clients
{
    public class PersonaApiClient : BaseApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public PersonaApiClient(HttpClient httpClient) : base(httpClient)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<IEnumerable<PersonaDto>> GetAllAsync()
        {
            var client = await CreateHttpClientAsync();
            var response = await client.GetAsync("api/personas");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<PersonaDto>>(json, _jsonOptions) ?? new List<PersonaDto>();
        }

        public async Task<PersonaDto?> GetByIdAsync(int id)
        {
            var client = await CreateHttpClientAsync();
            var response = await client.GetAsync($"api/personas/{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound) return null;
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<PersonaDto>(json, _jsonOptions);
        }

        public async Task<IEnumerable<PersonaDto>> GetByCriteriaAsync(string texto)
        {
            var client = await CreateHttpClientAsync();
            var encodedTexto = Uri.EscapeDataString(texto ?? "");
            var response = await client.GetAsync($"api/personas/criteria?texto={encodedTexto}");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<PersonaDto>>(json, _jsonOptions) ?? new List<PersonaDto>();
        }

        public async Task<PersonaDto> CreateAsync(CrearPersonaDto persona)
        {
            var client = await CreateHttpClientAsync();
            var json = JsonSerializer.Serialize(persona, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("api/personas", content);
            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                var message = string.IsNullOrWhiteSpace(body) ? response.ReasonPhrase : body;
                throw new Exception(message);
            }
            var responseJson = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<PersonaDto>(responseJson, _jsonOptions)!;
        }

        public async Task<bool> UpdateAsync(PersonaDto persona)
        {
            var client = await CreateHttpClientAsync();
            var json = JsonSerializer.Serialize(persona, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PutAsync("api/personas", content);
            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                var message = string.IsNullOrWhiteSpace(body) ? response.ReasonPhrase : body;
                throw new Exception(message);
            }
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var client = await CreateHttpClientAsync();
            var response = await client.DeleteAsync($"api/personas/{id}");
            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                var message = string.IsNullOrWhiteSpace(body) ? response.ReasonPhrase : body;
                throw new Exception(message);
            }
            return true;
        }
    }
}
