using Biblioteca.Data.Entities;
using Biblioteca.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Data
{
    public class AutorRepository : IRepository<Autor>
    {
        private readonly BibliotecaContext _context;

        public AutorRepository(BibliotecaContext context)
        {
            _context = context;
        }

        public void Add(Autor autor)
        {
            if (autor == null)
                throw new ArgumentNullException(nameof(autor));

            var autorEntity = AutorEntity.FromAutor(autor);
            _context.Autores.Add(autorEntity);
        }

        public Autor? Get(int id)
        {
            var autorEntity = _context.Autores.Find(id);
            return autorEntity?.ToAutor();
        }

        public IEnumerable<Autor> GetAll()
        {
            return _context.Autores
                .OrderBy(a => a.Apellido)
                .ThenBy(a => a.Nombre)
                .ToList()
                .Select(a => a.ToAutor());
        }

        public bool Update(Autor autor)
        {
            if (autor == null)
                throw new ArgumentNullException(nameof(autor));

            var existing = _context.Autores.Find(autor.Id);
            if (existing == null)
                return false;

            existing.Nombre = autor.Nombre;
            existing.Apellido = autor.Apellido;
            return true;
        }

        public bool Delete(int id)
        {
            var autor = _context.Autores.Find(id);
            if (autor == null)
                return false;

            // Verificar si el autor tiene libros asociados
            var tieneLibros = _context.Libros.Any(l => l.AutorId == id);
            if (tieneLibros)
                throw new InvalidOperationException("No se puede eliminar el autor porque tiene libros asociados.");

            _context.Autores.Remove(autor);
            return true;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public IEnumerable<Autor> GetByCriteria(string criterio)
        {
            if (string.IsNullOrWhiteSpace(criterio))
                return GetAll();

            return _context.Autores
                .Where(a => a.Nombre.Contains(criterio) || a.Apellido.Contains(criterio))
                .OrderBy(a => a.Apellido)
                .ThenBy(a => a.Nombre)
                .ToList()
                .Select(a => a.ToAutor());
        }
    }
}