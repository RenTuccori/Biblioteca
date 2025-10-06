using Biblioteca.DTOs;
using System.Text;
using System.Text.Json;

namespace Biblioteca.API.Clients
{
    public class EditorialApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public EditorialApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<IEnumerable<EditorialDto>> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/editoriales");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<IEnumerable<EditorialDto>>(json, _jsonOptions) ?? new List<EditorialDto>();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al obtener editoriales: {ex.Message}", ex);
            }
        }

        public async Task<EditorialDto?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/editoriales/{id}");
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    return null;

                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<EditorialDto>(json, _jsonOptions);
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al obtener editorial {id}: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<EditorialDto>> GetByCriteriaAsync(string texto)
        {
            try
            {
                var encodedTexto = Uri.EscapeDataString(texto ?? "");
                var response = await _httpClient.GetAsync($"api/editoriales/criteria?texto={encodedTexto}");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<IEnumerable<EditorialDto>>(json, _jsonOptions) ?? new List<EditorialDto>();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al buscar editoriales: {ex.Message}", ex);
            }
        }

        public async Task<EditorialDto> CreateAsync(CrearEditorialDto editorial)
        {
            try
            {
                var json = JsonSerializer.Serialize(editorial, _jsonOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("api/editoriales", content);
                response.EnsureSuccessStatusCode();

                var responseJson = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<EditorialDto>(responseJson, _jsonOptions)!;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al crear editorial: {ex.Message}", ex);
            }
        }

        public async Task<bool> UpdateAsync(EditorialDto editorial)
        {
            try
            {
                var json = JsonSerializer.Serialize(editorial, _jsonOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync("api/editoriales", content);
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al actualizar editorial: {ex.Message}", ex);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/editoriales/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al eliminar editorial {id}: {ex.Message}", ex);
            }
        }
    }
}
