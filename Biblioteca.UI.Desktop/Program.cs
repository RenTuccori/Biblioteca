using Biblioteca.UI.Desktop;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Biblioteca.API.Clients;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        Application.SetHighDpiMode(HighDpiMode.SystemAware);
        ApplicationConfiguration.Initialize();

        // Configurar servicios
        var host = CreateHostBuilder().Build();
        Biblioteca.UI.Desktop.ServiceProvider.Initialize(host.Services);

        // Registrar servicio de auth global para los ApiClients
        var auth = host.Services.GetRequiredService<IAuthService>();
        AuthServiceProvider.Register(auth);
        // Inicializar estado previo (si existe)
        _ = auth.InitializeAsync();

        // Mostrar login si no autenticado
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;
        using (var login = services.GetRequiredService<FormLogin>())
        {
            var authOk = auth.IsAuthenticatedAsync().GetAwaiter().GetResult();
            if (!authOk)
            {
                var dlg = login.ShowDialog();
                if (dlg != DialogResult.OK)
                {
                    return; // salir si no se autenticó
                }
            }
        }

        Application.Run(host.Services.GetRequiredService<FormPrincipal>());
    }

    static IHostBuilder CreateHostBuilder() =>
        Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                var apiBaseUrl = "https://localhost:7152/";

                // ApiClients
                services.AddHttpClient<AutorApiClient>(client => client.BaseAddress = new Uri(apiBaseUrl));
                services.AddHttpClient<LibroApiClient>(client => client.BaseAddress = new Uri(apiBaseUrl));
                services.AddHttpClient<GeneroApiClient>(client => client.BaseAddress = new Uri(apiBaseUrl));
                services.AddHttpClient<EditorialApiClient>(client => client.BaseAddress = new Uri(apiBaseUrl));
                services.AddHttpClient<UsuarioApiClient>(client => client.BaseAddress = new Uri(apiBaseUrl));
                services.AddHttpClient<PersonaApiClient>(client => client.BaseAddress = new Uri(apiBaseUrl));
                services.AddHttpClient<PrestamoApiClient>(client => client.BaseAddress = new Uri(apiBaseUrl));
                services.AddHttpClient<AuthApiClient>(client => client.BaseAddress = new Uri(apiBaseUrl));

                // Auth service para escritorio
                services.AddSingleton<IAuthService, SimpleAuthService>();

                // Formularios
                services.AddTransient<FormPrincipal>();
                services.AddTransient<FormAutores>();
                services.AddTransient<FormLibros>();
                services.AddTransient<FormGeneros>();
                services.AddTransient<FormEditoriales>();
                services.AddTransient<FormUsuarios>();
                services.AddTransient<FormPrestamos>();
                services.AddTransient<FormLogin>();
                services.AddTransient<FormChangePassword>();
            });
}