namespace Biblioteca.Data.Entities
{
    public class UsuarioEntity
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public string Clave { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
        public int PersonaId { get; set; }

        // Propiedad de navegación
        public PersonaEntity? Persona { get; set; }

        // Método para convertir a entidad de dominio
        public Biblioteca.Domain.Model.Usuario ToUsuario()
        {
            var usuario = new Biblioteca.Domain.Model.Usuario(Id, NombreUsuario, Clave, Rol, PersonaId);
            
            if (Persona != null)
                usuario.SetPersona(Persona.ToPersona());
            
            return usuario;
        }

        // Método estático para crear desde entidad de dominio
        public static UsuarioEntity FromUsuario(Biblioteca.Domain.Model.Usuario usuario)
        {
            return new UsuarioEntity
            {
                Id = usuario.Id,
                NombreUsuario = usuario.NombreUsuario,
                Clave = usuario.Clave,
                Rol = usuario.Rol,
                PersonaId = usuario.PersonaId
            };
        }
    }
}
