namespace Biblioteca.DTOs
{
    public class ReportePrestamosDto
    {
        public int PrestamoId { get; set; }
        public string LibroTitulo { get; set; } = string.Empty;
        public string AutorNombre { get; set; } = string.Empty;
        public string SocioNombre { get; set; } = string.Empty;
        public DateTime FechaPrestamo { get; set; }
        public DateTime FechaDevolucionPrevista { get; set; }
        public DateTime? FechaDevolucionReal { get; set; }
        public string Estado { get; set; } = string.Empty; // "Activo", "Devuelto", "Vencido"
    }
}