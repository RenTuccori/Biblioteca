using Biblioteca.DTOs;
using System.Text;
using System.Text.Json;

namespace Biblioteca.API.Clients
{
    public class PrestamoApiClient : BaseApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public PrestamoApiClient(HttpClient httpClient) : base(httpClient)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<IEnumerable<PrestamoDto>> GetAllAsync()
        {
            var client = await CreateHttpClientAsync();
            var response = await client.GetAsync("api/prestamos");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<PrestamoDto>>(json, _jsonOptions) ?? new List<PrestamoDto>();
        }

        public async Task<IEnumerable<PrestamoDto>> GetActivosAsync()
        {
            var client = await CreateHttpClientAsync();
            var response = await client.GetAsync("api/prestamos/activos");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<PrestamoDto>>(json, _jsonOptions) ?? new List<PrestamoDto>();
        }

        public async Task<IEnumerable<PrestamoDto>> GetVencidosAsync()
        {
            var client = await CreateHttpClientAsync();
            var response = await client.GetAsync("api/prestamos/vencidos");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<PrestamoDto>>(json, _jsonOptions) ?? new List<PrestamoDto>();
        }

        public async Task<IEnumerable<PrestamoDto>> GetBySocioAsync(int socioId)
        {
            var client = await CreateHttpClientAsync();
            var response = await client.GetAsync($"api/prestamos/socio/{socioId}");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<PrestamoDto>>(json, _jsonOptions) ?? new List<PrestamoDto>();
        }

        public async Task<PrestamoDto?> GetByIdAsync(int id)
        {
            var client = await CreateHttpClientAsync();
            var response = await client.GetAsync($"api/prestamos/{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound) return null;
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<PrestamoDto>(json, _jsonOptions);
        }

        public async Task<PrestamoDto> CreateAsync(CrearPrestamoDto prestamo)
        {
            var client = await CreateHttpClientAsync();
            var json = JsonSerializer.Serialize(prestamo, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("api/prestamos", content);
            response.EnsureSuccessStatusCode();
            var responseJson = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<PrestamoDto>(responseJson, _jsonOptions)!;
        }

        public async Task<bool> UpdateAsync(PrestamoDto prestamo)
        {
            var client = await CreateHttpClientAsync();
            var json = JsonSerializer.Serialize(prestamo, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PutAsync("api/prestamos", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var client = await CreateHttpClientAsync();
            var response = await client.DeleteAsync($"api/prestamos/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DevolverAsync(int id, DateTime? fechaDevolucion = null)
        {
            var client = await CreateHttpClientAsync();
            var content = new StringContent("", Encoding.UTF8, "application/json");
            var url = $"api/prestamos/{id}/devolver" + (fechaDevolucion.HasValue ? $"?fechaDevolucion={fechaDevolucion.Value:O}" : "");
            var response = await client.PostAsync(url, content);
            return response.IsSuccessStatusCode;
        }
    }
}
