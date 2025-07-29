namespace Biblioteca.DTOs
{
    // DTO para CREAR o MODIFICAR un libro
    public class CrearLibroDto
    {
        public string Titulo { get; set; }
        public string ISBN { get; set; }
        public int AutorId { get; set; }
        public int GeneroId { get; set; }
    }
}