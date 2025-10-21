using Biblioteca.API.Clients;
using Biblioteca.Blazor.WebAssembly;
using Biblioteca.Blazor.WebAssembly.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configurar la URL base de la API
var apiBaseUrl = builder.Configuration["ApiBaseUrl"] ?? "https://localhost:7152";

// Registrar HttpClient base (Scoped en WASM)
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiBaseUrl) });

// Registrar los API Clients reutilizando el HttpClient de DI
builder.Services.AddScoped<AutorApiClient>(sp => new AutorApiClient(sp.GetRequiredService<HttpClient>()));
builder.Services.AddScoped<GeneroApiClient>(sp => new GeneroApiClient(sp.GetRequiredService<HttpClient>()));
builder.Services.AddScoped<LibroApiClient>(sp => new LibroApiClient(sp.GetRequiredService<HttpClient>()));
builder.Services.AddScoped<EditorialApiClient>(sp => new EditorialApiClient(sp.GetRequiredService<HttpClient>()));
builder.Services.AddScoped<PersonaApiClient>(sp => new PersonaApiClient(sp.GetRequiredService<HttpClient>()));
builder.Services.AddScoped<UsuarioApiClient>(sp => new UsuarioApiClient(sp.GetRequiredService<HttpClient>()));
builder.Services.AddScoped<PrestamoApiClient>(sp => new PrestamoApiClient(sp.GetRequiredService<HttpClient>()));
builder.Services.AddScoped<ReporteApiClient>(sp => new ReporteApiClient(sp.GetRequiredService<HttpClient>()));

builder.Services.AddScoped<AuthApiClient>(sp => new AuthApiClient(sp.GetRequiredService<HttpClient>()));

// IAuthService debe ser Scoped (no Singleton) ya que depende de servicios Scoped
builder.Services.AddScoped<IAuthService, WasmAuthService>();

var host = builder.Build();

// Registrar el servicio de auth en el proveedor estático antes de que se use cualquier ApiClient
var authSvc = host.Services.GetRequiredService<IAuthService>();
AuthServiceProvider.Register(authSvc);

await host.RunAsync();
