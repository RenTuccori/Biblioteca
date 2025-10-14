using Biblioteca.API.Clients;
using Biblioteca.Blazor.WebAssembly;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configurar la URL base de la API
var apiBaseUrl = builder.Configuration["ApiBaseUrl"] ?? "https://localhost:7152";

// Registrar HttpClient base
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiBaseUrl) });

// Registrar los API Clients con HttpClient
builder.Services.AddScoped<AutorApiClient>(sp =>
{
    var httpClient = new HttpClient { BaseAddress = new Uri(apiBaseUrl) };
    return new AutorApiClient(httpClient);
});

builder.Services.AddScoped<GeneroApiClient>(sp =>
{
    var httpClient = new HttpClient { BaseAddress = new Uri(apiBaseUrl) };
    return new GeneroApiClient(httpClient);
});

builder.Services.AddScoped<LibroApiClient>(sp =>
{
    var httpClient = new HttpClient { BaseAddress = new Uri(apiBaseUrl) };
    return new LibroApiClient(httpClient);
});

builder.Services.AddScoped<EditorialApiClient>(sp =>
{
    var httpClient = new HttpClient { BaseAddress = new Uri(apiBaseUrl) };
    return new EditorialApiClient(httpClient);
});

builder.Services.AddScoped<PersonaApiClient>(sp =>
{
    var httpClient = new HttpClient { BaseAddress = new Uri(apiBaseUrl) };
    return new PersonaApiClient(httpClient);
});

builder.Services.AddScoped<UsuarioApiClient>(sp =>
{
    var httpClient = new HttpClient { BaseAddress = new Uri(apiBaseUrl) };
    return new UsuarioApiClient(httpClient);
});

builder.Services.AddScoped<PrestamoApiClient>(sp =>
{
    var httpClient = new HttpClient { BaseAddress = new Uri(apiBaseUrl) };
    return new PrestamoApiClient(httpClient);
});

await builder.Build().RunAsync();
