using Biblioteca.Data.Entities;
using Biblioteca.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Data
{
    public class GeneroRepository : IRepository<Genero>
    {
        private readonly BibliotecaContext _context;

        public GeneroRepository(BibliotecaContext context)
        {
            _context = context;
        }

        public void Add(Genero genero)
        {
            if (genero == null)
                throw new ArgumentNullException(nameof(genero));

            // Verificar si ya existe un género con el mismo nombre
            if (NombreExists(genero.Nombre))
                throw new InvalidOperationException($"Ya existe un género con el nombre '{genero.Nombre}'.");

            var generoEntity = GeneroEntity.FromGenero(genero);
            _context.Generos.Add(generoEntity);
        }

        public Genero? Get(int id)
        {
            var generoEntity = _context.Generos.Find(id);
            return generoEntity?.ToGenero();
        }

        public IEnumerable<Genero> GetAll()
        {
            return _context.Generos
                .OrderBy(g => g.Nombre)
                .ToList()
                .Select(g => g.ToGenero());
        }

        public bool Update(Genero genero)
        {
            if (genero == null)
                throw new ArgumentNullException(nameof(genero));

            var existing = _context.Generos.Find(genero.Id);
            if (existing == null)
                return false;

            // Verificar si ya existe otro género con el mismo nombre
            if (NombreExists(genero.Nombre, genero.Id))
                throw new InvalidOperationException($"Ya existe otro género con el nombre '{genero.Nombre}'.");

            existing.Nombre = genero.Nombre;
            return true;
        }

        public bool Delete(int id)
        {
            var genero = _context.Generos.Find(id);
            if (genero == null)
                return false;

            // Verificar si el género tiene libros asociados
            var tieneLibros = _context.Libros.Any(l => l.GeneroId == id);
            if (tieneLibros)
                throw new InvalidOperationException("No se puede eliminar el género porque tiene libros asociados.");

            _context.Generos.Remove(genero);
            return true;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public bool NombreExists(string nombre, int? excludeId = null)
        {
            var query = _context.Generos.Where(g => g.Nombre == nombre);
            
            if (excludeId.HasValue)
                query = query.Where(g => g.Id != excludeId.Value);

            return query.Any();
        }

        public IEnumerable<Genero> GetByCriteria(string criterio)
        {
            if (string.IsNullOrWhiteSpace(criterio))
                return GetAll();

            return _context.Generos
                .Where(g => g.Nombre.Contains(criterio))
                .OrderBy(g => g.Nombre)
                .ToList()
                .Select(g => g.ToGenero());
        }
    }
}