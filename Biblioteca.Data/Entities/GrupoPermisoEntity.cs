namespace Biblioteca.Data.Entities
{
    public class GrupoPermisoEntity
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public bool Activo { get; set; } = true;
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        public ICollection<PermisoEntity> Permisos { get; set; } = new List<PermisoEntity>();
        public ICollection<UsuarioEntity> Usuarios { get; set; } = new List<UsuarioEntity>();

        public Biblioteca.Domain.Model.GrupoPermiso ToGrupo()
        {
            var permisosDom = Permisos?.Select(p => p.ToPermiso()) ?? Enumerable.Empty<Biblioteca.Domain.Model.Permiso>();
            var grupo = new Biblioteca.Domain.Model.GrupoPermiso(Id, Nombre, Descripcion, permisosDom);
            return grupo;
        }

        public static GrupoPermisoEntity FromGrupo(Biblioteca.Domain.Model.GrupoPermiso grupo)
        {
            return new GrupoPermisoEntity
            {
                Id = grupo.Id,
                Nombre = grupo.Nombre,
                Descripcion = grupo.Descripcion,
                Activo = grupo.Activo,
                FechaCreacion = grupo.FechaCreacion
            };
        }
    }
}
