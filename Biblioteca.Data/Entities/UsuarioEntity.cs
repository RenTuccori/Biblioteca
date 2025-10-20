namespace Biblioteca.Data.Entities
{
    public class UsuarioEntity
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
        public int PersonaId { get; set; }

        // Seguridad
        public byte[] PasswordHash { get; set; } = Array.Empty<byte>();
        public byte[] Salt { get; set; } = Array.Empty<byte>();

        // Auditoría
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public bool Activo { get; set; } = true;

        // Propiedad de navegación
        public PersonaEntity? Persona { get; set; }

        // Relación muchos a muchos con GrupoPermisoEntity
        public ICollection<GrupoPermisoEntity> Grupos { get; set; } = new List<GrupoPermisoEntity>();

        // Método para convertir a entidad de dominio
        public Biblioteca.Domain.Model.Usuario ToUsuario()
        {
            var usuario = new Biblioteca.Domain.Model.Usuario(Id, NombreUsuario, Rol, PersonaId);
            usuario.ApplyCredentials(PasswordHash, Salt);
            usuario.SetAudit(FechaCreacion, Activo);
            
            if (Persona != null)
                usuario.SetPersona(Persona.ToPersona());

            if (Grupos != null && Grupos.Count > 0)
            {
                var gruposDominio = Grupos.Select(g => g.ToGrupo());
                usuario.SetGrupos(gruposDominio);
            }
            
            return usuario;
        }

        // Método estático para crear desde entidad de dominio
        public static UsuarioEntity FromUsuario(Biblioteca.Domain.Model.Usuario usuario)
        {
            return new UsuarioEntity
            {
                Id = usuario.Id,
                NombreUsuario = usuario.NombreUsuario,
                Rol = usuario.Rol,
                PersonaId = usuario.PersonaId,
                PasswordHash = usuario.PasswordHash,
                Salt = usuario.Salt,
                FechaCreacion = usuario.FechaCreacion,
                Activo = usuario.Activo
            };
        }
    }
}
