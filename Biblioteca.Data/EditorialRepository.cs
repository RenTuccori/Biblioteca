using Biblioteca.Data.Entities;
using Biblioteca.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Data
{
    public class EditorialRepository : IRepository<Editorial>
    {
        private readonly BibliotecaContext _context;

        public EditorialRepository(BibliotecaContext context)
        {
            _context = context;
        }

        public void Add(Editorial editorial)
        {
            if (editorial == null)
                throw new ArgumentNullException(nameof(editorial));

            // Verificar si ya existe una editorial con el mismo nombre
            if (NombreExists(editorial.Nombre))
                throw new InvalidOperationException($"Ya existe una editorial con el nombre '{editorial.Nombre}'.");

            var editorialEntity = EditorialEntity.FromEditorial(editorial);
            _context.Editoriales.Add(editorialEntity);
        }

        public Editorial? Get(int id)
        {
            var editorialEntity = _context.Editoriales.Find(id);
            return editorialEntity?.ToEditorial();
        }

        public IEnumerable<Editorial> GetAll()
        {
            return _context.Editoriales
                .OrderBy(e => e.Nombre)
                .ToList()
                .Select(e => e.ToEditorial());
        }

        public bool Update(Editorial editorial)
        {
            if (editorial == null)
                throw new ArgumentNullException(nameof(editorial));

            var existing = _context.Editoriales.Find(editorial.Id);
            if (existing == null)
                return false;

            // Verificar si ya existe otra editorial con el mismo nombre
            if (NombreExists(editorial.Nombre, editorial.Id))
                throw new InvalidOperationException($"Ya existe otra editorial con el nombre '{editorial.Nombre}'.");

            existing.Nombre = editorial.Nombre;
            return true;
        }

        public bool Delete(int id)
        {
            var editorial = _context.Editoriales.Find(id);
            if (editorial == null)
                return false;

            // Verificar si la editorial tiene libros asociados
            var tieneLibros = _context.Libros.Any(l => l.EditorialId == id);
            if (tieneLibros)
                throw new InvalidOperationException("No se puede eliminar la editorial porque tiene libros asociados.");

            _context.Editoriales.Remove(editorial);
            return true;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public bool NombreExists(string nombre, int? excludeId = null)
        {
            var query = _context.Editoriales.Where(e => e.Nombre == nombre);
            
            if (excludeId.HasValue)
                query = query.Where(e => e.Id != excludeId.Value);

            return query.Any();
        }

        public IEnumerable<Editorial> GetByCriteria(string criterio)
        {
            if (string.IsNullOrWhiteSpace(criterio))
                return GetAll();

            return _context.Editoriales
                .Where(e => e.Nombre.Contains(criterio))
                .OrderBy(e => e.Nombre)
                .ToList()
                .Select(e => e.ToEditorial());
        }
    }
}
