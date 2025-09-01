using Biblioteca.Data;
using Biblioteca.Domain.Model;
using Biblioteca.DTOs;

namespace Biblioteca.Domain.Services
{
    public class LibroService
    {
        private readonly LibroRepository _libroRepository;
        private readonly AutorRepository _autorRepository;
        private readonly GeneroRepository _generoRepository;

        public LibroService(LibroRepository libroRepository, AutorRepository autorRepository, GeneroRepository generoRepository)
        {
            _libroRepository = libroRepository;
            _autorRepository = autorRepository;
            _generoRepository = generoRepository;
        }

        public LibroDto Add(CrearLibroDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            // Verificar que el autor y género existan
            var autor = _autorRepository.Get(dto.AutorId);
            if (autor == null)
                throw new ArgumentException($"No existe un autor con Id {dto.AutorId}");

            var genero = _generoRepository.Get(dto.GeneroId);
            if (genero == null)
                throw new ArgumentException($"No existe un género con Id {dto.GeneroId}");

            var libro = new Libro(0, dto.Titulo, dto.ISBN, dto.AutorId, dto.GeneroId);
            
            _libroRepository.Add(libro);
            _libroRepository.SaveChanges();

            return new LibroDto
            {
                Id = libro.Id,
                Titulo = libro.Titulo,
                ISBN = libro.ISBN,
                AutorId = libro.AutorId,
                GeneroId = libro.GeneroId,
                AutorNombreCompleto = $"{autor.Nombre} {autor.Apellido}",
                GeneroNombre = genero.Nombre
            };
        }

        public bool Delete(int id)
        {
            var result = _libroRepository.Delete(id);
            if (result)
                _libroRepository.SaveChanges();
            return result;
        }

        public LibroDto? Get(int id)
        {
            var libro = _libroRepository.Get(id);
            if (libro == null)
                return null;

            return new LibroDto
            {
                Id = libro.Id,
                Titulo = libro.Titulo,
                ISBN = libro.ISBN,
                AutorId = libro.AutorId,
                GeneroId = libro.GeneroId,
                AutorNombreCompleto = libro.Autor != null ? $"{libro.Autor.Nombre} {libro.Autor.Apellido}" : "",
                GeneroNombre = libro.Genero?.Nombre ?? ""
            };
        }

        public IEnumerable<LibroDto> GetAll()
        {
            var libros = _libroRepository.GetAll();
            return libros.Select(l => new LibroDto
            {
                Id = l.Id,
                Titulo = l.Titulo,
                ISBN = l.ISBN,
                AutorId = l.AutorId,
                GeneroId = l.GeneroId,
                AutorNombreCompleto = l.Autor != null ? $"{l.Autor.Nombre} {l.Autor.Apellido}" : "",
                GeneroNombre = l.Genero?.Nombre ?? ""
            });
        }

        public bool Update(LibroDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            // Verificar que el autor y género existan
            var autor = _autorRepository.Get(dto.AutorId);
            if (autor == null)
                throw new ArgumentException($"No existe un autor con Id {dto.AutorId}");

            var genero = _generoRepository.Get(dto.GeneroId);
            if (genero == null)
                throw new ArgumentException($"No existe un género con Id {dto.GeneroId}");

            var libro = new Libro(dto.Id, dto.Titulo, dto.ISBN, dto.AutorId, dto.GeneroId);
            
            var result = _libroRepository.Update(libro);
            if (result)
                _libroRepository.SaveChanges();
            
            return result;
        }

        public IEnumerable<LibroDto> GetByCriteria(BusquedaCriterioDto criterioDto)
        {
            var libros = _libroRepository.GetByCriteria(criterioDto.Texto);
            return libros.Select(l => new LibroDto
            {
                Id = l.Id,
                Titulo = l.Titulo,
                ISBN = l.ISBN,
                AutorId = l.AutorId,
                GeneroId = l.GeneroId,
                AutorNombreCompleto = l.Autor != null ? $"{l.Autor.Nombre} {l.Autor.Apellido}" : "",
                GeneroNombre = l.Genero?.Nombre ?? ""
            });
        }

        public IEnumerable<LibroDto> GetByAutor(int autorId)
        {
            var libros = _libroRepository.GetByAutor(autorId);
            return libros.Select(l => new LibroDto
            {
                Id = l.Id,
                Titulo = l.Titulo,
                ISBN = l.ISBN,
                AutorId = l.AutorId,
                GeneroId = l.GeneroId,
                AutorNombreCompleto = l.Autor != null ? $"{l.Autor.Nombre} {l.Autor.Apellido}" : "",
                GeneroNombre = l.Genero?.Nombre ?? ""
            });
        }

        public IEnumerable<LibroDto> GetByGenero(int generoId)
        {
            var libros = _libroRepository.GetByGenero(generoId);
            return libros.Select(l => new LibroDto
            {
                Id = l.Id,
                Titulo = l.Titulo,
                ISBN = l.ISBN,
                AutorId = l.AutorId,
                GeneroId = l.GeneroId,
                AutorNombreCompleto = l.Autor != null ? $"{l.Autor.Nombre} {l.Autor.Apellido}" : "",
                GeneroNombre = l.Genero?.Nombre ?? ""
            });
        }
    }
}