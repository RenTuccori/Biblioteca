using Biblioteca.Data.Entities;
using Biblioteca.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Data
{
    public class LibroRepository : IRepository<Libro>
    {
        private readonly BibliotecaContext _context;

        public LibroRepository(BibliotecaContext context)
        {
            _context = context;
        }

        public void Add(Libro libro)
        {
            if (libro == null)
                throw new ArgumentNullException(nameof(libro));

            // Verificar si ya existe un libro con el mismo ISBN
            if (ISBNExists(libro.ISBN))
                throw new InvalidOperationException($"Ya existe un libro con el ISBN '{libro.ISBN}'.");

            var libroEntity = LibroEntity.FromLibro(libro);
            _context.Libros.Add(libroEntity);
        }

        public Libro? Get(int id)
        {
            var libroEntity = _context.Libros
                .Include(l => l.Autor)
                .Include(l => l.Genero)
                .FirstOrDefault(l => l.Id == id);

            return libroEntity?.ToLibro();
        }

        public IEnumerable<Libro> GetAll()
        {
            return _context.Libros
                .Include(l => l.Autor)
                .Include(l => l.Genero)
                .OrderBy(l => l.Titulo)
                .ToList()
                .Select(l => l.ToLibro());
        }

        public bool Update(Libro libro)
        {
            if (libro == null)
                throw new ArgumentNullException(nameof(libro));

            var existing = _context.Libros.Find(libro.Id);
            if (existing == null)
                return false;

            // Verificar si ya existe otro libro con el mismo ISBN
            if (ISBNExists(libro.ISBN, libro.Id))
                throw new InvalidOperationException($"Ya existe otro libro con el ISBN '{libro.ISBN}'.");

            // Actualizar propiedades
            existing.Titulo = libro.Titulo;
            existing.ISBN = libro.ISBN;
            existing.AutorId = libro.AutorId;
            existing.GeneroId = libro.GeneroId;

            return true;
        }

        public bool Delete(int id)
        {
            var libro = _context.Libros.Find(id);
            if (libro == null)
                return false;

            _context.Libros.Remove(libro);
            return true;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public bool ISBNExists(string isbn, int? excludeId = null)
        {
            var query = _context.Libros.Where(l => l.ISBN == isbn);
            
            if (excludeId.HasValue)
                query = query.Where(l => l.Id != excludeId.Value);

            return query.Any();
        }

        public IEnumerable<Libro> GetByCriteria(string criterio)
        {
            if (string.IsNullOrWhiteSpace(criterio))
                return GetAll();

            // Usar LINQ en lugar de SQL raw para mejor compatibilidad
            return _context.Libros
                .Include(l => l.Autor)
                .Include(l => l.Genero)
                .Where(l => l.Titulo.Contains(criterio) ||
                           l.ISBN.Contains(criterio) ||
                           l.Autor!.Nombre.Contains(criterio) ||
                           l.Autor!.Apellido.Contains(criterio) ||
                           l.Genero!.Nombre.Contains(criterio))
                .OrderBy(l => l.Titulo)
                .ToList()
                .Select(l => l.ToLibro());
        }

        public IEnumerable<Libro> GetByAutor(int autorId)
        {
            return _context.Libros
                .Include(l => l.Autor)
                .Include(l => l.Genero)
                .Where(l => l.AutorId == autorId)
                .OrderBy(l => l.Titulo)
                .ToList()
                .Select(l => l.ToLibro());
        }

        public IEnumerable<Libro> GetByGenero(int generoId)
        {
            return _context.Libros
                .Include(l => l.Autor)
                .Include(l => l.Genero)
                .Where(l => l.GeneroId == generoId)
                .OrderBy(l => l.Titulo)
                .ToList()
                .Select(l => l.ToLibro());
        }
    }
}