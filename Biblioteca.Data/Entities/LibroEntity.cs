namespace Biblioteca.Data.Entities
{
    public class LibroEntity
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public int AutorId { get; set; }
        public int GeneroId { get; set; }
        public int EditorialId { get; set; }
        public string Estado { get; set; } = "disponible";

        // Propiedades de navegación
        public AutorEntity? Autor { get; set; }
        public GeneroEntity? Genero { get; set; }
        public EditorialEntity? Editorial { get; set; }

        // Método para convertir a entidad de dominio
        public Biblioteca.Domain.Model.Libro ToLibro()
        {
            var libro = new Biblioteca.Domain.Model.Libro(Id, Titulo, ISBN, AutorId, GeneroId, EditorialId, Estado);
            
            if (Autor != null)
                libro.SetAutor(Autor.ToAutor());
            
            if (Genero != null)
                libro.SetGenero(Genero.ToGenero());
            
            if (Editorial != null)
                libro.SetEditorial(Editorial.ToEditorial());
            
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
                GeneroId = libro.GeneroId,
                EditorialId = libro.EditorialId,
                Estado = libro.Estado
            };
        }
    }
}