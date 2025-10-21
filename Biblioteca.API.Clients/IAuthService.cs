using Biblioteca.DTOs;

namespace Biblioteca.API.Clients
{
    public interface IAuthService
    {
        event Action? AuthenticationStateChanged;

        Task InitializeAsync();
        Task<bool> IsAuthenticatedAsync();
        Task<string?> GetTokenAsync();
        Task<string?> GetUsernameAsync();
        Task<bool> LoginAsync(LoginRequest request);
        Task LogoutAsync();
        Task<bool> CheckTokenExpirationAsync();
        Task<bool> HasPermissionAsync(string permission);
        Task<bool> IsInRoleAsync(string role);
    }
}
