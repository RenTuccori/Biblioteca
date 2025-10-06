using Biblioteca.DTOs;
using System.Text;
using System.Text.Json;

namespace Biblioteca.API.Clients
{
    public class UsuarioApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public UsuarioApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<IEnumerable<UsuarioDto>> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/usuarios");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<IEnumerable<UsuarioDto>>(json, _jsonOptions) ?? new List<UsuarioDto>();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al obtener usuarios: {ex.Message}", ex);
            }
        }

        public async Task<UsuarioDto?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/usuarios/{id}");
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    return null;

                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<UsuarioDto>(json, _jsonOptions);
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al obtener usuario {id}: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<UsuarioDto>> GetByRolAsync(string rol)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/usuarios/rol/{rol}");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<IEnumerable<UsuarioDto>>(json, _jsonOptions) ?? new List<UsuarioDto>();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al obtener usuarios por rol: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<UsuarioDto>> GetByCriteriaAsync(string texto)
        {
            try
            {
                var encodedTexto = Uri.EscapeDataString(texto ?? "");
                var response = await _httpClient.GetAsync($"api/usuarios/criteria?texto={encodedTexto}");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<IEnumerable<UsuarioDto>>(json, _jsonOptions) ?? new List<UsuarioDto>();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al buscar usuarios: {ex.Message}", ex);
            }
        }

        public async Task<UsuarioDto> CreateAsync(CrearUsuarioDto usuario)
        {
            try
            {
                var json = JsonSerializer.Serialize(usuario, _jsonOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("api/usuarios", content);
                response.EnsureSuccessStatusCode();

                var responseJson = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<UsuarioDto>(responseJson, _jsonOptions)!;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al crear usuario: {ex.Message}", ex);
            }
        }

        public async Task<bool> UpdateAsync(UsuarioDto usuario)
        {
            try
            {
                var json = JsonSerializer.Serialize(usuario, _jsonOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync("api/usuarios", content);
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al actualizar usuario: {ex.Message}", ex);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/usuarios/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al eliminar usuario {id}: {ex.Message}", ex);
            }
        }
    }
}
