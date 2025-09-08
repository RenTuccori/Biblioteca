namespace Biblioteca.DTOs
{
    // DTO para MOSTRAR un libro en una lista
    public class LibroDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public string AutorNombreCompleto { get; set; } = string.Empty;
        public string GeneroNombre { get; set; } = string.Empty;
        public int AutorId { get; set; }
        public int GeneroId { get; set; }
    }
}