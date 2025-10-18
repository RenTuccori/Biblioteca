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
        private readonly EditorialRepository _editorialRepository;

        public LibroService(LibroRepository libroRepository, AutorRepository autorRepository, GeneroRepository generoRepository, EditorialRepository editorialRepository)
        {
            _libroRepository = libroRepository;
            _autorRepository = autorRepository;
            _generoRepository = generoRepository;
            _editorialRepository = editorialRepository;
        }

        public LibroDto Add(CrearLibroDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            // Verificar que el autor, género y editorial existan
            var autor = _autorRepository.Get(dto.AutorId) ?? throw new ArgumentException($"No existe un autor con Id {dto.AutorId}");
            var genero = _generoRepository.Get(dto.GeneroId) ?? throw new ArgumentException($"No existe un género con Id {dto.GeneroId}");
            var editorial = _editorialRepository.Get(dto.EditorialId) ?? throw new ArgumentException($"No existe una editorial con Id {dto.EditorialId}");

            var libro = new Libro(0, dto.Titulo, dto.ISBN, dto.AutorId, dto.GeneroId, dto.EditorialId, dto.Estado);
            
            _libroRepository.Add(libro);
            _libroRepository.SaveChanges();

            var saved = _libroRepository.GetAll().LastOrDefault(l => string.Equals(l.Titulo, dto.Titulo, StringComparison.Ordinal)
                                                                 && string.Equals(l.ISBN, dto.ISBN, StringComparison.Ordinal)
                                                                 && l.AutorId == dto.AutorId
                                                                 && l.GeneroId == dto.GeneroId
                                                                 && l.EditorialId == dto.EditorialId);

            return new LibroDto
            {
                Id = saved?.Id ?? libro.Id,
                Titulo = libro.Titulo,
                ISBN = libro.ISBN,
                AutorId = libro.AutorId,
                GeneroId = libro.GeneroId,
                EditorialId = libro.EditorialId,
                Estado = libro.Estado,
                AutorNombreCompleto = $"{autor.Nombre} {autor.Apellido}",
                GeneroNombre = genero.Nombre,
                EditorialNombre = editorial.Nombre
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
                EditorialId = libro.EditorialId,
                Estado = libro.Estado,
                AutorNombreCompleto = libro.Autor != null ? $"{libro.Autor.Nombre} {libro.Autor.Apellido}" : "",
                GeneroNombre = libro.Genero?.Nombre ?? "",
                EditorialNombre = libro.Editorial?.Nombre ?? ""
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
                EditorialId = l.EditorialId,
                Estado = l.Estado,
                AutorNombreCompleto = l.Autor != null ? $"{l.Autor.Nombre} {l.Autor.Apellido}" : "",
                GeneroNombre = l.Genero?.Nombre ?? "",
                EditorialNombre = l.Editorial?.Nombre ?? ""
            });
        }

        public bool Update(LibroDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            var autor = _autorRepository.Get(dto.AutorId) ?? throw new ArgumentException($"No existe un autor con Id {dto.AutorId}");
            var genero = _generoRepository.Get(dto.GeneroId) ?? throw new ArgumentException($"No existe un género con Id {dto.GeneroId}");
            var editorial = _editorialRepository.Get(dto.EditorialId) ?? throw new ArgumentException($"No existe una editorial con Id {dto.EditorialId}");

            var libro = new Libro(dto.Id, dto.Titulo, dto.ISBN, dto.AutorId, dto.GeneroId, dto.EditorialId, dto.Estado);
            
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
                EditorialId = l.EditorialId,
                Estado = l.Estado,
                AutorNombreCompleto = l.Autor != null ? $"{l.Autor.Nombre} {l.Autor.Apellido}" : "",
                GeneroNombre = l.Genero?.Nombre ?? "",
                EditorialNombre = l.Editorial?.Nombre ?? ""
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
                EditorialId = l.EditorialId,
                Estado = l.Estado,
                AutorNombreCompleto = l.Autor != null ? $"{l.Autor.Nombre} {l.Autor.Apellido}" : "",
                GeneroNombre = l.Genero?.Nombre ?? "",
                EditorialNombre = l.Editorial?.Nombre ?? ""
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
                EditorialId = l.EditorialId,
                Estado = l.Estado,
                AutorNombreCompleto = l.Autor != null ? $"{l.Autor.Nombre} {l.Autor.Apellido}" : "",
                GeneroNombre = l.Genero?.Nombre ?? "",
                EditorialNombre = l.Editorial?.Nombre ?? ""
            });
        }

        public IEnumerable<LibroDto> GetByEditorial(int editorialId)
        {
            var libros = _libroRepository.GetByEditorial(editorialId);
            return libros.Select(l => new LibroDto
            {
                Id = l.Id,
                Titulo = l.Titulo,
                ISBN = l.ISBN,
                AutorId = l.AutorId,
                GeneroId = l.GeneroId,
                EditorialId = l.EditorialId,
                Estado = l.Estado,
                AutorNombreCompleto = l.Autor != null ? $"{l.Autor.Nombre} {l.Autor.Apellido}" : "",
                GeneroNombre = l.Genero?.Nombre ?? "",
                EditorialNombre = l.Editorial?.Nombre ?? ""
            });
        }

        public IEnumerable<LibroDto> GetByEstado(string estado)
        {
            var libros = _libroRepository.GetByEstado(estado);
            return libros.Select(l => new LibroDto
            {
                Id = l.Id,
                Titulo = l.Titulo,
                ISBN = l.ISBN,
                AutorId = l.AutorId,
                GeneroId = l.GeneroId,
                EditorialId = l.EditorialId,
                Estado = l.Estado,
                AutorNombreCompleto = l.Autor != null ? $"{l.Autor.Nombre} {l.Autor.Apellido}" : "",
                GeneroNombre = l.Genero?.Nombre ?? "",
                EditorialNombre = l.Editorial?.Nombre ?? ""
            });
        }
    }
}