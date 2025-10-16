using Biblioteca.DTOs;
using System.Text;
using System.Text.Json;

namespace Biblioteca.API.Clients
{
    public class EditorialApiClient : BaseApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public EditorialApiClient(HttpClient httpClient) : base(httpClient)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<IEnumerable<EditorialDto>> GetAllAsync()
        {
            var client = await CreateHttpClientAsync();
            var response = await client.GetAsync("api/editoriales");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<EditorialDto>>(json, _jsonOptions) ?? new List<EditorialDto>();
        }

        public async Task<EditorialDto?> GetByIdAsync(int id)
        {
            var client = await CreateHttpClientAsync();
            var response = await client.GetAsync($"api/editoriales/{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound) return null;
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<EditorialDto>(json, _jsonOptions);
        }

        public async Task<IEnumerable<EditorialDto>> GetByCriteriaAsync(string texto)
        {
            var client = await CreateHttpClientAsync();
            var encodedTexto = Uri.EscapeDataString(texto ?? "");
            var response = await client.GetAsync($"api/editoriales/criteria?texto={encodedTexto}");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<EditorialDto>>(json, _jsonOptions) ?? new List<EditorialDto>();
        }

        public async Task<EditorialDto> CreateAsync(CrearEditorialDto editorial)
        {
            var client = await CreateHttpClientAsync();
            var json = JsonSerializer.Serialize(editorial, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("api/editoriales", content);
            response.EnsureSuccessStatusCode();
            var responseJson = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<EditorialDto>(responseJson, _jsonOptions)!;
        }

        public async Task<bool> UpdateAsync(EditorialDto editorial)
        {
            var client = await CreateHttpClientAsync();
            var json = JsonSerializer.Serialize(editorial, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PutAsync("api/editoriales", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var client = await CreateHttpClientAsync();
            var response = await client.DeleteAsync($"api/editoriales/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
