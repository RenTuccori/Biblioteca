using Biblioteca.Data;
using Biblioteca.DTOs;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Biblioteca.Domain.Services
{
    public class ReporteService
    {
        private readonly string _connectionString;

        public ReporteService()
        {
            _connectionString = DatabaseConfiguration.GetConnectionString();
        }

        public IEnumerable<ReportePrestamosDto> GetPrestamosPorFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            var prestamos = new List<ReportePrestamosDto>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = @"
                    SELECT 
                        p.Id as PrestamoId,
                        l.Titulo as LibroTitulo,
                        CONCAT(a.Nombre, ' ', a.Apellido) as AutorNombre,
                        CONCAT(per.Nombre, ' ', per.Apellido) as SocioNombre,
                        p.FechaPrestamo,
                        p.FechaDevolucionPrevista,
                        p.FechaDevolucionReal,
                        CASE 
                            WHEN p.FechaDevolucionReal IS NOT NULL THEN 'Devuelto'
                            WHEN p.FechaDevolucionPrevista < GETDATE() AND p.FechaDevolucionReal IS NULL THEN 'Vencido'
                            ELSE 'Activo'
                        END as Estado
                    FROM Prestamos p
                    INNER JOIN Libros l ON p.LibroId = l.Id
                    INNER JOIN Autores a ON l.AutorId = a.Id
                    INNER JOIN Usuarios u ON p.SocioId = u.Id
                    INNER JOIN Personas per ON u.PersonaId = per.Id
                    WHERE p.FechaPrestamo >= @FechaInicio 
                        AND p.FechaPrestamo <= @FechaFin
                    ORDER BY p.FechaPrestamo DESC";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add(new SqlParameter("@FechaInicio", SqlDbType.DateTime) { Value = fechaInicio });
                    command.Parameters.Add(new SqlParameter("@FechaFin", SqlDbType.DateTime) { Value = fechaFin.Date.AddDays(1).AddSeconds(-1) }); // Incluir todo el día

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            prestamos.Add(new ReportePrestamosDto
                            {
                                PrestamoId = reader.GetInt32("PrestamoId"),
                                LibroTitulo = reader.GetString("LibroTitulo"),
                                AutorNombre = reader.GetString("AutorNombre"),
                                SocioNombre = reader.GetString("SocioNombre"),
                                FechaPrestamo = reader.GetDateTime("FechaPrestamo"),
                                FechaDevolucionPrevista = reader.GetDateTime("FechaDevolucionPrevista"),
                                FechaDevolucionReal = reader.IsDBNull("FechaDevolucionReal") ? null : reader.GetDateTime("FechaDevolucionReal"),
                                Estado = reader.GetString("Estado")
                            });
                        }
                    }
                }
            }

            return prestamos;
        }

        public Dictionary<string, int> GetEstadisticasPorFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            var estadisticas = new Dictionary<string, int>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = @"
                    SELECT 
                        COUNT(*) as TotalPrestamos,
                        SUM(CASE WHEN FechaDevolucionReal IS NOT NULL THEN 1 ELSE 0 END) as Devueltos,
                        SUM(CASE WHEN FechaDevolucionReal IS NULL AND FechaDevolucionPrevista < GETDATE() THEN 1 ELSE 0 END) as Vencidos,
                        SUM(CASE WHEN FechaDevolucionReal IS NULL AND FechaDevolucionPrevista >= GETDATE() THEN 1 ELSE 0 END) as Activos
                    FROM Prestamos
                    WHERE FechaPrestamo >= @FechaInicio 
                        AND FechaPrestamo <= @FechaFin";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add(new SqlParameter("@FechaInicio", SqlDbType.DateTime) { Value = fechaInicio });
                    command.Parameters.Add(new SqlParameter("@FechaFin", SqlDbType.DateTime) { Value = fechaFin.Date.AddDays(1).AddSeconds(-1) });

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            estadisticas["TotalPrestamos"] = reader.GetInt32("TotalPrestamos");
                            estadisticas["Devueltos"] = reader.GetInt32("Devueltos");
                            estadisticas["Vencidos"] = reader.GetInt32("Vencidos");
                            estadisticas["Activos"] = reader.GetInt32("Activos");
                        }
                    }
                }
            }

            return estadisticas;
        }
    }
}