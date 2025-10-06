using Biblioteca.DTOs;
using System.Text;
using System.Text.Json;

namespace Biblioteca.API.Clients
{
    public class PrestamoApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public PrestamoApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<IEnumerable<PrestamoDto>> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/prestamos");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<IEnumerable<PrestamoDto>>(json, _jsonOptions) ?? new List<PrestamoDto>();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al obtener préstamos: {ex.Message}", ex);
            }
        }

        public async Task<PrestamoDto?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/prestamos/{id}");
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    return null;

                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<PrestamoDto>(json, _jsonOptions);
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al obtener préstamo {id}: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<PrestamoDto>> GetActivosAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/prestamos/activos");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<IEnumerable<PrestamoDto>>(json, _jsonOptions) ?? new List<PrestamoDto>();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al obtener préstamos activos: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<PrestamoDto>> GetVencidosAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/prestamos/vencidos");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<IEnumerable<PrestamoDto>>(json, _jsonOptions) ?? new List<PrestamoDto>();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al obtener préstamos vencidos: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<PrestamoDto>> GetBySocioAsync(int socioId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/prestamos/socio/{socioId}");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<IEnumerable<PrestamoDto>>(json, _jsonOptions) ?? new List<PrestamoDto>();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al obtener préstamos del socio: {ex.Message}", ex);
            }
        }

        public async Task<PrestamoDto> CreateAsync(CrearPrestamoDto prestamo)
        {
            try
            {
                var json = JsonSerializer.Serialize(prestamo, _jsonOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("api/prestamos", content);
                response.EnsureSuccessStatusCode();

                var responseJson = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<PrestamoDto>(responseJson, _jsonOptions)!;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al crear préstamo: {ex.Message}", ex);
            }
        }

        public async Task<bool> UpdateAsync(PrestamoDto prestamo)
        {
            try
            {
                var json = JsonSerializer.Serialize(prestamo, _jsonOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync("api/prestamos", content);
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al actualizar préstamo: {ex.Message}", ex);
            }
        }

        public async Task<bool> DevolverAsync(int id, DateTime? fechaDevolucion = null)
        {
            try
            {
                var fecha = fechaDevolucion ?? DateTime.Now;
                var response = await _httpClient.PostAsync($"api/prestamos/{id}/devolver?fechaDevolucion={fecha:yyyy-MM-dd}", null);
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al devolver libro: {ex.Message}", ex);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/prestamos/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al eliminar préstamo {id}: {ex.Message}", ex);
            }
        }
    }
}
