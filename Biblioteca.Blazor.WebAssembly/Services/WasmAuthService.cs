using System.Text;
using System.Text.Json;
using Microsoft.JSInterop;
using Biblioteca.API.Clients;
using Biblioteca.DTOs;

namespace Biblioteca.Blazor.WebAssembly.Services
{
    public class WasmAuthService : IAuthService
    {
        private readonly IJSRuntime _js;
        private readonly AuthApiClient _authApiClient;
        private string? _token;
        private DateTime _expiresAt;
        private string? _username;
        private readonly HashSet<string> _permissions = new(StringComparer.OrdinalIgnoreCase);
        private readonly HashSet<string> _roles = new(StringComparer.OrdinalIgnoreCase);

        private const string StorageKey = "auth_state";

        public event Action? AuthenticationStateChanged;

        public WasmAuthService(IJSRuntime js, AuthApiClient authApiClient)
        {
            _js = js;
            _authApiClient = authApiClient;
        }

        public async Task InitializeAsync()
        {
            try
            {
                var json = await _js.InvokeAsync<string?>("localStorage.getItem", StorageKey);
                if (!string.IsNullOrEmpty(json))
                {
                    var state = JsonSerializer.Deserialize<AuthState>(json!);
                    if (state != null)
                    {
                        _token = state.Token;
                        _expiresAt = state.ExpiresAt;
                        _username = state.Username;
                        if (!string.IsNullOrEmpty(_token))
                            ParsePermissionsFromToken(_token);
                    }
                }
            }
            catch { }
        }

        public Task<bool> IsAuthenticatedAsync() => Task.FromResult(!string.IsNullOrEmpty(_token) && DateTime.UtcNow < _expiresAt);
        public Task<string?> GetTokenAsync() => Task.FromResult(_token);
        public Task<string?> GetUsernameAsync() => Task.FromResult(_username);

        public async Task<bool> LoginAsync(LoginRequest request)
        {
            var res = await _authApiClient.LoginAsync(request);
            if (res == null || string.IsNullOrEmpty(res.Token)) return false;
            _token = res.Token;
            _expiresAt = res.ExpiresAt;
            _username = res.Username;
            ParsePermissionsFromToken(_token);
            await PersistAsync();
            AuthenticationStateChanged?.Invoke();
            return true;
        }

        public async Task LogoutAsync()
        {
            _token = null;
            _username = null;
            _expiresAt = DateTime.MinValue;
            _permissions.Clear();
            _roles.Clear();
            try { await _js.InvokeVoidAsync("localStorage.removeItem", StorageKey); } catch { }
            AuthenticationStateChanged?.Invoke();
        }

        public Task<bool> CheckTokenExpirationAsync()
        {
            if (string.IsNullOrEmpty(_token)) return Task.FromResult(false);
            var valid = DateTime.UtcNow < _expiresAt;
            if (!valid)
            {
                _ = LogoutAsync();
            }
            return Task.FromResult(valid);
        }

        public Task<bool> HasPermissionAsync(string permission)
        {
            if (_roles.Contains("administrador")) return Task.FromResult(true);
            return Task.FromResult(_permissions.Contains(permission));
        }

        public Task<bool> IsInRoleAsync(string role)
        {
            return Task.FromResult(_roles.Contains(role));
        }

        private async Task PersistAsync()
        {
            try
            {
                var json = JsonSerializer.Serialize(new AuthState { Token = _token, ExpiresAt = _expiresAt, Username = _username });
                await _js.InvokeVoidAsync("localStorage.setItem", StorageKey, json);
            }
            catch { }
        }

        private void ParsePermissionsFromToken(string token)
        {
            _permissions.Clear();
            _roles.Clear();
            try
            {
                var parts = token.Split('.');
                if (parts.Length < 2) return;
                string payload = parts[1];
                payload = payload.Replace('-', '+').Replace('_', '/');
                switch (payload.Length % 4)
                {
                    case 2: payload += "=="; break;
                    case 3: payload += "="; break;
                }
                var json = Encoding.UTF8.GetString(Convert.FromBase64String(payload));
                using var doc = JsonDocument.Parse(json);
                var root = doc.RootElement;

                string[] roleKeys = new[] { "role", "roles", "http://schemas.microsoft.com/ws/2008/06/identity/claims/role" };
                foreach (var key in roleKeys)
                {
                    if (root.TryGetProperty(key, out var roleEl))
                    {
                        if (roleEl.ValueKind == JsonValueKind.Array)
                        {
                            foreach (var item in roleEl.EnumerateArray())
                                _roles.Add(item.GetString() ?? string.Empty);
                        }
                        else if (roleEl.ValueKind == JsonValueKind.String)
                        {
                            _roles.Add(roleEl.GetString() ?? string.Empty);
                        }
                    }
                }

                if (root.TryGetProperty("permiso", out var permEl))
                {
                    if (permEl.ValueKind == JsonValueKind.Array)
                    {
                        foreach (var item in permEl.EnumerateArray())
                            _permissions.Add(item.GetString() ?? string.Empty);
                    }
                    else if (permEl.ValueKind == JsonValueKind.String)
                    {
                        _permissions.Add(permEl.GetString() ?? string.Empty);
                    }
                }
            }
            catch { }
        }

        private class AuthState
        {
            public string? Token { get; set; }
            public DateTime ExpiresAt { get; set; }
            public string? Username { get; set; }
        }
    }
}
