using Biblioteca.Data;
using Biblioteca.Domain.Model;
using Biblioteca.DTOs;

namespace Biblioteca.Domain.Services
{
    public class PersonaService
    {
        private readonly PersonaRepository _personaRepository;

        public PersonaService(PersonaRepository personaRepository)
        {
            _personaRepository = personaRepository;
        }

        public PersonaDto Add(CrearPersonaDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            var persona = new Persona(0, dto.Nombre, dto.Apellido, dto.Dni, dto.Email);
            
            _personaRepository.Add(persona);
            _personaRepository.SaveChanges();

            return new PersonaDto
            {
                Id = persona.Id,
                Nombre = persona.Nombre,
                Apellido = persona.Apellido,
                Dni = persona.Dni,
                Email = persona.Email
            };
        }

        public bool Delete(int id)
        {
            try
            {
                var result = _personaRepository.Delete(id);
                if (result)
                    _personaRepository.SaveChanges();
                return result;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
        }

        public PersonaDto? Get(int id)
        {
            var persona = _personaRepository.Get(id);
            if (persona == null)
                return null;

            return new PersonaDto
            {
                Id = persona.Id,
                Nombre = persona.Nombre,
                Apellido = persona.Apellido,
                Dni = persona.Dni,
                Email = persona.Email
            };
        }

        public IEnumerable<PersonaDto> GetAll()
        {
            var personas = _personaRepository.GetAll();
            return personas.Select(p => new PersonaDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Apellido = p.Apellido,
                Dni = p.Dni,
                Email = p.Email
            });
        }

        public bool Update(PersonaDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            var persona = new Persona(dto.Id, dto.Nombre, dto.Apellido, dto.Dni, dto.Email);
            
            var result = _personaRepository.Update(persona);
            if (result)
                _personaRepository.SaveChanges();
            
            return result;
        }

        public IEnumerable<PersonaDto> GetByCriteria(BusquedaCriterioDto criterioDto)
        {
            var personas = _personaRepository.GetByCriteria(criterioDto.Texto);
            return personas.Select(p => new PersonaDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Apellido = p.Apellido,
                Dni = p.Dni,
                Email = p.Email
            });
        }
    }
}
