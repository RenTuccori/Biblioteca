// EN: Biblioteca.Domain.Services/AutorService.cs

using Biblioteca.Data;
using Biblioteca.Domain.Model;
using Biblioteca.DTOs;

namespace Biblioteca.Domain.Services
{
    public class AutorService
    {
        private readonly AutorRepository _autorRepository;

        public AutorService(AutorRepository autorRepository)
        {
            _autorRepository = autorRepository;
        }

        public AutorDto Add(CrearAutorDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            var autor = new Autor(0, dto.Nombre, dto.Apellido);
            
            _autorRepository.Add(autor);
            _autorRepository.SaveChanges();

            return new AutorDto
            {
                Id = autor.Id,
                Nombre = autor.Nombre,
                Apellido = autor.Apellido
            };
        }

        public bool Delete(int id)
        {
            try
            {
                var result = _autorRepository.Delete(id);
                if (result)
                    _autorRepository.SaveChanges();
                return result;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
        }

        public AutorDto? Get(int id)
        {
            var autor = _autorRepository.Get(id);
            if (autor == null)
                return null;

            return new AutorDto
            {
                Id = autor.Id,
                Nombre = autor.Nombre,
                Apellido = autor.Apellido
            };
        }

        public IEnumerable<AutorDto> GetAll()
        {
            var autores = _autorRepository.GetAll();
            return autores.Select(a => new AutorDto
            {
                Id = a.Id,
                Nombre = a.Nombre,
                Apellido = a.Apellido
            });
        }

        public bool Update(AutorDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            var autor = new Autor(dto.Id, dto.Nombre, dto.Apellido);
            
            var result = _autorRepository.Update(autor);
            if (result)
                _autorRepository.SaveChanges();
            
            return result;
        }

        public IEnumerable<AutorDto> GetByCriteria(BusquedaCriterioDto criterioDto)
        {
            var autores = _autorRepository.GetByCriteria(criterioDto.Texto);
            return autores.Select(a => new AutorDto
            {
                Id = a.Id,
                Nombre = a.Nombre,
                Apellido = a.Apellido
            });
        }
    }
}