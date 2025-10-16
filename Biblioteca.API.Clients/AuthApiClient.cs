using Biblioteca.DTOs;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Biblioteca.API.Clients
{
    public class AuthApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true };

        public AuthApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<LoginResponse?> LoginAsync(LoginRequest request)
        {
            var json = JsonSerializer.Serialize(request, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("auth/login", content);
            if (!response.IsSuccessStatusCode) return null;
            var body = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<LoginResponse>(body, _jsonOptions);
        }

        public async Task<bool> ChangePasswordAsync(ChangePasswordDto dto, string token)
        {
            using var req = new HttpRequestMessage(HttpMethod.Post, "auth/change-password")
            {
                Content = new StringContent(JsonSerializer.Serialize(dto, _jsonOptions), Encoding.UTF8, "application/json")
            };
            req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var res = await _httpClient.SendAsync(req);
            return res.IsSuccessStatusCode;
        }
    }
}
