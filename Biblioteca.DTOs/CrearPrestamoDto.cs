namespace Biblioteca.DTOs
{
    public class CrearPrestamoDto
    {
        public int LibroId { get; set; }
        public int SocioId { get; set; }
        public DateTime FechaPrestamo { get; set; }
        public DateTime FechaDevolucionPrevista { get; set; }
    }
}
