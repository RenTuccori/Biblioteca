namespace Biblioteca.Data.Entities
{
    public class PersonaEntity
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Dni { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // Método para convertir a entidad de dominio
        public Biblioteca.Domain.Model.Persona ToPersona()
        {
            return new Biblioteca.Domain.Model.Persona(Id, Nombre, Apellido, Dni, Email);
        }

        // Método estático para crear desde entidad de dominio
        public static PersonaEntity FromPersona(Biblioteca.Domain.Model.Persona persona)
        {
            return new PersonaEntity
            {
                Id = persona.Id,
                Nombre = persona.Nombre,
                Apellido = persona.Apellido,
                Dni = persona.Dni,
                Email = persona.Email
            };
        }
    }
}
