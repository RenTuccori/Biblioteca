namespace Biblioteca.Data.Entities
{
    public class LibroEntity
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public int AutorId { get; set; }
        public int GeneroId { get; set; }

        // Propiedades de navegación
        public AutorEntity? Autor { get; set; }
        public GeneroEntity? Genero { get; set; }

        // Método para convertir a entidad de dominio
        public Biblioteca.Domain.Model.Libro ToLibro()
        {
            var libro = new Biblioteca.Domain.Model.Libro(Id, Titulo, ISBN, AutorId, GeneroId);
            
            if (Autor != null)
                libro.SetAutor(Autor.ToAutor());
            
            if (Genero != null)
                libro.SetGenero(Genero.ToGenero());
            
            return libro;
        }

        // Método estático para crear desde entidad de dominio
        public static LibroEntity FromLibro(Biblioteca.Domain.Model.Libro libro)
        {
            return new LibroEntity
            {
                Id = libro.Id,
                Titulo = libro.Titulo,
                ISBN = libro.ISBN,
                AutorId = libro.AutorId,
                GeneroId = libro.GeneroId
            };
        }
    }
}