namespace Biblioteca.Domain.Model
{
    public class Libro
    {
        private int _id;
        private string _titulo = string.Empty;
        private string _isbn = string.Empty;
        private int _autorId;
        private int _generoId;
        private Autor? _autor;
        private Genero? _genero;

        public int Id 
        { 
            get => _id; 
            private set => _id = value; 
        }
        
        public string Titulo 
        { 
            get => _titulo; 
            private set => _titulo = value; 
        }
        
        public string ISBN 
        { 
            get => _isbn; 
            private set => _isbn = value; 
        }

        public int AutorId 
        { 
            get => _autorId; 
            private set => _autorId = value; 
        }

        public int GeneroId 
        { 
            get => _generoId; 
            private set => _generoId = value; 
        }

        // Propiedades de navegación
        public Autor? Autor 
        { 
            get => _autor; 
            private set => _autor = value; 
        }
        
        public Genero? Genero 
        { 
            get => _genero; 
            private set => _genero = value; 
        }

        // Constructor sin parámetros para Entity Framework
        protected Libro() { }

        public Libro(int id, string titulo, string isbn, Autor autor, Genero genero)
        {
            SetId(id);
            SetTitulo(titulo);
            SetISBN(isbn);
            SetAutor(autor);
            SetGenero(genero);
        }

        public Libro(int id, string titulo, string isbn, int autorId, int generoId)
        {
            SetId(id);
            SetTitulo(titulo);
            SetISBN(isbn);
            SetAutorId(autorId);
            SetGeneroId(generoId);
        }

        public void SetId(int id) 
        { 
            if (id < 0)
                throw new ArgumentException("El Id no puede ser negativo.", nameof(id));
            _id = id; 
        }
        
        public void SetTitulo(string titulo) 
        {
            if (string.IsNullOrWhiteSpace(titulo))
                throw new ArgumentException("El título no puede ser nulo o vacío.", nameof(titulo));
            _titulo = titulo; 
        }
        
        public void SetISBN(string isbn) 
        {
            if (string.IsNullOrWhiteSpace(isbn))
                throw new ArgumentException("El ISBN no puede ser nulo o vacío.", nameof(isbn));
            _isbn = isbn; 
        }

        public void SetAutorId(int autorId)
        {
            if (autorId <= 0)
                throw new ArgumentException("El AutorId debe ser mayor que 0.", nameof(autorId));
            _autorId = autorId;
        }

        public void SetGeneroId(int generoId)
        {
            if (generoId <= 0)
                throw new ArgumentException("El GeneroId debe ser mayor que 0.", nameof(generoId));
            _generoId = generoId;
        }

        public void SetAutor(Autor autor)
        {
            if (autor == null)
                throw new ArgumentNullException(nameof(autor), "El autor no puede ser nulo.");
            _autor = autor;
            _autorId = autor.Id;
        }

        public void SetGenero(Genero genero)
        {
            if (genero == null)
                throw new ArgumentNullException(nameof(genero), "El género no puede ser nulo.");
            _genero = genero;
            _generoId = genero.Id;
        }
    }
}