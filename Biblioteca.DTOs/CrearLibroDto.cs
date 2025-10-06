namespace Biblioteca.DTOs
{
    // DTO para CREAR o MODIFICAR un libro
    public class CrearLibroDto
    {
        public string Titulo { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public int AutorId { get; set; }
        public int GeneroId { get; set; }
        public int EditorialId { get; set; }
        public string Estado { get; set; } = "disponible";
    }
}