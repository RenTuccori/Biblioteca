namespace Biblioteca.DTOs
{
    public class PrestamoDto
    {
        public int Id { get; set; }
        public int LibroId { get; set; }
        public int SocioId { get; set; }
        public DateTime FechaPrestamo { get; set; }
        public DateTime FechaDevolucionPrevista { get; set; }
        public DateTime? FechaDevolucionReal { get; set; }
        public string LibroTitulo { get; set; } = string.Empty;
        public string SocioNombreCompleto { get; set; } = string.Empty;
    }
}
