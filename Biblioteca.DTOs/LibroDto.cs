namespace Biblioteca.DTOs
{
    // DTO para MOSTRAR un libro en una lista
    public class LibroDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string ISBN { get; set; }
        public string AutorNombreCompleto { get; set; }
        public string GeneroNombre { get; set; }
    }
}