using Biblioteca.DTOs;
using System.Text;
using System.Text.Json;

namespace Biblioteca.API.Clients
{
    public class ReporteApiClient : BaseApiClient
    {
        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public ReporteApiClient(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<(IEnumerable<ReportePrestamosDto> prestamos, Dictionary<string, int> estadisticas)> GetPrestamosPorFechasAsync(DateTime fechaInicio, DateTime fechaFin)
        {
            await EnsureAuthenticatedAsync();

            var dto = new ReporteFechasDto
            {
                FechaInicio = fechaInicio,
                FechaFin = fechaFin
            };

            var json = JsonSerializer.Serialize(dto, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await PostAsync("/api/reportes/prestamos-por-fechas", content);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ReporteResponse>(responseContent, _jsonOptions);

            return (result?.Prestamos ?? Enumerable.Empty<ReportePrestamosDto>(), 
                    result?.Estadisticas ?? new Dictionary<string, int>());
        }

        private class ReporteResponse
        {
            public IEnumerable<ReportePrestamosDto> Prestamos { get; set; } = Enumerable.Empty<ReportePrestamosDto>();
            public Dictionary<string, int> Estadisticas { get; set; } = new Dictionary<string, int>();
        }
    }
}