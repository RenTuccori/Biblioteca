using Biblioteca.Data.Entities;
using Biblioteca.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Data
{
    public class PrestamoRepository : IRepository<Prestamo>
    {
        private readonly BibliotecaContext _context;

        public PrestamoRepository(BibliotecaContext context)
        {
            _context = context;
        }

        public void Add(Prestamo prestamo)
        {
            if (prestamo == null)
                throw new ArgumentNullException(nameof(prestamo));

            // Verificar si el libro está disponible
            var libro = _context.Libros.Find(prestamo.LibroId);
            if (libro == null)
                throw new InvalidOperationException("El libro no existe.");
            
            if (libro.Estado == "prestado")
                throw new InvalidOperationException("El libro ya está prestado.");

            var prestamoEntity = PrestamoEntity.FromPrestamo(prestamo);
            _context.Prestamos.Add(prestamoEntity);
            
            // Cambiar el estado del libro a prestado
            libro.Estado = "prestado";
        }

        public Prestamo? Get(int id)
        {
            var prestamoEntity = _context.Prestamos
                .Include(p => p.Libro)
                    .ThenInclude(l => l!.Autor)
                .Include(p => p.Libro)
                    .ThenInclude(l => l!.Genero)
                .Include(p => p.Libro)
                    .ThenInclude(l => l!.Editorial)
                .Include(p => p.Socio)
                    .ThenInclude(s => s!.Persona)
                .FirstOrDefault(p => p.Id == id);

            return prestamoEntity?.ToPrestamo();
        }

        public IEnumerable<Prestamo> GetAll()
        {
            return _context.Prestamos
                .Include(p => p.Libro)
                    .ThenInclude(l => l!.Autor)
                .Include(p => p.Libro)
                    .ThenInclude(l => l!.Genero)
                .Include(p => p.Libro)
                    .ThenInclude(l => l!.Editorial)
                .Include(p => p.Socio)
                    .ThenInclude(s => s!.Persona)
                .OrderByDescending(p => p.FechaPrestamo)
                .ToList()
                .Select(p => p.ToPrestamo());
        }

        public bool Update(Prestamo prestamo)
        {
            if (prestamo == null)
                throw new ArgumentNullException(nameof(prestamo));

            var existing = _context.Prestamos.Find(prestamo.Id);
            if (existing == null)
                return false;

            // Si se está devolviendo el libro (se establece FechaDevolucionReal)
            if (prestamo.FechaDevolucionReal.HasValue && !existing.FechaDevolucionReal.HasValue)
            {
                var libro = _context.Libros.Find(prestamo.LibroId);
                if (libro != null)
                    libro.Estado = "disponible";
            }

            existing.LibroId = prestamo.LibroId;
            existing.SocioId = prestamo.SocioId;
            existing.FechaPrestamo = prestamo.FechaPrestamo;
            existing.FechaDevolucionPrevista = prestamo.FechaDevolucionPrevista;
            existing.FechaDevolucionReal = prestamo.FechaDevolucionReal;
            return true;
        }

        public bool Delete(int id)
        {
            var prestamo = _context.Prestamos.Find(id);
            if (prestamo == null)
                return false;

            // Si el préstamo no ha sido devuelto, cambiar el estado del libro a disponible
            if (!prestamo.FechaDevolucionReal.HasValue)
            {
                var libro = _context.Libros.Find(prestamo.LibroId);
                if (libro != null)
                    libro.Estado = "disponible";
            }

            _context.Prestamos.Remove(prestamo);
            return true;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public IEnumerable<Prestamo> GetPrestamosActivos()
        {
            return _context.Prestamos
                .Include(p => p.Libro)
                    .ThenInclude(l => l!.Autor)
                .Include(p => p.Libro)
                    .ThenInclude(l => l!.Genero)
                .Include(p => p.Libro)
                    .ThenInclude(l => l!.Editorial)
                .Include(p => p.Socio)
                    .ThenInclude(s => s!.Persona)
                .Where(p => p.FechaDevolucionReal == null)
                .OrderByDescending(p => p.FechaPrestamo)
                .ToList()
                .Select(p => p.ToPrestamo());
        }

        public IEnumerable<Prestamo> GetPrestamosBySocio(int socioId)
        {
            return _context.Prestamos
                .Include(p => p.Libro)
                    .ThenInclude(l => l!.Autor)
                .Include(p => p.Libro)
                    .ThenInclude(l => l!.Genero)
                .Include(p => p.Libro)
                    .ThenInclude(l => l!.Editorial)
                .Include(p => p.Socio)
                    .ThenInclude(s => s!.Persona)
                .Where(p => p.SocioId == socioId)
                .OrderByDescending(p => p.FechaPrestamo)
                .ToList()
                .Select(p => p.ToPrestamo());
        }

        public IEnumerable<Prestamo> GetPrestamosVencidos()
        {
            var hoy = DateTime.Now.Date;
            return _context.Prestamos
                .Include(p => p.Libro)
                    .ThenInclude(l => l!.Autor)
                .Include(p => p.Libro)
                    .ThenInclude(l => l!.Genero)
                .Include(p => p.Libro)
                    .ThenInclude(l => l!.Editorial)
                .Include(p => p.Socio)
                    .ThenInclude(s => s!.Persona)
                .Where(p => p.FechaDevolucionReal == null && p.FechaDevolucionPrevista < hoy)
                .OrderBy(p => p.FechaDevolucionPrevista)
                .ToList()
                .Select(p => p.ToPrestamo());
        }
    }
}
