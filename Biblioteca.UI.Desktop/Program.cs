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
        
        // Configurar el proveedor de servicios para toda la aplicación
        Biblioteca.UI.Desktop.ServiceProvider.Initialize(host.Services);

        Application.Run(host.Services.GetRequiredService<FormPrincipal>());
    }

    static IHostBuilder CreateHostBuilder() =>
        Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                // Configurar HttpClient con el puerto correcto de la WebAPI
                var apiBaseUrl = "https://localhost:7152/";
                
                services.AddHttpClient<AutorApiClient>(client =>
                {
                    client.BaseAddress = new Uri(apiBaseUrl);
                });
                
                services.AddHttpClient<LibroApiClient>(client =>
                {
                    client.BaseAddress = new Uri(apiBaseUrl);
                });
                
                services.AddHttpClient<GeneroApiClient>(client =>
                {
                    client.BaseAddress = new Uri(apiBaseUrl);
                });
                
                services.AddHttpClient<EditorialApiClient>(client =>
                {
                    client.BaseAddress = new Uri(apiBaseUrl);
                });
                
                services.AddHttpClient<UsuarioApiClient>(client =>
                {
                    client.BaseAddress = new Uri(apiBaseUrl);
                });
                
                services.AddHttpClient<PersonaApiClient>(client =>
                {
                    client.BaseAddress = new Uri(apiBaseUrl);
                });
                
                services.AddHttpClient<PrestamoApiClient>(client =>
                {
                    client.BaseAddress = new Uri(apiBaseUrl);
                });

                // Registrar formularios
                services.AddTransient<FormPrincipal>();
                services.AddTransient<FormAutores>();
                services.AddTransient<FormLibros>();
                services.AddTransient<FormGeneros>();
                services.AddTransient<FormEditoriales>();
                services.AddTransient<FormUsuarios>();
                services.AddTransient<FormPrestamos>();
            });
}