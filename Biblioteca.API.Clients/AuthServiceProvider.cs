namespace Biblioteca.API.Clients
{
    public static class AuthServiceProvider
    {
        private static IAuthService? _instance;
        public static void Register(IAuthService authService) => _instance = authService;
        public static IAuthService Instance => _instance ?? throw new InvalidOperationException("IAuthService no registrado");
    }
}
