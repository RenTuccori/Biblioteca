using Biblioteca.Data;
using Biblioteca.Domain.Model;
using Biblioteca.DTOs;

namespace Biblioteca.Domain.Services
{
    public class PrestamoService
    {
        private readonly PrestamoRepository _prestamoRepository;
        private readonly LibroRepository _libroRepository;
        private readonly UsuarioRepository _usuarioRepository;

        public PrestamoService(PrestamoRepository prestamoRepository, LibroRepository libroRepository, UsuarioRepository usuarioRepository)
        {
            _prestamoRepository = prestamoRepository;
            _libroRepository = libroRepository;
            _usuarioRepository = usuarioRepository;
        }

        public PrestamoDto Add(CrearPrestamoDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            // Verificar que el libro y socio existan
            var libro = _libroRepository.Get(dto.LibroId);
            if (libro == null)
                throw new ArgumentException($"No existe un libro con Id {dto.LibroId}");

            var socio = _usuarioRepository.Get(dto.SocioId);
            if (socio == null)
                throw new ArgumentException($"No existe un usuario con Id {dto.SocioId}");

            if (socio.Rol != "socio")
                throw new ArgumentException("El usuario debe tener rol de socio para realizar préstamos.");

            var prestamo = new Prestamo(0, dto.LibroId, dto.SocioId, dto.FechaPrestamo, dto.FechaDevolucionPrevista);
            
            _prestamoRepository.Add(prestamo);
            _prestamoRepository.SaveChanges();

            return new PrestamoDto
            {
                Id = prestamo.Id,
                LibroId = prestamo.LibroId,
                SocioId = prestamo.SocioId,
                FechaPrestamo = prestamo.FechaPrestamo,
                FechaDevolucionPrevista = prestamo.FechaDevolucionPrevista,
                FechaDevolucionReal = prestamo.FechaDevolucionReal,
                LibroTitulo = libro.Titulo,
                SocioNombreCompleto = socio.Persona != null ? $"{socio.Persona.Nombre} {socio.Persona.Apellido}" : ""
            };
        }

        public bool Delete(int id)
        {
            try
            {
                var result = _prestamoRepository.Delete(id);
                if (result)
                    _prestamoRepository.SaveChanges();
                return result;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
        }

        public PrestamoDto? Get(int id)
        {
            var prestamo = _prestamoRepository.Get(id);
            if (prestamo == null)
                return null;

            return new PrestamoDto
            {
                Id = prestamo.Id,
                LibroId = prestamo.LibroId,
                SocioId = prestamo.SocioId,
                FechaPrestamo = prestamo.FechaPrestamo,
                FechaDevolucionPrevista = prestamo.FechaDevolucionPrevista,
                FechaDevolucionReal = prestamo.FechaDevolucionReal,
                LibroTitulo = prestamo.Libro?.Titulo ?? "",
                SocioNombreCompleto = prestamo.Socio?.Persona != null ? $"{prestamo.Socio.Persona.Nombre} {prestamo.Socio.Persona.Apellido}" : ""
            };
        }

        public IEnumerable<PrestamoDto> GetAll()
        {
            var prestamos = _prestamoRepository.GetAll();
            return prestamos.Select(p => new PrestamoDto
            {
                Id = p.Id,
                LibroId = p.LibroId,
                SocioId = p.SocioId,
                FechaPrestamo = p.FechaPrestamo,
                FechaDevolucionPrevista = p.FechaDevolucionPrevista,
                FechaDevolucionReal = p.FechaDevolucionReal,
                LibroTitulo = p.Libro?.Titulo ?? "",
                SocioNombreCompleto = p.Socio?.Persona != null ? $"{p.Socio.Persona.Nombre} {p.Socio.Persona.Apellido}" : ""
            });
        }

        public bool Update(PrestamoDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            // Validar existencia de libro y socio
            var libro = _libroRepository.Get(dto.LibroId) ?? throw new ArgumentException($"No existe un libro con Id {dto.LibroId}");
            var socio = _usuarioRepository.Get(dto.SocioId) ?? throw new ArgumentException($"No existe un usuario con Id {dto.SocioId}");

            var prestamo = new Prestamo(dto.Id, dto.LibroId, dto.SocioId, dto.FechaPrestamo, dto.FechaDevolucionPrevista, dto.FechaDevolucionReal);
            
            var result = _prestamoRepository.Update(prestamo);
            if (result)
                _prestamoRepository.SaveChanges();
            
            return result;
        }

        public bool DevolverLibro(int prestamoId, DateTime fechaDevolucion)
        {
            var prestamo = _prestamoRepository.Get(prestamoId);
            if (prestamo == null)
                return false;

            if (prestamo.FechaDevolucionReal.HasValue)
                throw new InvalidOperationException("Este préstamo ya ha sido devuelto.");

            prestamo.SetFechaDevolucionReal(fechaDevolucion);
            
            var result = _prestamoRepository.Update(prestamo);
            if (result)
                _prestamoRepository.SaveChanges();
            
            return result;
        }

        public IEnumerable<PrestamoDto> GetPrestamosActivos()
        {
            var prestamos = _prestamoRepository.GetPrestamosActivos();
            return prestamos.Select(p => new PrestamoDto
            {
                Id = p.Id,
                LibroId = p.LibroId,
                SocioId = p.SocioId,
                FechaPrestamo = p.FechaPrestamo,
                FechaDevolucionPrevista = p.FechaDevolucionPrevista,
                FechaDevolucionReal = p.FechaDevolucionReal,
                LibroTitulo = p.Libro?.Titulo ?? "",
                SocioNombreCompleto = p.Socio?.Persona != null ? $"{p.Socio.Persona.Nombre} {p.Socio.Persona.Apellido}" : ""
            });
        }

        public IEnumerable<PrestamoDto> GetPrestamosBySocio(int socioId)
        {
            var prestamos = _prestamoRepository.GetPrestamosBySocio(socioId);
            return prestamos.Select(p => new PrestamoDto
            {
                Id = p.Id,
                LibroId = p.LibroId,
                SocioId = p.SocioId,
                FechaPrestamo = p.FechaPrestamo,
                FechaDevolucionPrevista = p.FechaDevolucionPrevista,
                FechaDevolucionReal = p.FechaDevolucionReal,
                LibroTitulo = p.Libro?.Titulo ?? "",
                SocioNombreCompleto = p.Socio?.Persona != null ? $"{p.Socio.Persona.Nombre} {p.Socio.Persona.Apellido}" : ""
            });
        }

        public IEnumerable<PrestamoDto> GetPrestamosVencidos()
        {
            var prestamos = _prestamoRepository.GetPrestamosVencidos();
            return prestamos.Select(p => new PrestamoDto
            {
                Id = p.Id,
                LibroId = p.LibroId,
                SocioId = p.SocioId,
                FechaPrestamo = p.FechaPrestamo,
                FechaDevolucionPrevista = p.FechaDevolucionPrevista,
                FechaDevolucionReal = p.FechaDevolucionReal,
                LibroTitulo = p.Libro?.Titulo ?? "",
                SocioNombreCompleto = p.Socio?.Persona != null ? $"{p.Socio.Persona.Nombre} {p.Socio.Persona.Apellido}" : ""
            });
        }
    }
}
