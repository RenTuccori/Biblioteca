namespace Biblioteca.Domain.Model
{
    public class Libro
    {
        public int Id { get; private set; }
        public string Titulo { get; private set; }
        public string ISBN { get; private set; }

        // Propiedades de relación
        public Autor Autor { get; private set; }
        public Genero Genero { get; private set; }

        public Libro(int id, string titulo, string isbn, Autor autor, Genero genero)
        {
            SetId(id);
            SetTitulo(titulo);
            SetISBN(isbn);
            SetAutor(autor);
            SetGenero(genero);
        }

        public void SetId(int id) { 
            if (id < 0)
                throw new ArgumentException("El Id no puede ser negativo.", nameof(id));
            Id = id; }
        public void SetTitulo(string titulo) {
            if (string.IsNullOrWhiteSpace(titulo))
                throw new ArgumentException("El título no puede ser nulo o vacío.", nameof(titulo));
            Titulo = titulo; }
        public void SetISBN(string isbn) {
            if (string.IsNullOrWhiteSpace(isbn))
                throw new ArgumentException("El ISBN no puede ser nulo o vacío.", nameof(isbn));
            ISBN = isbn; }

        public void SetAutor(Autor autor)
        {
            if (autor == null)
                throw new ArgumentNullException(nameof(autor), "El autor no puede ser nulo.");
            Autor = autor;
        }

        public void SetGenero(Genero genero)
        {
            if (genero == null)
                throw new ArgumentNullException(nameof(genero), "El género no puede ser nulo.");
            Genero = genero;
        }
    }
}