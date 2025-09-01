using Biblioteca.Data;
using Biblioteca.Domain.Model;
using Biblioteca.DTOs;

namespace Biblioteca.Domain.Services
{
    public class GeneroService
    {
        private readonly GeneroRepository _generoRepository;

        public GeneroService(GeneroRepository generoRepository)
        {
            _generoRepository = generoRepository;
        }

        public GeneroDto Add(CrearGeneroDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            var genero = new Genero(0, dto.Nombre);
            
            _generoRepository.Add(genero);
            _generoRepository.SaveChanges();

            return new GeneroDto
            {
                Id = genero.Id,
                Nombre = genero.Nombre
            };
        }

        public bool Delete(int id)
        {
            try
            {
                var result = _generoRepository.Delete(id);
                if (result)
                    _generoRepository.SaveChanges();
                return result;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
        }

        public GeneroDto? Get(int id)
        {
            var genero = _generoRepository.Get(id);
            if (genero == null)
                return null;

            return new GeneroDto
            {
                Id = genero.Id,
                Nombre = genero.Nombre
            };
        }

        public IEnumerable<GeneroDto> GetAll()
        {
            var generos = _generoRepository.GetAll();
            return generos.Select(g => new GeneroDto
            {
                Id = g.Id,
                Nombre = g.Nombre
            });
        }

        public bool Update(GeneroDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            var genero = new Genero(dto.Id, dto.Nombre);
            
            var result = _generoRepository.Update(genero);
            if (result)
                _generoRepository.SaveChanges();
            
            return result;
        }

        public IEnumerable<GeneroDto> GetByCriteria(BusquedaCriterioDto criterioDto)
        {
            var generos = _generoRepository.GetByCriteria(criterioDto.Texto);
            return generos.Select(g => new GeneroDto
            {
                Id = g.Id,
                Nombre = g.Nombre
            });
        }
    }
}