using Biblioteca.DTOs;
using System.Text;
using System.Text.Json;

namespace Biblioteca.API.Clients
{
    public class LibroApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public LibroApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<IEnumerable<LibroDto>> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/libros");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<IEnumerable<LibroDto>>(json, _jsonOptions) ?? new List<LibroDto>();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al obtener libros: {ex.Message}", ex);
            }
        }

        public async Task<LibroDto?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/libros/{id}");
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    return null;

                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<LibroDto>(json, _jsonOptions);
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al obtener libro {id}: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<LibroDto>> GetByCriteriaAsync(string texto)
        {
            try
            {
                var encodedTexto = Uri.EscapeDataString(texto ?? "");
                var response = await _httpClient.GetAsync($"api/libros/criteria?texto={encodedTexto}");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<IEnumerable<LibroDto>>(json, _jsonOptions) ?? new List<LibroDto>();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al buscar libros: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<LibroDto>> GetByAutorAsync(int autorId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/libros/autor/{autorId}");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<IEnumerable<LibroDto>>(json, _jsonOptions) ?? new List<LibroDto>();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al obtener libros del autor {autorId}: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<LibroDto>> GetByGeneroAsync(int generoId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/libros/genero/{generoId}");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<IEnumerable<LibroDto>>(json, _jsonOptions) ?? new List<LibroDto>();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al obtener libros del género {generoId}: {ex.Message}", ex);
            }
        }

        public async Task<LibroDto> CreateAsync(CrearLibroDto libro)
        {
            try
            {
                var json = JsonSerializer.Serialize(libro, _jsonOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("api/libros", content);
                response.EnsureSuccessStatusCode();

                var responseJson = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<LibroDto>(responseJson, _jsonOptions)!;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al crear libro: {ex.Message}", ex);
            }
        }

        public async Task<bool> UpdateAsync(LibroDto libro)
        {
            try
            {
                var json = JsonSerializer.Serialize(libro, _jsonOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync("api/libros", content);
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al actualizar libro: {ex.Message}", ex);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/libros/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al eliminar libro {id}: {ex.Message}", ex);
            }
        }
    }
}