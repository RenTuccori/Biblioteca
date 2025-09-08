using Biblioteca.API.Clients;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Configurar HttpClient para API
builder.Services.AddHttpClient<AutorApiClient>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7142/"); // Ajustar según tu puerto de WebAPI
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddHttpClient<GeneroApiClient>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7142/"); // Ajustar según tu puerto de WebAPI
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddHttpClient<LibroApiClient>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7142/"); // Ajustar según tu puerto de WebAPI
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();