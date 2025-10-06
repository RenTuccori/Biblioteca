using Biblioteca.Data.Entities;
using Biblioteca.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Data
{
    public class PersonaRepository : IRepository<Persona>
    {
        private readonly BibliotecaContext _context;

        public PersonaRepository(BibliotecaContext context)
        {
            _context = context;
        }

        public void Add(Persona persona)
        {
            if (persona == null)
                throw new ArgumentNullException(nameof(persona));

            // Verificar si ya existe una persona con el mismo DNI
            if (DniExists(persona.Dni))
                throw new InvalidOperationException($"Ya existe una persona con el DNI '{persona.Dni}'.");

            var personaEntity = PersonaEntity.FromPersona(persona);
            _context.Personas.Add(personaEntity);
        }

        public Persona? Get(int id)
        {
            var personaEntity = _context.Personas.Find(id);
            return personaEntity?.ToPersona();
        }

        public IEnumerable<Persona> GetAll()
        {
            return _context.Personas
                .OrderBy(p => p.Apellido)
                .ThenBy(p => p.Nombre)
                .ToList()
                .Select(p => p.ToPersona());
        }

        public bool Update(Persona persona)
        {
            if (persona == null)
                throw new ArgumentNullException(nameof(persona));

            var existing = _context.Personas.Find(persona.Id);
            if (existing == null)
                return false;

            // Verificar si ya existe otra persona con el mismo DNI
            if (DniExists(persona.Dni, persona.Id))
                throw new InvalidOperationException($"Ya existe otra persona con el DNI '{persona.Dni}'.");

            existing.Nombre = persona.Nombre;
            existing.Apellido = persona.Apellido;
            existing.Dni = persona.Dni;
            existing.Email = persona.Email;
            return true;
        }

        public bool Delete(int id)
        {
            var persona = _context.Personas.Find(id);
            if (persona == null)
                return false;

            // Verificar si la persona tiene usuarios asociados
            var tieneUsuarios = _context.Usuarios.Any(u => u.PersonaId == id);
            if (tieneUsuarios)
                throw new InvalidOperationException("No se puede eliminar la persona porque tiene usuarios asociados.");

            _context.Personas.Remove(persona);
            return true;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public bool DniExists(string dni, int? excludeId = null)
        {
            var query = _context.Personas.Where(p => p.Dni == dni);
            
            if (excludeId.HasValue)
                query = query.Where(p => p.Id != excludeId.Value);

            return query.Any();
        }

        public IEnumerable<Persona> GetByCriteria(string criterio)
        {
            if (string.IsNullOrWhiteSpace(criterio))
                return GetAll();

            return _context.Personas
                .Where(p => p.Nombre.Contains(criterio) || 
                           p.Apellido.Contains(criterio) || 
                           p.Dni.Contains(criterio) ||
                           p.Email.Contains(criterio))
                .OrderBy(p => p.Apellido)
                .ThenBy(p => p.Nombre)
                .ToList()
                .Select(p => p.ToPersona());
        }
    }
}
